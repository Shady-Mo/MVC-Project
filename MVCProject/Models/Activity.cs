using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [Range(0.01, 10000.00)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 500)]
        public int Capacity { get; set; }


        [ForeignKey(nameof(Booking))]
        public int? BookingId { get; set; }
        public virtual Booking? Booking { get; set; }
    }
}
