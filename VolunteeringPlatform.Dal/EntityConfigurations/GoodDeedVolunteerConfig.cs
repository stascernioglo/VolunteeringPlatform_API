using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteeringPlatform.Domain.Entities;

namespace VolunteeringPlatform.Dal.EntityConfigurations
{
    public class GoodDeedVolunteerConfig : IEntityTypeConfiguration<GoodDeedVolunteer>
    {
        public void Configure(EntityTypeBuilder<GoodDeedVolunteer> builder)
        {
            builder.ToTable("GoodDeedVolunteers");
            builder.HasKey(gv => new { gv.GoodDeedId, gv.VolunteerId });
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Volunteer)
                .WithMany(x => x.GoodDeedVolunteers)
                .HasForeignKey(x => x.VolunteerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
