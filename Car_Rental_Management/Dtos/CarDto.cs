namespace Car_Rental_Management.Dtos
{
    public class CarDto
    {

        public Guid Id { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public string Plate { get; set; }
        public int? Seats { get; set; }

        public string Transmission { get; set; }  // Auto / Manual
        public string Fuel { get; set; }          // Petrol / Diesel / Hybrid / Electric

        public int? Mileage { get; set; }
        public decimal? Price { get; set; }

        public bool Available { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }

        // Image paths (already uploaded & stored in DB)
        public List<string>? ImageUrls { get; set; }

        // Optional: include created/updated timestamps
        public DateTime CreatedAt { get; set; }
    }
}
