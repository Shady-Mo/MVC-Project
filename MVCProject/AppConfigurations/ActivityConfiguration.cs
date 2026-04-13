using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.Models;

namespace MVCProject.AppConfigurations {
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity> {
        public void Configure(EntityTypeBuilder<Activity> builder) {
            /* Activity-Booking Relationship */
            builder.HasOne(a => a.Booking)
                .WithMany(b => b.Activities)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
