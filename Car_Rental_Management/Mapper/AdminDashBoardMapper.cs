using Car_Rental_Management.Dtos;

namespace Car_Rental_Management.Mapper
{
    public static class AdminDashboardMapper
    {
        public static AdminDashBoardDto MapToDto(
            int staffCount,
            int customerCount,
            int carCount,
            int availableCars,
            int driverCount)
        {
            return new AdminDashBoardDto
            {
                StaffCount = staffCount,
                CustomerCount = customerCount,
                CarCount = carCount,
                AvailableCarCount = availableCars,
                UnavailableCarCount = carCount - availableCars,
                DriverCount = driverCount
            };
        }
    }
  
    
 }

