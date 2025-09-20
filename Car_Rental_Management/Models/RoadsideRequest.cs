using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental_Management.Models
{
    public class RoadsideRequest
    {
        [Key]
        public Guid Id { get; set; } // Request Id

        [Required]
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; } // nullable safe

        [Required]
        public Guid CarId { get; set; }

        [ForeignKey("CarId")]
        public virtual Car? Car { get; set; } // nullable safe

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public string? Notes { get; set; } // nullable safe

        [Required]
        public string Status { get; set; } = "Pending";

        [Required]
        public DateTime RequestDate { get; set; } = DateTime.Now;
    }
}
