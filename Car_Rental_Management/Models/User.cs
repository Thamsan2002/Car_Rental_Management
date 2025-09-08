
﻿using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.Models
{
    public class User
    {

        [Key]
        public Guid userId { get; set; } = Guid.NewGuid();

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Role { get; set; }
        public Driver Driver { get; set; }
    }
}