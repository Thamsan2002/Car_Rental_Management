using Car_Rental_Management.Models;

namespace Car_Rental_Management.Service.Interface
{
    public interface IAdminLoginService
    {
        Task<User?> VerifyAdminLoginAsync(string emailOrPhone, string password);
    }
}
