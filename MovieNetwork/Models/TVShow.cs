using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieNetwork.Models
{
    public class TVShow
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required, MaxLength(50)]
        public string Genre { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Range(0, 10)]
        public double Rating { get; set; }

        [Range(1, int.MaxValue)]
        public int Seasons { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}