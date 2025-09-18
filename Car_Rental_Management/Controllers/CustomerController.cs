using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Car_Rental_Management.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // ---------------- SIGNUP ----------------
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(CustomerSignupViewmodel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Password hashing handled inside CustomerService
            var result = await _customerService.RegisterUserAsync(model);

            if (result != "Account created successfully")
            {
                ModelState.AddModelError("", result);
                return View(model);
            }

            return RedirectToAction("Login");
        }

        // ---------------- LOGIN ----------------
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(CustomerLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Password hashing verification handled inside CustomerService
            var user = await _customerService.LoginCustomerAsync(model);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email/phone or password.");
                return View(model);
            }

            // Store essential info in Session
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserEmail", user.Email);

            return RedirectToAction("Index", "Home");
        }

        // ---------------- REGISTER DETAILED CUSTOMER INFO ----------------
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CustomerRegisterViewModel model, Guid? userId)
        {
            if (userId == null || userId == Guid.Empty)
            {
                var sessionUserId = HttpContext.Session.GetString("UserId");
                if (!string.IsNullOrWhiteSpace(sessionUserId))
                    userId = Guid.Parse(sessionUserId);
            }

            model.UserId = (Guid)userId;

            var (isSuccess, errorMessage, customerId) = await _customerService.RegisterCustomerAsync(model);

            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
                return View(model);
            }

            ViewBag.CustomerId = customerId;
            return View("Login");
        }

        // ---------------- PROFILE ----------------
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToAction("Login");

            var userId = Guid.Parse(userIdStr);

            var customer = await _customerService.GetCustomerByUserIdAsync(userId);

            if (customer == null)
                return RedirectToAction("Register");

            return View(customer);
        }
    }
}
