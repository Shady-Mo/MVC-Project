using MVCProject.ViewModels.AccomodationViewModels;
using MVCProject.ViewModels.ActivityViewModels;

namespace MVCProject.ViewModels.HomeViewModels
{
    public class HomeIndexViewModel
    {
        public List<DisplayAccomodationVM> Accommodations { get; set; } = new List<DisplayAccomodationVM>();
        public List<DisplayActivityVM> Activities { get; set; } = new List<DisplayActivityVM>();
        public int TotalAccommodations { get; set; }
        public int TotalActivities { get; set; }
    }
}