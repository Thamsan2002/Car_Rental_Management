using Car_Rental_Management.Dtos;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;

public class RoadsideController : Controller
{
    private readonly IRoadsideRequestService _service;
    private readonly IBookingService _bookingService;

    public RoadsideController(IRoadsideRequestService service, IBookingService bookingService)
    {
        _service = service;
        _bookingService = bookingService;
    }

    [HttpGet]
    public async Task<IActionResult> RequestHelp()
    {
        var customerIdStr = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(customerIdStr))
        {
            TempData["Error"] = "⚠️ Please login first.";
            return RedirectToAction("Login", "Customer");
        }
        var customerId = Guid.Parse(customerIdStr);

        var booking = await _bookingService.GetActiveBookingAsync(customerId);
        if (booking == null)
        {
            TempData["Error"] = "❌ No active booking found.";
            return RedirectToAction("Index", "Home");
        }

        var vm = new RoadsideRequestViewModel
        {
            CarId = booking.CarId,
            CarModel = booking.Car?.Model,
            CarNumberPlate = booking.Car?.PlateNumber
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> RequestHelp(RoadsideRequestViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var customerIdStr = HttpContext.Session.GetString("CustomerId");
        if (string.IsNullOrEmpty(customerIdStr))
        {
            TempData["Error"] = "⚠️ Login required.";
            return RedirectToAction("Signup", "Customer");
        }
        var customerId = Guid.Parse(customerIdStr);

        var dto = new RoadsideRequestDto
        {
            CustomerId = customerId,
            CarId = vm.CarId,
            Latitude = vm.Latitude,
            Longitude = vm.Longitude,
            Notes = vm.Notes
        };

        await _service.CreateRequestAsync(dto);

        TempData["Success"] = "✅ Request sent successfully!";
        return RedirectToAction("Dashboard", "Customer");
    }
}
