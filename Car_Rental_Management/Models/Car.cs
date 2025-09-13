namespace Car_Rental_Management.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string PlateNumber { get; set; }
        public int? Seats { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }
        public int? Mileage { get; set; }
        public decimal? PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string Features { get; set; } // Comma-separated list
        public List<string> ImagePaths { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
