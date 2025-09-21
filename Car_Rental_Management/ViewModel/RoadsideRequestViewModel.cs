using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.ViewModel
{
    public class RoadsideRequestViewModel
    {
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "Car number required")]
        public string CarNumber { get; set; }

        [Required(ErrorMessage = "Car make/model required")]
        public string CarModel { get; set; }

        public double Latitude { get; set; } // hidden in form
        public double Longitude { get; set; } // hidden in form

        [StringLength(250)]
        public string? Notes { get; set; }
    }
}
