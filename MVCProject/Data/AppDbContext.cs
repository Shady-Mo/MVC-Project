using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using System.Reflection;
using System.Reflection.Emit;

namespace MVCProject.Data {
    public class AppDbContext : IdentityDbContext<AppUser> {
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<BookingAccomodation> BookingAccomodations { get; set; }
        public DbSet<BookingActivity> BookingActivities { get; set; }
        public DbSet<BookingFlight> BookingFlights { get; set; }
    }
}
