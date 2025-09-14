using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class AddCarController : Controller
    {
        private readonly ICarService _carService;

        public AddCarController(ICarService carService)
        {
            _carService = carService;
        }
        public IActionResult Details()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(CarViewModel carViewModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(carViewModel); // Re-render form with validation errors
            //}

            var result = await _carService.AddCarAsync(carViewModel);

            if (result)
            {
                TempData["SuccessMessage"] = "Car added successfully!";
                return RedirectToAction("AddCar"); // or wherever you want
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong while saving the car.");
                return View(carViewModel);
            }
        }
    }
}
