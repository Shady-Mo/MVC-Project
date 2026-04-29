using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVCProject.Models;
using MVCProject.Repositories;
using MVCProject.Services.ImgAddingService;
using MVCProject.ViewModels.ActivityViewModels;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVCProject.Controllers
{
    
    public class ActivityController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        public IFileService FileService { get; }

        public ActivityController(UnitOfWork unitOfWork, IFileService fileService)
        {
            this.unitOfWork = unitOfWork;
            FileService = fileService;
        }
        [HttpGet]
        public IActionResult Index([FromQuery] string searchQuery = "", [FromQuery] decimal? maxPrice = null, [FromQuery] int? minCapacity = null, [FromQuery] int pageNumber = 1)
        {
            int pageSize = 6; // Show 6 cards per page
            var (activities, totalCount) = unitOfWork.ActivityRepository.GetAllWithFilterBy(searchQuery, maxPrice, minCapacity, pageNumber, pageSize);

            var activitiesVM = activities.Adapt<List<DisplayActivityVM>>();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return View("Index", activitiesVM);
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpGet]
        public IActionResult SellerActivities([FromQuery] string searchQuery = "", [FromQuery] decimal? maxPrice = null, [FromQuery] int? minCapacity = null, [FromQuery] int pageNumber = 1)
        {
            int pageSize = 6; // Show 6 cards per page
            var (activities, totalCount) = unitOfWork.ActivityRepository.GetAllWithFilterBySellerId(User.FindFirstValue(ClaimTypes.NameIdentifier), searchQuery, maxPrice, minCapacity, pageNumber, pageSize);

            var activitiesVM = activities.Adapt<List<DisplayActivityVM>>();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return View("SellerActivities", activitiesVM);
        }



        [Authorize(Roles = "Admin,Seller")]
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpPost]
        public async Task<IActionResult> Create(AddActivityVM addActivityVM)
        {
            if (ModelState.IsValid)
            {
                var activity = addActivityVM.Adapt<Activity>();

                activity.SellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (addActivityVM.ImageFile != null)
                {
                    // Use the global service
                    activity.Img = await FileService.SaveFileAsync(addActivityVM.ImageFile, "images");
                }

                unitOfWork.ActivityRepository.Add(activity);
                unitOfWork.Save();
                return RedirectToAction(nameof(SellerActivities));
            }
            return View("Create", addActivityVM);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var activityEntity = unitOfWork.ActivityRepository.GetById(id);
            if (activityEntity == null) return NotFound();

            var activity = activityEntity.Adapt<DisplayActivityVM>();
            return View("Details", activity);
        }


        [Authorize(Roles = "Admin,Seller")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var activityEntity = unitOfWork.ActivityRepository.GetById(id);
            if (activityEntity == null) return NotFound();

            var activity = activityEntity.Adapt<UpdateActivityVM>();
            return View("Edit", activity);
        }


        [Authorize(Roles = "Admin,Seller")]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateActivityVM updateActivityVM)
        {
            if (ModelState.IsValid)
            {
                var existingActivity = unitOfWork.ActivityRepository.GetById(updateActivityVM.Id);
                if (existingActivity == null) return NotFound();

                updateActivityVM.Adapt(existingActivity);

                // 3. Handle image update
                if (updateActivityVM.ImageFile != null)
                {
                    FileService.DeleteFile(existingActivity.Img, "images");

                    existingActivity.Img = await FileService.SaveFileAsync(updateActivityVM.ImageFile, "images");
                }

                unitOfWork.ActivityRepository.Update(existingActivity);
                unitOfWork.Save();
                return RedirectToAction(nameof(SellerActivities));
            }
            return View("Edit", updateActivityVM);
        }


        [Authorize(Roles = "Admin,Seller")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var activityEntity = unitOfWork.ActivityRepository.GetById(id);
            if (activityEntity == null) return NotFound();

            if (!string.IsNullOrEmpty(activityEntity.Img))
            {
                FileService.DeleteFile(activityEntity.Img, "images");
            }
            unitOfWork.ActivityRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction(nameof(SellerActivities));
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpGet]
        public IActionResult checkDate(DateTime Date)
        {
            if (Date <= DateTime.Now)
            {
                return Json("The date must be in the future.");
            }
            return Json(true);
        }
    }
}
