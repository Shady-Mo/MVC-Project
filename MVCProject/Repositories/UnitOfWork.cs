using MVCProject.Data;
using MVCProject.Repositories.AccmodationRepo;
using MVCProject.Repositories.ActivityRepo;
using MVCProject.Repositories.BookingRepo;
using MVCProject.Repositories.FlightRepo;

namespace MVCProject.Repositories
{
    public class UnitOfWork
    {
        private readonly AppDbContext context;

        private IActivityRepository activityRepository;
        private IAccomodationRepositroy accomodationRepositroy;
        private IFlightRepository flightRepository;
        private IBookingRepository bookingRepository;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        public IActivityRepository ActivityRepository { 
            get {
                if (activityRepository == null)
                    activityRepository = new ActivityRepository(context);
                return activityRepository;
            }
        }
        public IAccomodationRepositroy AccomodationRepositroy
        {
            get
            {
                if (accomodationRepositroy == null)
                    accomodationRepositroy = new AccomodationRepositroy(context);
                return accomodationRepositroy;
            }
        }

        public IFlightRepository FlightRepository
        {
            get
            {
                if (flightRepository == null)
                    flightRepository = new FlightRepository(context);
                return flightRepository;
            }
        }

        public IBookingRepository BookingRepository
        {
            get
            {
                if (bookingRepository == null)
                    bookingRepository = new BookingRepository(context);
                return bookingRepository;
            }
        }


        public void Save()
        {
            context.SaveChanges();
        }
    }
}
