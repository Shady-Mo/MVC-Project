using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class BookingActivity
    {
        public int Id { get; set; }


        [ForeignKey(nameof(Booking))]
        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }

        [ForeignKey(nameof(Activity))]
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }

    }
}
