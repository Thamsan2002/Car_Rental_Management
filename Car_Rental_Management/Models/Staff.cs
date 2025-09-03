namespace Car_Rental_Management.Models
{
    public class Staff
    {
        public Guid Id { get; set; }
        public string StaffCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public StaffStatus Status { get; set; }
        public string? ProfileImage { get; set; }
        public decimal Salary { get; set; }
        public TimeSpan ShiftTime { get; set; }


        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
