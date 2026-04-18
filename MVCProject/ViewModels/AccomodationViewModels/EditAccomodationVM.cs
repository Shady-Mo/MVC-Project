using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.AccomodationViewModels
{
    public class EditAccomodationVM
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Activity name is required.")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(200, MinimumLength = 3)]
        public string Location { get; set; }

        [Required]
        [Range(0.01, 10000.00)]
        [DataType(DataType.Currency)]
        public decimal PricePerNight { get; set; }

        [Required]
        [Range(1, 500)]
        public int AvailableRooms { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
