using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProject.MappingRegisters;
using MVCProject.Models;
using MVCProject.Repositories;
using MVCProject.ViewModels.AccomodationViewModels;
using MVCProject.ViewModels.ActivityViewModels;
using MVCProject.ViewModels.BookingViewModel;
using MVCProject.ViewModels.FlightViewModels;
using System.Diagnostics;
using System.Security.Claims;

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

        [HttpGet]
        public IActionResult Index([FromQuery] string searchQuery = "", [FromQuery] int pageNumber = 1)
        {
            int pageSize = 6;
            var (bookings, count) = unitOfWork.BookingRepository.GetAllWithFilterBy(searchQuery, pageNumber, pageSize);

            var bookingVMs = bookings.Adapt<List<DisplayBookingVM>>();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            return View("Index", bookingVMs);
        }

        public IActionResult Book()
        {
            return View("Book");
        }

        [HttpPost]
        public IActionResult Book(AddBookingVM addBookingVM)
        {
            if(ModelState.IsValid)
            {
                foreach (var i in addBookingVM.Accomodations)
                {
                    if (i.CheckInDate >= i.CheckOutDate)
                    {
                        ModelState.AddModelError("", "Check out date must be > Check in date");
                        return View("Book", addBookingVM);
                    }
                }

                var booking = addBookingVM.Adapt<Models.Booking>();
                string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                booking.UserId = currentUserId;
                
                unitOfWork.BookingRepository.Add(booking);
                unitOfWork.Save();

                decimal totalAmount = 0.0m;

                foreach(var i in addBookingVM.Accomodations)
                {

                    var accomodation = unitOfWork.AccomodationRepositroy.GetById(i.Id);
                    accomodation.AvailableRooms -= 1;

                    unitOfWork.BookingAccomodationRepository.Add(new BookingAccomodation
                    {
                        BookingId = booking.Id,
                        AccomodationId = i.Id,
                        CheckInDate = i.CheckInDate,
                        CheckOutDate = i.CheckOutDate
                    });
                    decimal days = (i.CheckOutDate.Date - i.CheckInDate.Date).Days;
                    totalAmount += accomodation.PricePerNight * days;
                }
                

                foreach (var i in addBookingVM.ActivitiesId)
                {
                    var activity = unitOfWork.ActivityRepository.GetById(i);
                    activity.Capacity -= 1;

                    unitOfWork.BookingActivityRepository.Add(new BookingActivity
                    {
                        BookingId = booking.Id,
                        ActivityId = i
                    });
                    totalAmount += activity.Price;

                }


                foreach (var i in addBookingVM.FlightsId)
                {

                    var flight = unitOfWork.FlightRepository.GetById(i);

                    flight.AvailableSeats -= 1;

                    unitOfWork.BookingFlightRepository.Add(new BookingFlight
                    {
                        BookingId = booking.Id,
                        FlightId = i
                    });

                    totalAmount += flight.Price;
                }

                booking.TotalAmount = totalAmount;
                unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View("Book", addBookingVM);
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
