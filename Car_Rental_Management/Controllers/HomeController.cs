using Car_Rental_Management.Models;
using Car_Rental_Management.Service.Interface;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            var cars = await _carService.GetAllCarsAsync(); // CarService call
            return View(cars); // Index view ku list pass pannuthu
        }
        public IActionResult About()
        {
            return View();
        }

       
        public IActionResult Contact()
        {
            return View();
        }

    }
}
