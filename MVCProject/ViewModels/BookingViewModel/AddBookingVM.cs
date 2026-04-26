using MVCProject.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.BookingViewModel
{
    public class AddBookingVM
    {
        [Required]
        [ValidCountry]
        public string Country { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        public int FlightId { get; set; }
        public List<BookingAccomodationVM>? Accomodations { get; set; }
        public List<int>? ActivitiesId { get; set; }
    }

    public class BookingAccomodationVM
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
