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

        //[Authorize(Roles ="Admin")]
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

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public IActionResult Details(int id)
        {

            var booking = unitOfWork.BookingRepository.GetByIdIncluded(id);

            if (booking == null) return NotFound();

            var bookingVM = booking.Adapt<DisplayBookingVM>();
            return View("Details", bookingVM);
        }

        [HttpGet]
        public IActionResult MyBooking([FromQuery] string searchQuery = "", [FromQuery] int pageNumber = 1)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            int pageSize = 6;
            var (bookings, count) = unitOfWork.BookingRepository.GetAllWithFilterByUserId(currentUserId, searchQuery, pageNumber, pageSize);

            var bookingVMs = bookings.Adapt<List<DisplayBookingVM>>();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            return View("MyBooking", bookingVMs);
        }

        [HttpGet]
        public IActionResult Book()
        {
            return View("Book");
        }

        [HttpPost]
        public IActionResult Book(AddBookingVM addBookingVM)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in addBookingVM.Accomodations)
                {
                    if (i.CheckInDate >= i.CheckOutDate)
                    {
                        ModelState.AddModelError("", "Check out date must be > Check in date");
                        return View("Book", addBookingVM);
                    }

                    if (i.CheckInDate < addBookingVM.BookingDate)
                    {
                        ModelState.AddModelError("", "Check in date must be > Booking Date");
                        return View("Book", addBookingVM);
                    }
                }

                foreach (var i in addBookingVM.ActivitiesId)
                {
                    var activity = unitOfWork.ActivityRepository.GetById(i);
                    if (activity.Date < addBookingVM.BookingDate)
                    {
                        ModelState.AddModelError("", "Activity date must be > Booking Date");
                        return View("Book", addBookingVM);
                    }
                }


                var booking = addBookingVM.Adapt<Models.Booking>();
                string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                booking.UserId = currentUserId;
                booking.FlightId = addBookingVM.FlightId;

                unitOfWork.BookingRepository.Add(booking);
                unitOfWork.Save();

                decimal totalAmount = 0.0m;

                foreach (var i in addBookingVM.Accomodations)
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

                var flight = unitOfWork.FlightRepository.GetById(addBookingVM.FlightId);
                flight.AvailableSeats -= 1;
                totalAmount += flight.Price;


                booking.TotalAmount = totalAmount;
                unitOfWork.Save();

                return RedirectToAction("MyBooking");
            }
            return View("Book", addBookingVM);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var booking = unitOfWork.BookingRepository.GetByIdIncluded(id);


            var bookingVM = new EditBookingVM
            {
                Id = booking.Id,
                Country = booking.Country,
                BookingDate = booking.BookingDate,
                Status = booking.Status,
                FlightId = booking.FlightId,
                ActivitiesId = booking.BookingActivities?.Select(a => a.ActivityId).ToList() ?? new List<int>(),
                Accomodations = booking.bookingAccomodations?.Select(a => new BookingAccomodationVM
                {
                    Id = a.AccomodationId,
                    CheckInDate = a.CheckInDate,
                    CheckOutDate = a.CheckOutDate
                }).ToList() ?? new List<BookingAccomodationVM>(),
                src = booking.Flight.DepartureAirport
            };

            return View("Edit", bookingVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditBookingVM editBookingVM)
        {
            if (!ModelState.IsValid) return View("Edit", editBookingVM);

            foreach (var i in editBookingVM.Accomodations)
            {
                if (i.CheckInDate >= i.CheckOutDate)
                {
                    ModelState.AddModelError("", "Check out date must be > Check in date");
                    return View("Edit", editBookingVM);
                }

                if (i.CheckInDate < editBookingVM.BookingDate)
                {
                    ModelState.AddModelError("", "Check in date must be > Booking Date");
                    return View("Edit", editBookingVM);
                }
            }

            foreach (var i in editBookingVM.ActivitiesId)
            {
                var activity = unitOfWork.ActivityRepository.GetById(i);
                if (activity.Date < editBookingVM.BookingDate)
                {
                    ModelState.AddModelError("", "Activity date must be > Booking Date");
                    return View("Edit", editBookingVM);
                }
            }


            var existingBooking = unitOfWork.BookingRepository.GetByIdIncluded(editBookingVM.Id);
            if (existingBooking == null) return NotFound();

            foreach (var i in existingBooking.bookingAccomodations)
            {
                unitOfWork.AccomodationRepositroy.GetById(i.AccomodationId).AvailableRooms += 1;
            }

            foreach (var i in existingBooking.BookingActivities)
            {
                unitOfWork.ActivityRepository.GetById(i.ActivityId).Capacity += 1;
            }

            unitOfWork.FlightRepository.GetById(existingBooking.FlightId).AvailableSeats += 1;
            

            existingBooking.bookingAccomodations.Clear();
            existingBooking.BookingActivities.Clear();
            unitOfWork.Save();

            existingBooking.Country = editBookingVM.Country;
            existingBooking.BookingDate = editBookingVM.BookingDate;
            existingBooking.Status = editBookingVM.Status;

            decimal totalAmount = 0.0m;

            if (editBookingVM.Accomodations != null)
            {
                foreach (var i in editBookingVM.Accomodations)
                {

                    var accomodation = unitOfWork.AccomodationRepositroy.GetById(i.Id);
                    accomodation.AvailableRooms -= 1;

                    unitOfWork.BookingAccomodationRepository.Add(new BookingAccomodation
                    {
                        BookingId = editBookingVM.Id,
                        AccomodationId = i.Id,
                        CheckInDate = i.CheckInDate,
                        CheckOutDate = i.CheckOutDate
                    });
                    decimal days = (i.CheckOutDate.Date - i.CheckInDate.Date).Days;
                    totalAmount += accomodation.PricePerNight * days;
                }
            }

            if (editBookingVM.ActivitiesId != null)
            {
                foreach (var i in editBookingVM.ActivitiesId)
                {
                    var activity = unitOfWork.ActivityRepository.GetById(i);
                    activity.Capacity -= 1;

                    unitOfWork.BookingActivityRepository.Add(new BookingActivity
                    {
                        BookingId = editBookingVM.Id,
                        ActivityId = i
                    });
                    totalAmount += activity.Price;
                }
            }

            var flight = unitOfWork.FlightRepository.GetById(editBookingVM.FlightId);

            flight.AvailableSeats -= 1;
            totalAmount += flight.Price;


            existingBooking.TotalAmount = totalAmount;
            unitOfWork.BookingRepository.Update(existingBooking);
            unitOfWork.Save();

            return RedirectToAction("MyBooking"); ;
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


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var booking = unitOfWork.BookingRepository.GetByIdIncluded(id);
            if (booking == null) return NotFound();

            var bookingVM = booking.Adapt<DisplayBookingVM>();
            return View("Delete", bookingVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var booking = unitOfWork.BookingRepository.GetByIdIncluded(id);
            if (booking == null) return NotFound();

            if (booking.bookingAccomodations != null)
            {
                foreach (var ba in booking.bookingAccomodations)
                {
                    var acc = unitOfWork.AccomodationRepositroy.GetById(ba.AccomodationId);
                    if (acc != null) acc.AvailableRooms += 1;
                }

            }

            if (booking.BookingActivities != null)
            {
                foreach (var ba in booking.BookingActivities)
                {
                    var act = unitOfWork.ActivityRepository.GetById(ba.ActivityId);
                    if (act != null) act.Capacity += 1;
                }
            }

            var flight = unitOfWork.FlightRepository.GetById(booking.FlightId);
            if (flight != null) flight.AvailableSeats += 1;

            unitOfWork.BookingRepository.Delete(id);
            unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

    }
}
