using System.Collections.Generic;

namespace MVCProject.ViewModels.AdminDashboardViewModels
{
    public class DashboardStatisticsVM
    {
        public RevenueOverviewVM RevenueOverview { get; set; }
        public ServiceDistributionVM ServiceDistribution { get; set; }
        public BookingStatusBreakdownVM BookingStatusBreakdown { get; set; }
        public TopDestinationsVM TopDestinations { get; set; }
    }

    public class RevenueOverviewVM
    {
        public List<string> Months { get; set; } = new List<string>();
        public List<decimal> Revenue { get; set; } = new List<decimal>();
    }

    public class ServiceDistributionVM
    {
        public List<string> Services { get; set; } = new List<string>();
        public List<int> Counts { get; set; } = new List<int>();
        public List<string> Colors { get; set; } = new List<string>
        {
            "rgba(58, 192, 173, 0.8)",    // Teal for Flights
            "rgba(244, 178, 102, 0.8)",   // Orange for Activities
            "rgba(52, 211, 153, 0.8)"     // Green for Accommodations
        };
    }

    public class BookingStatusBreakdownVM
    {
        public List<string> Statuses { get; set; } = new List<string>();
        public List<int> Counts { get; set; } = new List<int>();
        public List<string> Colors { get; set; } = new List<string>
        {
            "rgba(255, 193, 7, 0.8)",     // Yellow for Pending
            "rgba(40, 167, 69, 0.8)",     // Green for Confirmed
            "rgba(220, 53, 69, 0.8)"      // Red for Cancelled
        };
    }

    public class TopDestinationsVM
    {
        public List<string> Countries { get; set; } = new List<string>();
        public List<int> BookingCounts { get; set; } = new List<int>();
        public List<string> Colors { get; set; } = new List<string>
        {
            "rgba(58, 192, 173, 0.8)",
            "rgba(244, 178, 102, 0.8)",
            "rgba(52, 211, 153, 0.8)",
            "rgba(106, 90, 205, 0.8)",
            "rgba(255, 107, 107, 0.8)"
        };
    }
}
