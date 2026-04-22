using System.ComponentModel.DataAnnotations;
using MVCProject.Models;

namespace MVCProject.ViewModels.BookingViewModel
{
    public class EditBookingVM
    {
        public int Id { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public Status Status { get; set; }

        public List<int> FlightsId { get; set; } = new List<int>();
        public List<BookingAccomodationVM> Accomodations { get; set; } = new List<BookingAccomodationVM>();
        public List<int> ActivitiesId { get; set; } = new List<int>();
    }
}