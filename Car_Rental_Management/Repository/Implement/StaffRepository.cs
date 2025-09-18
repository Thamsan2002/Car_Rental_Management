using Car_Rental_Management.Data;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Management.Repository.Implement
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ApplicationDbcontext _context;

        public StaffRepository(ApplicationDbcontext context)
        {
            _context = context;
        }

        // Add Staff to DB
        public async Task<Staff?> AddAsync(Staff staff)
        {
            await _context.Staffs.AddAsync(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        // Get staff by StaffCode
        public async Task<Staff?> GetByStaffCodeAsync(string staffCode)
        {
            return await _context.Staffs
                                 .Include(s => s.User) // Include related user info
                                 .FirstOrDefaultAsync(s => s.StaffCode == staffCode);
        }

        // Get all staff
        public async Task<List<Staff>> GetAllAsync()
        {
            return await _context.Staffs
                                 .Include(s => s.User) // Include related user info
                                 .ToListAsync();
        }

        // Get by Id
        public async Task<Staff?> GetByIdAsync(Guid id)
        {
            return await _context.Staffs
                                 .Include(s => s.User) // Include related user info
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        // Delete by Id
        public async Task DeleteByIdAsync(Guid id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff != null)
            {
                _context.Staffs.Remove(staff);
                await _context.SaveChangesAsync();
            }
        }

        // Update staff
        public async Task UpdateAsync(Staff staff)
        {
            _context.Staffs.Update(staff);
            await _context.SaveChangesAsync();
        }
        public async Task<int> GetStaffCountAsync()
        {
            return await _context.Staffs.CountAsync();
        }

    }
}
