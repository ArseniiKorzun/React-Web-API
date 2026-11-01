using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(500)]
        public string PhotoPath { get; set; }

        [Required]
        [MaxLength(100)]
        public string Director { get; set; }

        public int Runtime { get; set; }
    }
}
