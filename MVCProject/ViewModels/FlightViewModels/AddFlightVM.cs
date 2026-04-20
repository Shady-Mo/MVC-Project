using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.FlightViewModels
{
    public class AddFlightVM
    {
        [Required]
        public string Airline { get; set; }

        [Required]
        public string DepartureAirport { get; set; }

        [Required]
        public string DestinationAirport { get; set; }

        [Required]
        public DateTime DepartureDateTime { get; set; }

        [Required]
        public DateTime ArrivalDateTime { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be positive")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Must have at least 1 seat")]
        public int AvailableSeats { get; set; }
    }
}