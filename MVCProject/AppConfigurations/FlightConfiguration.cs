using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.Models;

namespace MVCProject.AppConfigurations {
    public class FlightConfiguration : IEntityTypeConfiguration<Flight> {
        public void Configure(EntityTypeBuilder<Flight> builder) {
            /* Flight-Booking Relationship */
            builder.HasMany(f => f.BookingFlights)
                .WithOne(b => b.Flight)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
