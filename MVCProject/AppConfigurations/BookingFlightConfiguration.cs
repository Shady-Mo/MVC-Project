using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.Models;

namespace MVCProject.AppConfigurations
{
    public class BookingFlightConfiguration : IEntityTypeConfiguration<BookingFlight>
    {
        public void Configure(EntityTypeBuilder<BookingFlight> builder)
        {
            builder.HasIndex(ba => new { ba.BookingId, ba.FlightId })
                .IsUnique();

            builder.HasOne(ba => ba.Booking)
                .WithMany(b => b.BookingFlights)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ba => ba.Flight)
                .WithMany(b => b.BookingFlights)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
