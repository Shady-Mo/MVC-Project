using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.ActivityViewModels
{
    public class DisplayActivityVM
    {
        public int Id { get; set; }
        [Display(Name = "Activity")]
        public string Name { get; set; }

        public string Location { get; set; }

        [Display(Name = "Activity Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public int Capacity { get; set; }

        public string? Img { get; set; }

        [Display(Name = "Booking Status")]
        public string? BookingReference { get; set; }
    }
}
