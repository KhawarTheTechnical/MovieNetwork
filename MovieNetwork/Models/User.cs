// filepath: c:\Users\KhawarSaeed\source\repos\MovieNetwork\MovieNetwork\Models\User.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieNetwork.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [MaxLength(500)]
        public string Interests { get; set; }

        public List<int> Watchlist { get; set; } = new List<int>();

        public List<int> Favorites { get; set; } = new List<int>();
    }
}