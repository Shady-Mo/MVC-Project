using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.Models;

namespace MVCProject.AppConfigurations
{
    public class BookingActivityConfiguration : IEntityTypeConfiguration<BookingActivity>
    {
        public void Configure(EntityTypeBuilder<BookingActivity> builder)
        {
            builder.HasIndex(ba => new { ba.BookingId, ba.ActivityId })
                .IsUnique();

            builder.HasOne(ba => ba.Booking)
                .WithMany(b => b.BookingActivities)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ba => ba.Activity)
                .WithMany(b => b.BookingActivities)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
