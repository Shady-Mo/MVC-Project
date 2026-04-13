using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models {
    public class Booking {
        public string Id { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Status Status { get; set; }

        [ForeignKey(nameof(AppUser))]
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }
    }

    public enum Status {
        Pending,
        Confirmed,
        Cancelled,
    }
}
