using Car_Rental_Management.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IBookingService _bookingService;

        public ReportsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<IActionResult> PeakHours()
        {
            var stats = await _bookingService.GetHourlyBookingStatsAsync();
            return View(stats);
        }
    }
}
