using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.ViewModel
{
    public class DriverViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, Phone]
        public string EmergencyContact { get; set; }

        [Required]
        public string Nic { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string LicenseNumber { get; set; }

        [Required]
        public DateTime LicenseExpiryDate { get; set; }

        [Required]
        public string Experience { get; set; }

        [Required]
        public string VehicleType { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }
    }
}