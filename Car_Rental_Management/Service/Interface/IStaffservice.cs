using Car_Rental_Management.Dtos;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Interface
{
    public interface IStaffservice
    {
        Task<IEnumerable<StaffDto>> GetAllAsync();
        Task<StaffViewModel?> GetStaffByIdAsync(Guid id);
        Task AddStaffAsync(StaffViewModel vm);
        Task UpdateStaffAsync(Guid id, StaffViewModel vm);
        Task DeleteStaffAsync(Guid id);


    }
}
