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

        public async Task<Staff?> GetByIdAsync(Guid id)
        {
            return await _context.Staffs.Include(s => s.User).FirstOrDefaultAsync(s => s.staffId == id);
        }

        public async Task<IEnumerable<Staff>> GetAllAsync()
        {
            return await _context.Staffs.Include(s => s.User).ToListAsync();
        }

        public async Task<Staff> AddAsync(Staff staff)
        {
            await _context.Staffs.AddAsync(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<Staff> UpdateAsync(Staff staff)
        {
            _context.Staffs.Update(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task DeleteAsync(Staff staff)
        {
            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();
        }
    }
}
