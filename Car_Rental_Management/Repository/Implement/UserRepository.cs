using Car_Rental_Management.Data;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Management.Repository.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbcontext _context;

        public UserRepository(ApplicationDbcontext context)
        {
            _context = context;
        }
        public async Task<Guid> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.userId;
        }


        public async Task<bool> IsEmailOrPhoneExistAsync(string email, string phone)
        {
            return await _context.Users
                .AnyAsync(u => (u.Email == email && u.PhoneNumber == phone) || u.Email == email || u.PhoneNumber == phone);
        }


    }
}