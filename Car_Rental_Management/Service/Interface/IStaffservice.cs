using Car_Rental_Management.Dtos;
using Car_Rental_Management.viewmodel;

namespace Car_Rental_Management.Service.Interface
{
    public interface IStaffservice
    {
        Task AddStaffAsync(Staffviewmodel vm);
        // Get all staff for list view
        Task<List<StaffDto>> GetAllStaffAsync();

        // Get full details of one staff
        Task<StaffDetailDto> GetStaffByIdAsync(Guid id);

        // Optional: Delete staff
        Task DeleteStaffAsync(Guid id);

        // Optional: Update staff
        Task UpdateStaffAsync(Guid id, Staffviewmodel vm);
    }
}
