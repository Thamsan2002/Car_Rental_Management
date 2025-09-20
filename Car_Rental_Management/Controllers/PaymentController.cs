using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Car_Rental_Management.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IStripeService _stripeService;
        private readonly IBookingService _bookingService;

        public PaymentController(
            IPaymentService paymentService,
            IStripeService stripeService,
            IBookingService bookingService)
        {
            _paymentService = paymentService;
            _stripeService = stripeService;
            _bookingService = bookingService;
        }

        //[HttpGet]
        //public async Task<IActionResult> Index(Guid bookingId)
        //{
        //    var paymentVm = await _paymentService.GetPaymentDetailsAsync(bookingId);

        //    // ✅ Load Booking info
        //    var booking = await _bookingService.GetBookingDtoByIdAsync(bookingId);
        //    if (booking != null)
        //    {
        //        //paymentVm.CustomerName = booking.CustomerName;
        //        paymentVm.CarName = $"{booking.CarMake} {booking.CarModel}";
        //        paymentVm.TotalAmount = booking.TotalPrice;
        //    }

        //    return View(paymentVm);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay(PaymentViewModel vm)
        {
            if (!ModelState.IsValid) return View("Index", vm);

            var paymentId = await _paymentService.CreatePaymentAsync(vm);

            var url = await _stripeService.CreateCheckoutSessionAsync(
                paymentId,
                vm.TotalAmount,
                Url.Action("Success", "Payment", new { paymentId }, Request.Scheme),
                Url.Action("Failed", "Payment", new { paymentId }, Request.Scheme)
            );

            return Redirect(url);
        }

        [HttpGet]
        public async Task<IActionResult> Success(Guid paymentId)
        {
            await _paymentService.UpdatePaymentStatusAsync(paymentId, "Success");
            TempData["Message"] = "Payment Successful!";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Failed(Guid paymentId)
        {
            await _paymentService.UpdatePaymentStatusAsync(paymentId, "Failed");
            TempData["Error"] = "Payment Failed!";
            return RedirectToAction("Index", new { bookingId = paymentId });
        }
    }
}
