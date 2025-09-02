using Car_Rental_Management.Models;

namespace Car_Rental_Management.Intrface
{
    public interface IStaffRepository
    {
        Task<Staff> AddAsync(Staff staff);
    }
}
