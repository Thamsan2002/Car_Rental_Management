using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; // Required for IFormFile

namespace Car_Rental_Management.ViewModel
{
    public class CarViewModel
    {
        [Required(ErrorMessage = "Make is required")]
        [StringLength(50, ErrorMessage = "Make cannot exceed 50 characters")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [StringLength(50, ErrorMessage = "Model cannot exceed 50 characters")]
        public string Model { get; set; }

        [Range(1900, 2099, ErrorMessage = "Year must be between 1900 and 2099")]
        public int Year { get; set; }

        [RegularExpression(@"^[A-Z0-9-]{3,10}$", ErrorMessage = "Invalid plate format")]
        [Display(Name = "Plate Number")]
        public string Plate { get; set; }

        [Range(1, 20, ErrorMessage = "Seats must be between 1 and 20")]
        public int? Seats { get; set; }

        [Required(ErrorMessage = "Transmission is required")]
        public string Transmission { get; set; }

        [Required(ErrorMessage = "Fuel type is required")]
        public string Fuel { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Mileage must be a positive number")]
        public int? Mileage { get; set; }

        [Range(0.01, 1000000, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Price Per Day")]
        public decimal? Price { get; set; }

        [Display(Name = "Available")]
        public bool Available { get; set; }

        [StringLength(30, ErrorMessage = "Color cannot exceed 30 characters")]
        public string Color { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Display(Name = "Car Images")]
        [Required(ErrorMessage = "At least one image is required")]
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();

        
    }
}
