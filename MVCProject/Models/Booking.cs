using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models {
    public class Booking {
        [Key]
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Status Status { get; set; }

        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public ICollection<Accomodation> Accomodations = new HashSet<Accomodation>();
        public ICollection<Activity> Activities = new HashSet<Activity>();
        public ICollection<Flight> Flights = new HashSet<Flight>();
    }

    public enum Status {
        Pending,
        Confirmed,
        Cancelled,
    }
}
