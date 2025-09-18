using System;
using System.Threading.Tasks;

namespace Car_Rental_Management.Service.Interface
{
    public interface IStripeService
    {
        Task<string> CreateCheckoutSessionAsync(Guid paymentId, decimal amount, string successUrl, string cancelUrl);
    }
}
