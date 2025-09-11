using Car_Rental_Management.Mapper;
using Car_Rental_Management.Service.Implement;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Car_Rental_Management.Controllers
{
    [Route("staff")]
    public class StaffController : Controller
    {
        private readonly IStaffservice _staffService;

        public StaffController(IStaffservice staffService)
        {
            _staffService = staffService;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index() => View(await _staffService.GetAllAsync());

        [HttpGet("create")]
        public IActionResult Create() => View();

        [HttpPost("create")]
        public async Task<IActionResult> Create(StaffViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _staffService.AddStaffAsync(vm);
            return RedirectToAction("Index");
        }

        // Edit, Delete same structure
    }
}

// DriverController and CustomerController same structure
