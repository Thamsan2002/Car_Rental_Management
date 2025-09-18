using Car_Rental_Management.Dtos;
using Car_Rental_Management.viewmodel;

namespace Car_Rental_Management.Service.Interface
{
    public interface IStaffservice
    {   // Add new staff
        Task AddStaffAsync(Staffviewmodel vm);

        // Get all staff as DTOs
        Task<List<StaffDto>> GetAllStaffAsync();

        // Get staff by ID
        Task<StaffDetailDto> GetStaffByIdAsync(Guid id);

        // Update staff
        Task UpdateStaffAsync(Guid id, Staffviewmodel vm);

        // Delete staff
        Task DeleteStaffAsync(Guid id);
    }
}
