using Car_Rental_Management.Data;
using Car_Rental_Management.Intrface;
using Car_Rental_Management.Models;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user.Id; // always returns Guid
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository] AddAsync Error: {ex.Message}");
                throw; // throw exception instead of returning null
            }
        }


        public async Task<User?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Users.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository] GetByIdAsync Error: {ex.Message}");
                return null;
            }
        }

        public async Task UpdateAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository] UpdateAsync Error: {ex.Message}");
            }
        }

        public async Task DeleteAsync(User user)
        {
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository] DeleteAsync Error: {ex.Message}");
            }
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _context.Users
                    .FirstOrDefaultAsync(u =>
                        u.EmailAddress != null &&
                        u.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository] GetUserByEmailAsync Error: {ex.Message}");
                return null;
            }
        }
    }
}
