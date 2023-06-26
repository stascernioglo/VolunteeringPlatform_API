using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteeringPlatform.Domain.Entities;

namespace VolunteeringPlatform.Dal.EntityConfigurations
{
    public class EventConfig : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Date)
                .HasColumnType("date");

            builder.Property(x => x.Locality)
                .HasMaxLength(50);

            builder.HasOne(x => x.Organization)
                .WithMany(x => x.Events)
                .HasForeignKey(x => x.OrganizationId);
        }
    }
}
