using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Areas.Admin.Controllers
{
    public class DriverController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
