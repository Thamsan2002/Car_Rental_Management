using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.ViewModel
{
    public class DriverViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "( personal phone number )Emergency contact number is required.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "contact number is required.")]
        public string EmergencyContact { get; set; }

        [Required(ErrorMessage = "NIC number is required.")]
        public string Nic { get; set; }

        [Required(ErrorMessage = "Please select your gender.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "License number is required.")]
        public string LicenseNumber { get; set; }

        [Required(ErrorMessage = "License expiry date is required.")]
        [DataType(DataType.Date)]
        public DateTime LicenseExpiryDate { get; set; }

        [Required(ErrorMessage = "Experience is required.")]
        public string Experience { get; set; }

        [Required(ErrorMessage = "Vehicle type is required.")]
        public string VehicleType { get; set; }
    }

}
