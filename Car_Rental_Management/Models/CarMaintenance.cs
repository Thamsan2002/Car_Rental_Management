namespace Car_Rental_Management.Models
{
    public class CarMaintenance
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public Car Car { get; set; }

        public string Description { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Notes { get; set; }       
        public decimal Cost { get; set; }       
        public bool IsReturned { get; set; }
    }
}
