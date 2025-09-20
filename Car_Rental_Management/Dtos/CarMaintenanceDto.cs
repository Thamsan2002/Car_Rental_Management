using System;

namespace Car_Rental_Management.Dtos
{
    public class CarMaintenanceDto
    {
        public Guid MaintenanceId { get; set; }
        public Guid CarId { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Cost { get; set; }
        public bool IsReturned { get; set; }
    }
}
