using Car_Rental_Management.ViewModel;
using System;
using System.Threading.Tasks;

namespace Car_Rental_Management.Service.Interface
{
    public interface IPaymentService
    {
        Task<Guid> CreatePaymentAsync(PaymentViewModel vm);
        Task<PaymentViewModel> GetPaymentDetailsAsync(Guid bookingId);
        Task<PaymentViewModel> GetPaymentByIdAsync(Guid paymentId);
        Task UpdatePaymentStatusAsync(Guid paymentId, string status);
    }
}
