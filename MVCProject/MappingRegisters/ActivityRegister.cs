using Mapster;
using MVCProject.Models;
using MVCProject.ViewModels.ActivityViewModels;
using MVCProject.ViewModels.HomeViewModels;

namespace MVCProject.MappingRegisters {
    public class ActivityRegister : IRegister {
        public void Register(TypeAdapterConfig config) {
            config.NewConfig<AddActivityVM, Activity>();
            config.NewConfig<Activity, AddActivityVM>();

            config.NewConfig<DisplayActivityVM, Activity>();
            config.NewConfig<Activity, DisplayActivityVM>()
                .Map(dest => dest.SellerName, src => (src.Seller != null)? src.Seller.FullName : "");

            config.NewConfig<Activity, MostBookedActivitiesViewModel>()
                .Map(d => d.BookingsHistory, s => s.BookingActivities.Count());
        }
    }
}
