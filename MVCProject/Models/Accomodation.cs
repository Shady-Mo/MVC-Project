using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Accomodation
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal PricePerNight { get; set; }
        public int AvailableRooms { get; set; }

        public ICollection<BookingAccomodation> bookingAccomodations = new HashSet<BookingAccomodation>();

    }
}
