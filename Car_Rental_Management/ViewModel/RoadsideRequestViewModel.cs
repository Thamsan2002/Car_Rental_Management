using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.ViewModel
{
    public class RoadsideRequestViewModel
        {

        public Guid CarId { get; set; }

        [Display(Name = "Car Model")]
        public string? CarModel { get; set; }

        [Display(Name = "Number Plate")]
        public string? CarNumberPlate { get; set; }

        [Required]
        [Display(Name = "Notes")]
        [MaxLength(500)]
        public string? Notes { get; set; }

        // Hidden for geolocation
        public double Latitude { get; set; }
        public double Longitude { get; set; }
       
         
         
    }
    

}
