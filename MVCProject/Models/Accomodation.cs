using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models
{
    public class Accomodation
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Location { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal PricePerNight { get; set; }

        [Required]
        [Range(0, 500)]
        public int AvailableRooms { get; set; }


    }
}
