using Car_Rental_Management.Dtos;

namespace Car_Rental_Management.Service.Interface
{
    public interface IAdminDashBoardService
    {
        Task<AdminDashBoardDto> GetDashboardSummaryAsync();
    }
}
