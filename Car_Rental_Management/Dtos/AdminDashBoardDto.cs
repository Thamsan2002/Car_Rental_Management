namespace Car_Rental_Management.Dtos
{
    public class AdminDashBoardDto
    {
        public int StaffCount { get; set; }
        public int CustomerCount { get; set; }
        public int CarCount { get; set; }
        public int DriverCount { get; set; }
        public int AvailableCarCount { get; set; }
        public int UnavailableCarCount { get; set; }
    }
}
