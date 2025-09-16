using Car_Rental_Management.Models;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IUserRepository
    {

        Task<Guid> AddAsync(User user);
        //Task<User?> GetByIdAsync(Guid id);
        Task UpdateAsync(User user);
        Task<bool> IsEmailOrPhoneExistAsync(string email, string phone);
        //Task<User> GetByEmailAndPhoneAsync(string email, string phone);
        Task<User?> GetByEmailOrPhoneAsync(string emailOrPhone, string password);




    }
}
