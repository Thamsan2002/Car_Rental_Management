using Car_Rental_Management.Models;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IUserRepository
    {
   
        Task<User> AddAsync(User user);
    
        Task<User> GetByEmailAndPhoneAsync(string email, string phone);
    }
}
