using Car_Rental_Management.Dtos;
using Car_Rental_Management.Service.Implement;
using Car_Rental_Management.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService _carService;
        private readonly IBookingService _bookingService;

        public HomeController(ICarService carService , IBookingService bookingService)
        {
            _carService = carService;
            _bookingService = bookingService;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllCarsAsync();
            var availableCars = cars.Where(c => c.Available).ToList();

            // Booking stats eduthu pricing adjust panna
            var stats = await _bookingService.GetHourlyBookingStatsAsync();
            int currentHour = DateTime.Now.Hour;

            bool isPeakHour = stats.ContainsKey(currentHour) && stats[currentHour] > 5; // >5 bookings = peak hour

            foreach (var car in availableCars)
            {
                if (isPeakHour)
                    car.Price = car.Price.HasValue ? car.Price.Value * 1.2M : car.Price; // +20% peak
                else
                    car.Price = car.Price.HasValue ? car.Price.Value * 0.9M : car.Price; // -10% off-peak
            }

            // Session UserId
            var userIdStr = HttpContext.Session.GetString("UserId");
            Guid? userId = null;
            if (!string.IsNullOrEmpty(userIdStr) && Guid.TryParse(userIdStr, out Guid parsedId))
            {
                userId = parsedId;
            }

            ViewBag.UserId = userId;
            return View(availableCars);
        }


        public IActionResult Privacy()
        {
            return View();
        }
    }
}
