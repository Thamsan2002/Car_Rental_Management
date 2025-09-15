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

        public async Task<IActionResult> Details()
        {
            var cars = await _carService.GetAllCarsAsync();
            return View(cars);
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(CarViewModel carViewModel)
        {
            if (!ModelState.IsValid)
                return View(carViewModel);

            var result = await _carService.AddCarAsync(carViewModel);

            if (result)
            {
                TempData["SuccessMessage"] = "Car added successfully!";
                return RedirectToAction("Details");
            }

            ModelState.AddModelError("", "Something went wrong while saving the car.");
            return View(carViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null) return NotFound();

            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarViewModel carViewModel)
        {
            if (!ModelState.IsValid)
                return View(carViewModel);

            var result = await _carService.UpdateCarAsync(carViewModel);

            if (result)
            {
                TempData["SuccessMessage"] = "Car updated successfully!";
                return RedirectToAction("Details");
            }

            ModelState.AddModelError("", "Something went wrong while updating the car.");
            return View(carViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _carService.DeleteCarAsync(id);

            if (result)
            {
                TempData["SuccessMessage"] = "Car deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete car.";
            }

            return RedirectToAction("Details");
        }
    }
}
