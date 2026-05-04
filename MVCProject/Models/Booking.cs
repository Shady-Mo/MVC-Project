using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models {
    public class Booking {
        [Key]
        public int Id { get; set; }
        public string Country { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Status Status { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }

        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        [ForeignKey(nameof(Flight))]
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }


        public ICollection<BookingAccomodation> bookingAccomodations = new HashSet<BookingAccomodation>();
        public ICollection<BookingActivity> BookingActivities = new HashSet<BookingActivity>();
        //public ICollection<BookingFlight> BookingFlights = new HashSet<BookingFlight>();

    }

    public enum Status {
        Pending,
        Confirmed,
        Cancelled,
    }

    public enum PaymentStatus {
        Pending,
        Approved,
        Rejected
    }
}
