using MVCProject.ViewModels.AccomodationViewModels;
using MVCProject.ViewModels.ActivityViewModels;

namespace MVCProject.ViewModels.HomeViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<MostBookedAccommodationsViewModel> Accommodations { get; set; } = [];
        public IEnumerable<MostBookedActivitiesViewModel> Activities { get; set; } = [];
        public IEnumerable<UpcomingFlightsViewModel> Flights { get; set; } = [];
    }
}