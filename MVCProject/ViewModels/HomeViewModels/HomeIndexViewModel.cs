using MVCProject.ViewModels.AccomodationViewModels;
using MVCProject.ViewModels.ActivityViewModels;

namespace MVCProject.ViewModels.HomeViewModels
{
    public class HomeIndexViewModel
    {
        public int TotalAccommodations { get; set; }
        public int TotalActivities { get; set; }

        public List<DisplayAccomodationVM> Accommodations { get; set; } = [];
        public List<DisplayActivityVM> Activities { get; set; } = [];
    }
}