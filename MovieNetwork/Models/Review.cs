using System.ComponentModel.DataAnnotations;

namespace MovieNetwork.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ContentId { get; set; }

        [Range(0, 10)]
        public double Rating { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }

        public User User { get; set; }
    }
}