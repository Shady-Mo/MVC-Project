using MVCProject.Data;
using MVCProject.Repositories.AccmodationRepo;
using MVCProject.Repositories.ActivityRepo;
using MVCProject.Repositories.BookingAccomodationRepo;
using MVCProject.Repositories.BookingActivityRepo;
using MVCProject.Repositories.BookingFlightRepo;
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
        private IBookingAccomodationRepository bookingAccomodationRepository;
        private IBookingActivityRepository bookingActivityRepository;
        private IBookingFlightRepository bookingFlightRepository;

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

        public IBookingAccomodationRepository BookingAccomodationRepository
        {
            get
            {
                if (bookingAccomodationRepository == null)
                    bookingAccomodationRepository = new BookingAccomodationRepository(context);
                return bookingAccomodationRepository;
            }
        }
        public IBookingActivityRepository BookingActivityRepository
        {
            get
            {
                if (bookingActivityRepository == null)
                    bookingActivityRepository = new BookingActivityRepository(context);
                return bookingActivityRepository;
            }
        }

        public IBookingFlightRepository BookingFlightRepository
        {
            get
            {
                if (bookingFlightRepository == null)
                    bookingFlightRepository = new BookingFlightRepository(context);
                return bookingFlightRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
