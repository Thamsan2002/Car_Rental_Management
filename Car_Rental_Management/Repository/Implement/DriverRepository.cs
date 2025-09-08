using Car_Rental_Management.Data;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Management.Repository.Implement
{
    public class DriverRepository : IDriverRepository
    {
        private readonly ApplicationDbcontext _context;

        public DriverRepository(ApplicationDbcontext context)
        {
            _context = context;
        }

        public async Task<Driver> AddAsync(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            return await _context.Drivers.Include(d => d.User).ToListAsync();
        }

        public async Task<Driver> GetByPhoneAsync(string phone)
        {
            return await _context.Drivers.FirstOrDefaultAsync(d => d.EmergencyContact == phone);
        }
    }
}