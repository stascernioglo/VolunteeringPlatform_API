using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteeringPlatform.Domain.Entities;

namespace VolunteeringPlatform.Dal.EntityConfigurations
{
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
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

            builder.HasOne(x => x.Organization)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.OrganizationId);

            builder.HasMany(x => x.ProjectVolunteers)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectId);

        }
    }
}
