using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteeringPlatform.Domain.Auth;

namespace VolunteeringPlatform.Dal.EntityConfigurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Locality)
                .HasMaxLength(50);
        }
    }
}
