using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Repositories;
using MVCProject.ViewModels.AccomodationViewModels;
using MVCProject.ViewModels.ActivityViewModels;
using MVCProject.ViewModels.FlightViewModels;

namespace MVCProject.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public BookingController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Book()
        {
            return View("Book");
        }

        [HttpGet]
        public JsonResult GetTravelData(string source, string dest, DateTime date)
        {
            var flights = unitOfWork.FlightRepository.GetByLocation(source, dest, date);
            var hotels = unitOfWork.AccomodationRepositroy.GetByLocation(dest);
            var activities = unitOfWork.ActivityRepository.GetByLocation(dest);

            var flightsVM = flights.Adapt<List<DisplayFlightVM>>();
            var hotelsVM = hotels.Adapt<List<DisplayAccomodationVM>>();
            var activitiesVM = activities.Adapt<List<DisplayActivityVM>>();

            return Json(new { flights = flightsVM, hotels = hotelsVM, activities = activitiesVM });
        }
    }
}
