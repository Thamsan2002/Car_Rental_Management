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
        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }


    }
}