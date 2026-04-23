namespace MVCProject.ViewModels.BookingViewModel
{
    public class DisplayBookingVM
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string Flight { get; set; }
        public int AccommodationsCount { get; set; }
        public int ActivitiesCount { get; set; }
    }
}
