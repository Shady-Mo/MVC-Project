using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models
{
    public class Activity
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
        public DateTime Date { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 500)]
        public int Capacity { get; set; }

    }
}
