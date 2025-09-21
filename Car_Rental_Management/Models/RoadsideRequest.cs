using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental_Management.Models
{
    public class RoadsideRequest
    {
        [Key]
        public Guid RequestId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }

        [Required]
        public Guid CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public virtual Car? Car { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public string? Notes { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        [Required]
        public DateTime RequestDate { get; set; } = DateTime.Now;
    }
}
