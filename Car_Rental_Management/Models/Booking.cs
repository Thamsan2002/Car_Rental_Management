using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental_Management.Models
{
    public class Booking
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Car")]
        public Guid CarId { get; set; }
        public Car car { get; set; }
        public string BookingType { get; set; } 
        public Guid? DriverId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
