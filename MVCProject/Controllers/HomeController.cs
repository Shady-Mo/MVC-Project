using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MVCProject.Models;
using MVCProject.Repositories;
using MVCProject.ViewModels.AccomodationViewModels;
using MVCProject.ViewModels.ActivityViewModels;
using MVCProject.ViewModels.HomeViewModels;
using System.Diagnostics;

namespace MVCProject.Controllers {
    public class HomeController : Controller {
        private readonly UnitOfWork _unitOfWork;

        public HomeController(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        //[OutputCache(PolicyName = "GlobalExpiry")]
        public async Task<IActionResult> Index() {
            var accommodations = await _unitOfWork.AccomodationRepositroy.GetAll()
                .Include(acc => acc.bookingAccomodations)
                .OrderByDescending(acc => acc.bookingAccomodations.Count())
                .Take(6)
                .ToListAsync();

            var activities = await _unitOfWork.ActivityRepository.GetAll()
                .Include(act => act.BookingActivities)
                .OrderByDescending(act => act.BookingActivities.Count())
                .Take(6)
                .ToListAsync();

            var flights = await _unitOfWork.FlightRepository.GetAll()
                .Where(f => f.DepartureDateTime > DateTime.Now)
                .OrderBy(f => f.DepartureDateTime)
                .Take(3)
                .ToListAsync();

            var model = new HomeIndexViewModel {
                Accommodations = accommodations.Adapt<List<MostBookedAccommodationsViewModel>>(),
                Activities = activities.Adapt<List<MostBookedActivitiesViewModel>>(),
                Flights = flights.Adapt<List<UpcomingFlightsViewModel>>()
            };

            return View(model);
        }

        [HttpGet]
        //[OutputCache(PolicyName = "GlobalExpiryWithFilteration")]
        public async Task<IActionResult> Filter(FilterResult request) {
            var accommodations = _unitOfWork.AccomodationRepositroy.GetAll();
            var activities = _unitOfWork.ActivityRepository.GetAll();

            if (!string.IsNullOrEmpty(request.Location) && request.Location != "all") {
                accommodations = accommodations.Where(acc => acc.Location.Contains(request.Location));
                activities = activities.Where(act => act.Location == request.Location);
            }

            if (request.Price != null && request.Price > 0) {
                accommodations = accommodations.Where(acc => acc.PricePerNight <= request.Price);
                activities = activities.Where(act => act.Price <= request.Price);
            }

            var accommodationsResult = await accommodations
                .Include(acc => acc.bookingAccomodations)
                .OrderByDescending(acc => acc.bookingAccomodations.Count())
                .Take(6)
                .ToListAsync();

            var activitiesResult = await activities
                .Include(acc => acc.BookingActivities)
                .OrderByDescending(act => act.BookingActivities.Count())
                .Take(6)
                .ToListAsync();

            return Json(new {
                Accommodations = accommodationsResult.Adapt<List<MostBookedAccommodationsViewModel>>(),
                Activities = activitiesResult.Adapt<List<MostBookedActivitiesViewModel>>(),
            });
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}