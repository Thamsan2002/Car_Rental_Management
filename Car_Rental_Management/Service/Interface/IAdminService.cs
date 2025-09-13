using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Interface
{
    public interface IAdminService
    {
        Task AddAdminAsync(Adminviewmodel vm);
    }
}
