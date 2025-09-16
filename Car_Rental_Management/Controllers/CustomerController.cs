using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(CustomerSignupViewmodel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _customerService.RegisterUserAsync(model);

            if (result != "Account created successfully")
            {
                // Show error message (duplicate email/phone)

                ModelState.AddModelError("", result);
                return View(model);
            }

            // Success → Redirect to Login
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(CustomerLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _customerService.LoginCustomerAsync(model);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email/phone or password.");
                return View(model);
            }

            // Store user temporarily
            TempData["FullName"] = user;
            // ✅ Success → Redirect to Home/Index
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CustomerRegisterViewModel model)
        {
            model.UserId = Guid.NewGuid(); // Assign a new GUID for UserId
            //if (!ModelState.IsValid)
            //    return View(model);

            var (isSuccess, errorMessage, customerId) = await _customerService.RegisterCustomerAsync(model);

            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
                return View(model);
            }

            // Registration successful, pass CustomerId to view
            ViewBag.CustomerId = customerId;
            return View("Login");
        }
    }
}
