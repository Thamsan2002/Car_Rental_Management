using Car_Rental_Management.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace Car_Rental_Management.DTO
{
    public class CustomerDto
    {
       
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string NationalIdentityCard { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string AccountCreateDate { get; set; }

    }
}
