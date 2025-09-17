using Car_Rental_Management.Dtos;

namespace Car_Rental_Management.ViewModel
{
    public class BookingFormViewModel
    {
        public Guid CarId { get; set; }
        public Guid? DriverId { get; set; } // driver select pannuradhukku
        public string BookingType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }

        public List<CarDto> Cars { get; set; } = new();
        public List<DriverDto> Drivers { get; set; } = new();
    }
}
