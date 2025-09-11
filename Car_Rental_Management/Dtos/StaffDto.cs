namespace Car_Rental_Management.Dtos
{
    public class StaffDto
    {
        public Guid staffId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public int Salary { get; set; }
        public TimeSpan ShiftTime { get; set; }
        public string ProfileImage { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
