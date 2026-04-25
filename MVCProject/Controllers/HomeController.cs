using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MVCProject.Models;
using MVCProject.Repositories;
using MVCProject.ViewModels.AccomodationViewModels;
using MVCProject.ViewModels.ActivityViewModels;
using MVCProject.ViewModels.HomeViewModels;
using System.Diagnostics;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public HomeController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[OutputCache(PolicyName = "GlobalExpiry")]
        public IActionResult Index()
        {
            var accommodations = _unitOfWork.AccomodationRepositroy.GetAll().Take(6).ToList();
            var activities = _unitOfWork.ActivityRepository.GetAll().Take(6).ToList();

            var model = new HomeIndexViewModel
            {
                Accommodations = accommodations.Adapt<List<DisplayAccomodationVM>>(),
                Activities = activities.Adapt<List<DisplayActivityVM>>(),
                TotalAccommodations = _unitOfWork.AccomodationRepositroy.GetAll().Count(),
                TotalActivities = _unitOfWork.ActivityRepository.GetAll().Count()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}