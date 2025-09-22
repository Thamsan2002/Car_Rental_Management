using Car_Rental_Management.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class AdminDashBoardController : Controller
    {
        private readonly IAdminDashBoardService _dashboardService;

        // Constructor Injection
        public AdminDashBoardController(IAdminDashBoardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        // GET: AdminDashboard/Index
        public async Task<IActionResult> Index()
        {
            var summary = await _dashboardService.GetDashboardSummaryAsync();
            return View(summary);
        }
    }
}
