using Car_Rental_Management.Models;
using System;
using System.Threading.Tasks;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IPaymentRepository
    {
        Task CreateAsync(Payment payment);
        Task<Payment> GetByIdAsync(Guid paymentId);
        Task<Payment> GetByBookingIdAsync(Guid bookingId);
        Task UpdateStatusAsync(Guid paymentId, string status);
    }
}
