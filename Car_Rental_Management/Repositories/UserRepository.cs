using Car_Rental_Management.Data;
using Car_Rental_Management.Intrface;
using Car_Rental_Management.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Car_Rental_Management.Repositories
{
    public class UserRepository : IUserRepository
    {


        private readonly Db _context;

        public UserRepository(Db context)
        {
            _context = context;
        }

        // Add user to database
        public async Task<Guid> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }
        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.EmailAddress != null &&
                    u.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase));
        }



    }

}
