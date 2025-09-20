using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.Models
{
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; }

        [Required]
        public Guid BookingId { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public string PaymentStatus { get; set; } = "Pending";

        [Required]
        public string PaymentMethod { get; set; } = "Card";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
