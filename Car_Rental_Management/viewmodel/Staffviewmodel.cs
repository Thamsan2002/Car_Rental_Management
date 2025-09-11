using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.ViewModel
{
    public class StaffViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Active";

        public string ProfileImage { get; set; } = string.Empty;

        [Range(1000, double.MaxValue)]
        public int Salary { get; set; }

        [Required]
        public TimeSpan ShiftTime { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required, Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}