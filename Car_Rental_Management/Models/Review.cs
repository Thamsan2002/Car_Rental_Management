using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        public Guid UserId { get; set; } // FK to User

        [Required, StringLength(100)]
        public string Description { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public User User { get; set; } = null!;
    }
}
