using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // ---------------- PROFILE UPDATE ----------------
    [HttpGet]
    public IActionResult EditProfile()
    {
        return View();
    }

    [HttpPost]
    public IActionResult EditProfile(UserViewModel model)
    {
        var userIdString = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userIdString))
            return RedirectToAction("Login", "Account");

        var userId = Guid.Parse(userIdString);

        try
        {
            _userService.UpdateProfile((Guid)userId, model);

            ViewBag.Message = "Profile updated successfully!";
        }
        catch (Exception ex)
        {
            ViewBag.Message = ex.Message;
        }

        return View(model);
    }

    // ---------------- PASSWORD CHANGE ----------------
    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View(); // Empty form (CurrentPassword, NewPassword, ConfirmPassword)
    }

    [HttpPost]
    public IActionResult ChangePassword(UserViewModel model)
    {
        var userIdString = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userIdString))
            return RedirectToAction("Login", "Account");

        var userId = Guid.Parse(userIdString);

        try
        {
            _userService.ChangePassword(userId, model.CurrentPassword, model.NewPassword, model.ConfirmPassword);
            ViewBag.Message = "Password changed successfully!";
        }
        catch (Exception ex)
        {
            ViewBag.Message = ex.Message;
        }

        return View(model);
    }
}
