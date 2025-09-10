using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.viewmodel
{
    public class ReviewViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Description { get; set; } = string.Empty;

        [Required, Range(1, 5)]
        public int Rating { get; set; }
    }
}
