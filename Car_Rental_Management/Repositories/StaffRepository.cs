using Car_Rental_Management.Data;
using Car_Rental_Management.Intrface;
using Car_Rental_Management.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Car_Rental_Management.Repositories
{
    public class StaffRepository:IStaffRepository
    {
        private readonly Db _context;

        public StaffRepository(Db context)
        {
            _context = context;
        }

        // Add Staff to DB
        public async Task<Staff> AddAsync(Staff staff)
        {
            _context.Staffs.Add(staff);       // Add staff model
            await _context.SaveChangesAsync(); // Save changes to DB
            return staff;                     // Return saved staff
        }
        public async Task<Staff?> GetByStaffCodeAsync(string staffCode)
        {
            return await _context.Staffs
                .FirstOrDefaultAsync(s => s.StaffCode == staffCode);
        }
        public async Task<List<Staff>> GetAllAsync()
        {
            return await _context.Staffs.ToListAsync();
        }

        public async Task<Staff?> GetByIdAsync(Guid id)
        {
            return await _context.Staffs.FindAsync(id);
        }
        public async Task DeleteByIdAsync(Guid id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff != null)
            {
                _context.Staffs.Remove(staff);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(Staff staff)
        {
            _context.Staffs.Update(staff);
            await _context.SaveChangesAsync();
        }



    }
}
