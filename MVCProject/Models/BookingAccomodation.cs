using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class BookingAccomodation
    {
        public int Id { get; set; }


        [ForeignKey(nameof(Booking))]
        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }

        [ForeignKey(nameof(Accomodation))]
        public int AccomodationId { get; set; }
        public virtual Accomodation Accomodation { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
