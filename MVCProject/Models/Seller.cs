namespace MVCProject.Models
{
    public class Seller: AppUser
    {
        public ICollection<Accomodation> Accomodations = new HashSet<Accomodation>();
        public ICollection<Activity> Activities = new HashSet<Activity>();
        public ICollection<Flight> Flights = new HashSet<Flight>();
    }
}
