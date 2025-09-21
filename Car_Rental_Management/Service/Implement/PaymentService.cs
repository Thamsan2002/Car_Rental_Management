using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using System;
using System.Threading.Tasks;

namespace Car_Rental_Management.Service.Implement
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repo;

        public PaymentService(IPaymentRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> CreatePaymentAsync(PaymentViewModel vm)
        {
            var payment = new Payment
            {
                PaymentId = Guid.NewGuid(),
                BookingId = vm.BookingId,
                TotalAmount = vm.TotalAmount,
                PaymentMethod = "Card", // Stripe
                PaymentStatus = "Pending",
                CreatedAt = DateTime.UtcNow
            };
            await _repo.CreateAsync(payment);
            return payment.PaymentId;
        }

        public async Task<PaymentViewModel> GetPaymentByIdAsync(Guid paymentId)
        {
            var payment = await _repo.GetByIdAsync(paymentId);
            if (payment == null) return null;

            return new PaymentViewModel
            {
                PaymentId = payment.PaymentId,
                BookingId = payment.BookingId,
                TotalAmount = payment.TotalAmount,
                Status = payment.PaymentStatus,
                SelectedPaymentMethod = payment.PaymentMethod
            };
        }

        public async Task<PaymentViewModel> GetPaymentDetailsAsync(Guid bookingId)
        {
            var payment = await _repo.GetByBookingIdAsync(bookingId);

            if (payment == null)
            {
                payment = new Payment
                {
                    PaymentId = Guid.NewGuid(),
                    BookingId = bookingId,
                    TotalAmount = 0,
                    PaymentStatus = "Pending",
                    PaymentMethod = "Card",
                    CreatedAt = DateTime.UtcNow
                };
                await _repo.CreateAsync(payment);
            }

            return new PaymentViewModel
            {
                PaymentId = payment.PaymentId,
                BookingId = payment.BookingId,
                TotalAmount = payment.TotalAmount,
                Status = payment.PaymentStatus,
                SelectedPaymentMethod = payment.PaymentMethod
            };
        }

        public async Task UpdatePaymentStatusAsync(Guid paymentId, string status)
        {
            await _repo.UpdateStatusAsync(paymentId, status);
        }
    }
}
