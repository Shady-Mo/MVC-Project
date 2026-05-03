using MVCProject.ViewModels.AdminDashboardViewModels;

namespace MVCProject.ViewModels.SellerDashboardViewModels
{
    public class SellerStatisticsVM
    {
        public RevenueOverviewVM RevenueOverview { get; set; } = new();
        public ServiceDistributionVM ServiceDistribution { get; set; } = new();
        public BookingStatusBreakdownVM BookingStatusBreakdown { get; set; } = new();
    }
}
