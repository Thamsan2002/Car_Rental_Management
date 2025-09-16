using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.ViewModel
{
    public class CustomerRegisterViewModel
    {
        public Guid UserId { get; set; }    
        [Required(ErrorMessage = "Full Name is required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "National Identity Card is required")]
        [Display(Name = "National Identity Card")]
        public string NationalIdentityCard { get; set; }

        [Required(ErrorMessage = "Driving License Number is required")]
        [Display(Name = "Driving License Number")]
        public string DrivingLicenseNumber { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone Number")]
        public string Phonenumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Account creation date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Account Creation Date")]
        public string AccountCreateDate { get; set; }
    }
}
