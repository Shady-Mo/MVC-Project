using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using MVCProject.Repositories;
using MVCProject.ViewModels.FlightViewModels;
using System.Security.Claims;

namespace MVCProject.Controllers
{
    public class FlightController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public FlightController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult Index([FromQuery] string searchQuery = "",
                           [FromQuery] string destination = "",
                           [FromQuery] DateTime? date = null,
                           [FromQuery] int pageNumber = 1,
                           [FromQuery] string sortBy = "default")
        {
            int pageSize = 6;
            var (flights, totalCount) = unitOfWork.FlightRepository
                .GetAllWithFilterBy(searchQuery, destination, date, pageNumber, pageSize, sortBy);

            var flightsVM = flights.Adapt<List<DisplayFlightVM>>();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewBag.SortBy = sortBy;

            return View("Index", flightsVM);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var flightEntity = unitOfWork.FlightRepository.GetById(id);
            if (flightEntity == null) return NotFound();

            var flight = flightEntity.Adapt<DisplayFlightVM>();
            return View("Details", flight);
        }

        // the next section is for Admins and Sellers to manage flights 

        [HttpGet]
        [Authorize(Roles = "Admin,Seller")]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Seller")]
        public IActionResult Create(AddFlightVM addFlightVM)
        {
            if (ModelState.IsValid)
            {
                var flight = addFlightVM.Adapt<Flight>();
                unitOfWork.FlightRepository.Add(flight);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View("Create", addFlightVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Seller")]
        public IActionResult Edit(int id)
        {
            var flightEntity = unitOfWork.FlightRepository.GetById(id);
            if (flightEntity == null) return NotFound();

            var flight = flightEntity.Adapt<UpdateFlightVM>();
            return View("Edit", flight);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Seller")]
        public IActionResult Edit(UpdateFlightVM updateFlightVM)
        {
            if (ModelState.IsValid)
            {
                var existingFlight = unitOfWork.FlightRepository.GetById(updateFlightVM.Id);
                if (existingFlight == null) return NotFound();

                updateFlightVM.Adapt(existingFlight);
                unitOfWork.FlightRepository.Update(existingFlight);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View("Edit", updateFlightVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Seller")]
        public IActionResult Delete(int id)
        {
            var flightEntity = unitOfWork.FlightRepository.GetById(id);
            if (flightEntity == null) return NotFound();

            var flight = flightEntity.Adapt<DisplayFlightVM>();
            return View("Delete", flight);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Seller")]
        [ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            var flightEntity = unitOfWork.FlightRepository.GetById(id);
            if (flightEntity == null) return NotFound();

            unitOfWork.FlightRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // the next section is for all authenticated users to book flights

        [HttpPost]
        [Authorize]
        public IActionResult Book(int id)
        {
            var flight = unitOfWork.FlightRepository.GetById(id);
            if (flight == null) return NotFound();

            if (flight.AvailableSeats <= 0)
            {
                TempData["Error"] = "No available seats for this flight.";
                return RedirectToAction(nameof(Details), new { id });
            }

            if (flight.DepartureDateTime <= DateTime.Now)
            {
                TempData["Error"] = "This flight has already departed and cannot be booked.";
                return RedirectToAction(nameof(Details), new { id });
            }

            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var booking = new Booking
            {
                UserId = currentUserId,
                FlightId = id,
                BookingDate = DateTime.Now,
                TotalAmount = flight.Price,
                Status = Status.Confirmed,
                Country = flight.DestinationAirport
            };

            unitOfWork.BookingRepository.Add(booking);

            flight.AvailableSeats--;
            unitOfWork.FlightRepository.Update(flight);

            unitOfWork.Save();

            TempData["Success"] = "Flight booked successfully!";
            //return RedirectToAction(nameof(Details), new { id });
            return RedirectToAction("MyBooking", "Booking");
        }
    }
}