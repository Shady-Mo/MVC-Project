using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.Models;

namespace MVCProject.AppConfigurations {
    public class AccomodationConfiguration : IEntityTypeConfiguration<Accomodation> {
        public void Configure(EntityTypeBuilder<Accomodation> builder) {
            /* Accomodation-Booking Relationship */
            builder.HasOne(a => a.Booking)
                .WithMany(b => b.Accomodations)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
