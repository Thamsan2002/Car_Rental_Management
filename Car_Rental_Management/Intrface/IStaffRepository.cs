using Car_Rental_Management.Models;

namespace Car_Rental_Management.Intrface
{
    public interface IStaffRepository
    {
        Task<Staff?> AddAsync(Staff staff);

        Task<Staff?> GetByStaffCodeAsync(string staffCode);
        Task<List<Staff>> GetAllAsync();
        Task<Staff?> GetByIdAsync(Guid id);
        Task DeleteByIdAsync(Guid id);
        Task UpdateAsync(Staff staff);
      
    }
}
