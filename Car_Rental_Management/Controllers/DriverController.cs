using Car_Rental_Management.Mapper;
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
            var drivers = await _service.GetAllDriversAsync();
            return View(drivers);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var driverDto = await _service.GetDriverByIdAsync(id);
            if (driverDto == null)
                return NotFound();


            var viewModel = DriverMapper.ToViewModel(driverDto);
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(DriverViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            await _service.UpdateDriverAsync(viewModel);
            return RedirectToAction("Index");
        }

    }
}
