using Car_Rental_Management.Dtos;

namespace Car_Rental_Management.ViewModel
{
    public class BookingViewmodel
    {
        public Guid CustomerId { get; set; }
        public Guid CarId { get; set; }
        public string BookingType { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string? DriverName { get; set; }

            public List<BookingDateRangeDto>? BookedPeriods { get; set; }
        


        // Car details
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string CarColor { get; set; }
        public decimal CarPricePerDay { get; set; }
        public string CarImage { get; set; }
        public Guid? DriverId { get; set; }

        // Driver list
    }
}
