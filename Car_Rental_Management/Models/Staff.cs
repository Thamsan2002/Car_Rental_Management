using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.Models
{
    public class Staff
    {
        [Key]
        public Guid staffId { get; set; } = Guid.NewGuid();

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

        public string Role { get; set; } = "Staff";

        // FK
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
