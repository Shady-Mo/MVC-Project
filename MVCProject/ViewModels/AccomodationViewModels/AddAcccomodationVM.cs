using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.AccomodationViewModels
{
    public class AddAcccomodationVM
    {
        [Required(ErrorMessage = "Activity name is required.")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(200, MinimumLength = 3)]
        public string Location { get; set; }
        
        [Required]
        [Range(0.01, 10000.00)]
        [DataType(DataType.Currency)]
        [Display(Name ="Price per night")]
        public decimal PricePerNight { get; set; }

        [Required]
        [Range(1, 500)]
        [Display(Name = "Number of rooms")]
        public int AvailableRooms { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
