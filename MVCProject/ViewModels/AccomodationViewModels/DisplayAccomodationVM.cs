using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.AccomodationViewModels
{
    public class DisplayAccomodationVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Location { get; set; }

        [DataType(DataType.Currency)]
        public decimal PricePerNight { get; set; }
        public int AvailableRooms { get; set; }

        public string SellerName { get; set; }
        public string? Image { get; set; }
    }
}
