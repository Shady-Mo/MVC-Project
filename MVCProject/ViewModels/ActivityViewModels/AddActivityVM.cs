using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.ActivityViewModels
{
    public class AddActivityVM
    {
        [Required(ErrorMessage = "Activity name is required.")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(200, MinimumLength = 3)]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please select a date.")]
        [DataType(DataType.DateTime)]
        [Remote("checkDate", "Activity")]
        [Display(Name = "Activity Date")]
        public DateTime Date { get; set; }

        [Required]
        [Range(0.01, 10000.00)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 500)]
        public int Capacity { get; set; }
    }
}
