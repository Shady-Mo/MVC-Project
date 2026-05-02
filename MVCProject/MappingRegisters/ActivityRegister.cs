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

            config.NewConfig<UpdateActivityVM, Activity>()
                .Map(dest => dest.Location, src => $"{src.City}, {src.Country}");

            config.NewConfig<Activity, UpdateActivityVM>()
                .Map(dest => dest.Country, src => src.Location != null && src.Location.Contains(",") 
                    ? src.Location.Substring(src.Location.LastIndexOf(",") + 1).Trim() 
                    : "")
                .Map(dest => dest.City, src => src.Location != null && src.Location.Contains(",") 
                    ? src.Location.Substring(0, src.Location.LastIndexOf(",")).Trim() 
                    : "");

            config.NewConfig<Activity, MostBookedActivitiesViewModel>()
                .Map(d => d.BookingsHistory, s => s.BookingActivities.Count());
        }
    }
}
