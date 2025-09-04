using Car_Rental_Management.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Car_Rental_Management.viewmodel
{
    public class Staffviewmodel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [BindNever]
        public string? StaffCode { get; set; }
        public string Address { get; set; }
        public StaffStatus Status { get; set; }
        public string? ProfileImage { get; set; }
        public decimal Salary { get; set; }
        public TimeSpan ShiftTime { get; set; }

        // User details
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; }

    }
}
