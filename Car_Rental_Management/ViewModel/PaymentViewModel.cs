using System;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.ViewModel
{
    public class PaymentViewModel
    {
        public Guid PaymentId { get; set; }
        public Guid BookingId { get; set; }

        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Payment Method")]
        [Required(ErrorMessage = "Select a payment method")]
        public string SelectedPaymentMethod { get; set; }

        // ✅ Booking/Customer details for display
        public string CustomerName { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }

        // ✅ Card details (only if Card selected)
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Display(Name = "CVV")]
        public string CVV { get; set; }

        [Display(Name = "Expiry Date")]
        public string ExpiryDate { get; set; }

        // ✅ Optional: Status of payment
        public string Status { get; set; }
        public string CarName { get; set; }
    }
}
