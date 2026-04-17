using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.Models;

namespace MVCProject.AppConfigurations
{
    public class BookingAccomodationConfiguration : IEntityTypeConfiguration<BookingAccomodation>
    {
        public void Configure(EntityTypeBuilder<BookingAccomodation> builder)
        {
            builder.HasIndex(ba => new { ba.BookingId, ba.AccomodationId })
                .IsUnique();

            builder.HasOne(ba => ba.Booking)
                .WithMany(b => b.bookingAccomodations)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ba => ba.Accomodation)
                .WithMany(b => b.bookingAccomodations)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
