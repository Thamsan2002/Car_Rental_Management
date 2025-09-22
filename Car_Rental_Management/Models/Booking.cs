using Car_Rental_Management.Models;
using System.ComponentModel.DataAnnotations.Schema;

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

    public required string BookingType { get; set; }  // required keyword fixes null warning
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Status { get; set; } 

}
