using MVCProject.ViewModels.AdminDashboardViewModels;

namespace MVCProject.ViewModels.SellerDashboardViewModels
{
    public class SellerDashboardIndexVM
    {
        public int TotalAccommodations { get; set; }
        public int TotalFlights { get; set; }
        public int TotalActivities { get; set; }

        public List<AccommodationVM> RecentAccommodations { get; set; } = new();
        public List<FlightVM> RecentFlights { get; set; } = new();
        public List<ActivityVM> RecentActivities { get; set; } = new();
    }

    public class AccommodationVM 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PricePerNight { get; set; }
    }
    public class FlightVM 
    { 
        public int Id { get; set; }
        public string DestinationAirport { get; set; }
        public decimal Price { get; set; }
    }
    public class ActivityVM 
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    
}