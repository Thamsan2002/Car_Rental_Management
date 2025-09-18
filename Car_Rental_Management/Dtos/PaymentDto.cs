namespace Car_Rental_Management.Dtos
{
    public class PaymentDto
    {
        public Guid PaymentId { get; set; }
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }  // Stripe / PayHere
        public string Status { get; set; }  // Pending / Success / Failed
        public DateTime CreatedAt { get; set; }
    }
}
