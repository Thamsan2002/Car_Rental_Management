using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental_Management.Models
{
    public class Booking
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Customer relationship
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }

        // Car relationship
        [ForeignKey("Car")]
        public Guid CarId { get; set; }
        public Car? Car { get; set; }

        // Optional Driver relationship
        [ForeignKey("Driver")]
        public Guid? DriverId { get; set; }  // nullable FK
        public Driver? Driver { get; set; }  // nullable navigation property

        public string BookingType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
