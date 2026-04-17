using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVCProject.Models;
using MVCProject.Repositories;
using MVCProject.ViewModels.ActivityViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVCProject.Controllers
{
    public class ActivityController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public ActivityController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var activities = unitOfWork.ActivityRepository.GetAll();
            var activitiesVM = activities.Adapt<List<DisplayActivityVM>>();
            return View("Index", activitiesVM);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public IActionResult Create(AddActivityVM addActivityVM)
        {
            if (ModelState.IsValid)
            {
                var activity = addActivityVM.Adapt<Activity>();
                unitOfWork.ActivityRepository.Add(activity);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
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
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var activityEntity = unitOfWork.ActivityRepository.GetById(id);
            if (activityEntity == null) return NotFound();

            var activity = activityEntity.Adapt<UpdateActivityVM>();
            return View("Edit", activity);
        }
        [HttpPost]
        public IActionResult Edit(UpdateActivityVM updateActivityVM)
        {
            if (ModelState.IsValid)
            {
                var activity = updateActivityVM.Adapt<Activity>();
                unitOfWork.ActivityRepository.Update(activity);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View("Edit", updateActivityVM);
        }
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
