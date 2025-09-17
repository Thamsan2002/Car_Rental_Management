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

        public async Task<List<Driver>> GetAllAsync()
        {
            return await _context.Drivers.Include(d => d.User).ToListAsync();
        }

        public async Task<Driver?> GetByIdAsync(Guid id)
        {
            return await _context.Drivers.Include(d => d.User).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task UpdateAsync(Driver driver)
        {
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Driver driver)
        {
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
        }
        public async Task<int> GetDriverCountAsync()
        {
            return await _context.Drivers.CountAsync();
        }


    }
}
