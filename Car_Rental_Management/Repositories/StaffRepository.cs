using Car_Rental_Management.Data;
using Car_Rental_Management.Intrface;
using Car_Rental_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Management.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly Db _context;

        public StaffRepository(Db context)
        {
            _context = context;
        }

        // Add Staff to DB
        public async Task<Staff?> AddAsync(Staff staff)
        {
            try
            {
                _context.Staffs.Add(staff);       // Add staff model
                await _context.SaveChangesAsync(); // Save changes to DB
                return staff;                     // Return saved staff
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StaffRepository] AddAsync Error: {ex.Message}");
                return null; // fail aanaal null return pannum
            }
        }

        public async Task<Staff?> GetByStaffCodeAsync(string staffCode)
        {
            try
            {
                return await _context.Staffs
                    .FirstOrDefaultAsync(s => s.StaffCode == staffCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StaffRepository] GetByStaffCodeAsync Error: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Staff>> GetAllAsync()
        {
            try
            {
                return await _context.Staffs.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StaffRepository] GetAllAsync Error: {ex.Message}");
                return new List<Staff>(); 
            }
        }

        public async Task<Staff?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Staffs
                                     .Include(s => s.User)  
                                     .FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StaffRepository] GetByIdAsync Error: {ex.Message}");
                return null;
            }
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            try
            {
                var staff = await _context.Staffs.FindAsync(id);
                if (staff != null)
                {
                    _context.Staffs.Remove(staff);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StaffRepository] DeleteByIdAsync Error: {ex.Message}");
            }
        }

        public async Task UpdateAsync(Staff staff)
        {
            try
            {
                _context.Staffs.Update(staff);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StaffRepository] UpdateAsync Error: {ex.Message}");
            }
        }
       

    }
}
