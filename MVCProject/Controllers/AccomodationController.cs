using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using MVCProject.Repositories;
using MVCProject.Services.ImgAddingService;
using MVCProject.ViewModels.AccomodationViewModels;
using MVCProject.ViewModels.ActivityViewModels;
using System.Security.Claims;

namespace MVCProject.Controllers
{
    [Authorize]
    public class AccomodationController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IFileService fileService;

        public AccomodationController(UnitOfWork unitOfWork, IFileService fileService)
        {
            this.unitOfWork = unitOfWork;
            this.fileService = fileService;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] string searchQuery = "", [FromQuery] decimal? maxPrice = null, [FromQuery] int? minCapacity = null, [FromQuery] int pageNumber = 1)
        {
            int pageSize = 6;
            var (accomodations, count) = unitOfWork.AccomodationRepositroy.GetAllWithFilterBy(searchQuery, maxPrice, minCapacity, pageNumber, pageSize);

            var accomodationesVM = accomodations.Adapt<List<DisplayAccomodationVM>>();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(count / (double)pageSize);


            return View("Index", accomodationesVM);
        }

        [Authorize(Roles = "Seller")]
        [HttpGet]
        public IActionResult SellerAccomodations([FromQuery] string searchQuery = "", [FromQuery] decimal? maxPrice = null, [FromQuery] int? minCapacity = null, [FromQuery] int pageNumber = 1)
        {
            int pageSize = 6;
            var (accomodations, count) = unitOfWork.AccomodationRepositroy.GetAllWithFilterBySellerId(User.FindFirstValue(ClaimTypes.NameIdentifier), searchQuery, maxPrice, minCapacity, pageNumber, pageSize);

            var accomodationesVM = accomodations.Adapt<List<DisplayAccomodationVM>>();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(count / (double)pageSize);


            return View("SellerAccomodations", accomodationesVM);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var accomodation = unitOfWork.AccomodationRepositroy.GetById(id);
            var accomodationVM = accomodation.Adapt<DisplayAccomodationVM>();

            return View("Details", accomodationVM);
        }


        [Authorize(Roles = "Seller")]
        [HttpGet]
        public IActionResult New()
        {
            return View("New");
        }


        [Authorize(Roles = "Seller")]
        [HttpPost]
        public async Task<IActionResult> New(AddAcccomodationVM acccomodationVM)
        {
            if(ModelState.IsValid)
            {
                var accomodation = acccomodationVM.Adapt<Accomodation>();

                accomodation.SellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (acccomodationVM.ImageFile != null)
                {
                    accomodation.Image = await fileService.SaveFileAsync(acccomodationVM.ImageFile, "images");
                }

                unitOfWork.AccomodationRepositroy.Add(accomodation);
                unitOfWork.Save();

                return RedirectToAction("SellerAccomodations");
            }
            return View("New", acccomodationVM);
        }


        [Authorize(Roles = "Seller")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var accomodation = unitOfWork.AccomodationRepositroy.GetById(id);
            var accomodationVM = accomodation.Adapt<EditAccomodationVM>();
            return View("Edit", accomodationVM);
        }


        [Authorize(Roles = "Seller")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditAccomodationVM accomodationVM)
        {
            if (ModelState.IsValid)
            {
                var existingAccomodation = unitOfWork.AccomodationRepositroy.GetById(accomodationVM.Id);
                if (existingAccomodation == null) return NotFound();

                accomodationVM.Adapt(existingAccomodation);

                if (accomodationVM.ImageFile != null)
                {
                    fileService.DeleteFile(existingAccomodation.Image, "images");

                    existingAccomodation.Image = await fileService.SaveFileAsync(accomodationVM.ImageFile, "images");
                }

                unitOfWork.AccomodationRepositroy.Update(existingAccomodation);
                unitOfWork.Save();
                return RedirectToAction(nameof(SellerAccomodations));
            }
            return View("Edit", accomodationVM);
        }


        [Authorize(Roles = "Seller")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var accomodation = unitOfWork.AccomodationRepositroy.GetById(id);
            if (accomodation == null) return NotFound();

            var accomodationVM = accomodation.Adapt<DisplayAccomodationVM>();

            return View("Delete",accomodationVM);
        }


        [Authorize(Roles = "Seller")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var accomodation = unitOfWork.AccomodationRepositroy.GetById(id);
            if (accomodation == null) return NotFound();

            if (!string.IsNullOrEmpty(accomodation.Image))
            {
                fileService.DeleteFile(accomodation.Image, "images");
            }

            unitOfWork.AccomodationRepositroy.Delete(id);
            unitOfWork.Save();

            return RedirectToAction(nameof(SellerAccomodations));
        }

    }
}
