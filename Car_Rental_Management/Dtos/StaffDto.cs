namespace Car_Rental_Management.Dtos
{
    public class StaffDto
    {
        public Guid Id { get; set; }          // Staff Id
        public string Name { get; set; }      // Staff Name
        public string Status { get; set; }    // Active / Inactive / OnLeave
        public decimal Salary { get; set; }   // Salary
        public string? EmailAddress { get; set; } // Optional: linked User email
        public string? PhoneNumber { get; set; }  // Optional: linked User phone
        public TimeSpan ShiftTime { get; set; }   // Shift time
    }
}
