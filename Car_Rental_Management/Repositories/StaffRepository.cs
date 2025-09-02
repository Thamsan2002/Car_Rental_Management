using Car_Rental_Management.Data;
using Car_Rental_Management.Intrface;
using Car_Rental_Management.Models;
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

    }
}
