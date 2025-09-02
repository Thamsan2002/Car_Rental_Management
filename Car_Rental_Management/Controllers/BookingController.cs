using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Booking()
        {
            return View();
        }
    }
}
