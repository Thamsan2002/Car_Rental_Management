using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; // Required for IFormFile

namespace Car_Rental_Management.ViewModel
{
    public class CarViewModel
    {
        public Guid Id { get; set; } // Add this property to fix CS1061
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Plate { get; set; }
        public int? Seats { get; set; }
        public string Transmission { get; set; }
        public string Fuel { get; set; }
        public int? Mileage { get; set; }
        public decimal? Price { get; set; }
        public bool Available { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
