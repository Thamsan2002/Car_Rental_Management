using Car_Rental_Management.Service.Implement;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            await _service.CreateCustomerAsync(viewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
