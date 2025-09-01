using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
