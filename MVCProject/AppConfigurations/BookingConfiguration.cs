using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.Models;

namespace MVCProject.AppConfigurations {
    public class BookingConfiguration : IEntityTypeConfiguration<Booking> {
        public void Configure(EntityTypeBuilder<Booking> builder) {
            /* Booking-User Relationship */
            builder.HasOne(b => b.AppUser)
                .WithMany(u => u.Bookings)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
