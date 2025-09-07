using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class DriverController : Controller
    {

        private readonly IDriverService _service;

        public DriverController(IDriverService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult CreateDriver()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateDriver(DriverViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            await _service.CreateDriverAsync(viewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
