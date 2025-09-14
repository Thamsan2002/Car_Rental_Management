using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.ViewModel
{
    public class CustomerViewModel
    {
        [Required(ErrorMessage = "Please enter your name.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Please select your gender.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "NIC is required.")]
        [RegularExpression(@"^\d{9}[Vv]|\d{12}$", ErrorMessage = "Please enter a valid NIC number.")]
        public string NationalIdentityCard { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string Phonenumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Account Created On")]
        public DateTime AccountCreateDate { get; set; }
    }
}
