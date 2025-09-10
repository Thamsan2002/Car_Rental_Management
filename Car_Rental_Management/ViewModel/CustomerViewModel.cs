using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.ViewModel
{
    public class CustomerViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string NationalIdentityCard { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        public string AccountCreateDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
    }
}
