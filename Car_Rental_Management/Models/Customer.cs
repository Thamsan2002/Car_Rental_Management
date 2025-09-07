using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental_Management.Models
{
    public class Customer
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Gender { get; set; }


        [Required]
        public string NationalIdentityCard { get; set; }

        [Required]
        [Phone]
        public string Phonenumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string AccountCreateDate { get; set; }

    }
}
