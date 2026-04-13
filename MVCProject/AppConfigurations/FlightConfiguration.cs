using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.Models;

namespace MVCProject.AppConfigurations {
    public class FlightConfiguration : IEntityTypeConfiguration<Flight> {
        public void Configure(EntityTypeBuilder<Flight> builder) {
            /* Flight-Booking Relationship */
            builder.HasOne(f => f.Booking)
                .WithMany(b => b.Flights)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
