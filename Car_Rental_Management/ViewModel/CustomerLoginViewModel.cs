using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.ViewModel
{
    public class CustomerLoginViewModel
    {
        [Required(ErrorMessage = "Email or Phone is required")]
        [Display(Name = "Email or Phone")]
        public string EmailOrPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; } = false;
    }
}
