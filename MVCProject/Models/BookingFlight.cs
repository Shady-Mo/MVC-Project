using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class BookingFlight
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Booking))]
        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }

        [ForeignKey(nameof(Flight))]
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }
    }
}
