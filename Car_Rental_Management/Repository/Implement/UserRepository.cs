using Car_Rental_Management.Data;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;

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
            return user.Id;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsEmailOrPhoneExistAsync(string email, string phone)
        {
            return await _context.Users.AnyAsync(u => u.Email == email || u.PhoneNumber == phone);
        }

        public async Task<User?> GetByPhoneAsync(string phoneNumber)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

        public async Task<User?> GetByEmailOrPhoneAsync(string emailOrPhone)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == emailOrPhone || u.PhoneNumber == emailOrPhone);
        }

        public async Task<User?> GetCustomerByLoginAsync(string emailOrPhone)
        {
            return await _context.Users
                .Where(u => u.Role == "Customer" &&
                            (u.Email == emailOrPhone || u.PhoneNumber == emailOrPhone))
                .FirstOrDefaultAsync();
        }
        // Fetch user by Id
        public User? GetById(Guid userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        // Update user details (email, phone, password)
        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public async Task<User?> GetByRoleAsync(string role)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Role == role);
        }
    }
}
