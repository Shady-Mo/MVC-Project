using MVCProject.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.BookingViewModel
{
    public class AddBookingVM
    {
        [Required]
        [ValidCountry]
        public string Country { get; set; }
        public DateTime BookingDate { get; set; }
        public string UserId { get; set; }

        public List<int> FlightsId { get; set; }
        public List<int> AccomodationsId { get; set; }
        public List<int> ActivitiesId { get; set; }
    }
}
