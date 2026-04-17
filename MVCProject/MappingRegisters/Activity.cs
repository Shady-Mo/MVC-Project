using Mapster;
using MVCProject.Models;
using MVCProject.ViewModels.ActivityViewModels;

namespace MVCProject.MappingRegisters
{
    public class ActivityRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddActivityVM, Activity>();
            config.NewConfig<Activity, AddActivityVM>();

            config.NewConfig<DisplayActivityVM, Activity>();
            config.NewConfig<Activity, DisplayActivityVM>();
        }
    }
}
