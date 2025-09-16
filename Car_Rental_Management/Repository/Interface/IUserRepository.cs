using Car_Rental_Management.Models;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IUserRepository
    {

        Task<Guid> AddAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task UpdateAsync(User user);
        Task<bool> IsEmailOrPhoneExistAsync(string email, string phone);
        Task<User?> GetByPhoneAsync(string phoneNumber);
        Task<User?> GetCustomerByLoginAsync(string emailOrPhone, string password);




    }
}
