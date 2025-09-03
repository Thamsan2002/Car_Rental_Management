using Car_Rental_Management.Models;

namespace Car_Rental_Management.Dtos
{

    public class StaffDetailDto
    {
        public string Name { get; set; }
        public string StaffCode { get; set; }
        public string Address { get; set; }
        public StaffStatus Status { get; set; }
        public string ProfileImage { get; set; }
        public decimal Salary { get; set; }
        public TimeSpan ShiftTime { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }



}
