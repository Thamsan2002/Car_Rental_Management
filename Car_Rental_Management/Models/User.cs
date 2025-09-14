using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        // Navigation
        public Driver? Driver { get; set; }
        public Staff? Staff { get; set; }
    }
}
