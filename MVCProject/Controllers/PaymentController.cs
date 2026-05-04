using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using MVCProject.Repositories;
using Stripe.Checkout;

namespace MVCProject.Controllers {
    [Authorize]
    public class PaymentController : Controller {

        private readonly UnitOfWork unitOfWork;

        public PaymentController(UnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public IActionResult Checkout(int id) {
            var booking = unitOfWork.BookingRepository.GetByIdIncluded(id);
            if (booking == null || booking.Status != Status.Pending) {
                return NotFound();
            }

            var service = new SessionService();
            Session session = service.Get(booking.SessionId);

            return Redirect(session.Url);
        }

        [HttpGet]
        public IActionResult ConfirmPayment(int bookingId) {
            var booking = unitOfWork.BookingRepository.GetByIdIncluded(bookingId);

            if (booking == null)
                return NotFound();

            var service = new SessionService();
            Session session = service.Get(booking.SessionId);

            if (session.PaymentStatus.ToLower() == "paid") {
                booking.PaymentStatus = PaymentStatus.Approved;
                booking.PaymentIntentId = session.PaymentIntentId;
                booking.Status = Status.Confirmed;
                booking.PaymentDate = DateTime.Now;

                var flight = unitOfWork.FlightRepository.GetById(booking.FlightId);
                flight.AvailableSeats -= 1;

                if (booking.bookingAccomodations != null) {
                    foreach (var accItem in booking.bookingAccomodations) {
                        var acc = unitOfWork.AccomodationRepositroy.GetById(accItem.AccomodationId);
                        if (acc != null) acc.AvailableRooms -= 1;
                    }
                }

                if (booking.BookingActivities != null) {
                    foreach (var actItem in booking.BookingActivities) {
                        var act = unitOfWork.ActivityRepository.GetById(actItem.ActivityId);
                        if (act != null) act.Capacity -= 1;
                    }
                }

                unitOfWork.Save();
            }

            return View(bookingId);
        }
    }
}
