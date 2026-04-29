using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.Models;

namespace MVCProject.AppConfigurations
{
    public class SellerConfiguration : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasMany(s => s.Activities)
                .WithOne(a => a.Seller)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(s => s.Accomodations)
                .WithOne(a => a.Seller)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(s => s.Flights)
                .WithOne(f => f.Seller)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
