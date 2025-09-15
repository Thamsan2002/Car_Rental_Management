using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental_Management.Models
{
    public class Driver
    {


        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
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

        public string Role { get; set; } = "Driver";

        [Required]
        public string VehicleType { get; set; }
    }

}
