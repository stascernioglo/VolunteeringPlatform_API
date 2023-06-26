using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteeringPlatform.Domain.Entities;

namespace VolunteeringPlatform.Dal.EntityConfigurations
{
    public class GoodDeedConfig : IEntityTypeConfiguration<GoodDeed>
    {
        public void Configure(EntityTypeBuilder<GoodDeed> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Category)
                .HasMaxLength(50);

            builder.Property(x => x.Date)
                .HasColumnType("date");

            builder.Property(x => x.Locality)
                .HasMaxLength(50);

            builder.HasMany(x => x.GoodDeedVolunteers)
                .WithOne(x => x.GoodDeed)
                .HasForeignKey(x => x.GoodDeedId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.GoodDeeds)
                .HasForeignKey(x => x.UserId);
        }
    }
}
