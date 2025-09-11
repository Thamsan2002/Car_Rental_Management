using Car_Rental_Management.Models;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IStaffRepository
    {
        Task<Staff?> GetByIdAsync(Guid id);
        Task<IEnumerable<Staff>> GetAllAsync();
        Task<Staff> AddAsync(Staff staff);
        Task<Staff> UpdateAsync(Staff staff);
        Task DeleteAsync(Staff staff);
    }
}
