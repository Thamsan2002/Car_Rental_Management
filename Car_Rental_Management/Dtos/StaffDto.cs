namespace Car_Rental_Management.Dtos
{
    public class StaffDto
    {
        public Guid staffId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
        public string ProfileImage { get; set; } = string.Empty;
        public int Salary { get; set; }
        public TimeSpan ShiftTime { get; set; }

        // User Info
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
