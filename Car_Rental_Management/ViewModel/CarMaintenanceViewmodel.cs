using Car_Rental_Management.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.ViewModel
{
    public class CarMaintenanceViewmodel
    {
        [Required]
        public Guid CarId { get; set; }

        [Required]
        public string MaintenanceType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime MaintenanceDate { get; set; }

        public string Notes { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }

        public bool IsReturned { get; set; }

        // For dropdown display
        public IEnumerable<CarDto> AvailableCars { get; set; }
        public string CarModel { get; set; }
    }
}
