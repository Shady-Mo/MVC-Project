using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.Models;

namespace MVCProject.AppConfigurations {
    public class UserConfiguration : IEntityTypeConfiguration<AppUser> {
        public void Configure(EntityTypeBuilder<AppUser> builder) {
            builder.HasDiscriminator<string>("UserType")
                .HasValue<AppUser>("User")
                .HasValue<Seller>("Seller");
        }
    }
}
