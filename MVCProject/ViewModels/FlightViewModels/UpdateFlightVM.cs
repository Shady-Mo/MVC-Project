//using System.ComponentModel.DataAnnotations;

//namespace MVCProject.ViewModels.FlightViewModels
//{
//    public class UpdateFlightVM
//    {
//        public int Id { get; set; }

//        [Required]
//        public string Airline { get; set; }

//        [Required]
//        public string DepartureAirport { get; set; }

//        [Required]
//        public string DestinationAirport { get; set; }

//        [Required]
//        public DateTime DepartureDateTime { get; set; }

//        [Required]
//        public DateTime ArrivalDateTime { get; set; }

//        [Required]
//        [Range(0, double.MaxValue, ErrorMessage = "Price must be positive")]
//        public decimal Price { get; set; }

//        [Required]
//        [Range(1, int.MaxValue, ErrorMessage = "Must have at least 1 seat")]
//        public int AvailableSeats { get; set; }
//    }
//}


using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.FlightViewModels
{
    public class UpdateFlightVM : IValidatableObject
    {
        public int Id { get; set; }

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
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Must have at least 1 seat")]
        public int AvailableSeats { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DepartureDateTime <= DateTime.Now)
                yield return new ValidationResult(
                    "Departure must be in the future.",
                    new[] { nameof(DepartureDateTime) });

            if (ArrivalDateTime <= DepartureDateTime)
                yield return new ValidationResult(
                    "Arrival must be after departure.",
                    new[] { nameof(ArrivalDateTime) });
        }
    }
}