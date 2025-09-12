using Car_Rental_Management.ViewModel;
using System.Threading.Tasks;

namespace Car_Rental_Management.Service.Interface
{
    public interface ICarService
    {
        Task<bool> AddCarAsync(CarViewModel model);
    }
}
