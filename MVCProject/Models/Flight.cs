using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Flight
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Airline { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string DepartureAirport { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string DestinationAirport { get; set; }

        [Required]
        public DateTime DepartureDateTime { get; set; }

        [Required]
        public DateTime ArrivalDateTime { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 500)]
        public int AvailableSeats { get; set; }

        [ForeignKey(nameof(Booking))]
        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
