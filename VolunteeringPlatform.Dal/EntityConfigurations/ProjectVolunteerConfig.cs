using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteeringPlatform.Domain.Entities;

namespace VolunteeringPlatform.Dal.EntityConfigurations
{
    public class ProjectVolunteerConfig : IEntityTypeConfiguration<ProjectVolunteer>
    {
        public void Configure(EntityTypeBuilder<ProjectVolunteer> builder)
        {
            builder.ToTable("ProjectVolunteers");
            builder.HasKey(gv => new { gv.ProjectId, gv.VolunteerId });
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Volunteer)
                .WithMany(x => x.ProjectVolunteers)
                .HasForeignKey(x => x.VolunteerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
