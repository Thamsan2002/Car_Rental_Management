using Car_Rental_Management.Models;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IAdminrepository
    {
        Task AddAdminAsync(Admin admin);
        Task<Admin?> GetByIdAsync(Guid id);
        Task UpdateAdminAsync(Admin admin);
        Task<List<Admin>> GetAllAsync();
        Task DeleteAdminAsync(Guid id);
    }
}
