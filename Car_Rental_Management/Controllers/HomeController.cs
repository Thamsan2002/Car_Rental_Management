using Car_Rental_Management.Dtos;
using Car_Rental_Management.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService _carService;

        public HomeController(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            // Get all cars from service
            var cars = await _carService.GetAllCarsAsync();

            // ✅ Filter only available cars
            var availableCars = cars.Where(c => c.Available).ToList();

            // Get userId from session
            var userIdStr = HttpContext.Session.GetString("UserId");
            Guid? userId = null;
            if (!string.IsNullOrEmpty(userIdStr) && Guid.TryParse(userIdStr, out Guid parsedId))
            {
                userId = parsedId;
            }

            // Pass to view
            ViewBag.UserId = userId;

            return View(availableCars);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
