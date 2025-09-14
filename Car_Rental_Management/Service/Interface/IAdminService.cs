using Car_Rental_Management.Dtos;
using Car_Rental_Management.ViewModel;
using System.Collections.Generic;

namespace Car_Rental_Management.Service.Interface
{
    public interface IAdminService
    {
        Task AddAdminAsync(Adminviewmodel vm);
        Task UpdateAdminAsync(Guid adminId, Adminviewmodel vm);
        Task<List<AdminDto>> GetAllAdminsAsync();
        Task<AdminDto> GetAdminByIdAsync(Guid id);


        Task DeleteAdminAsync(Guid Id);

    }
}
