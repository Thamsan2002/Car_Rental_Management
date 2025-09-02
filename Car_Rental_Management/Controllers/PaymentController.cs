using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Paymentdetails()
        {
            return View();
        }
    }
}
