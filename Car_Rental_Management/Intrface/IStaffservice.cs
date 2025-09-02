using Car_Rental_Management.Dtos;
using Car_Rental_Management.viewmodel;

namespace Car_Rental_Management.Intrface
{
    public interface IStaffservice
    {
        Task AddStaffAsync(Staffviewmodel vm);
    }
}
