using VolunteeringPlatform.Domain.Auth;

namespace VolunteeringPlatform.Domain.Entities
{
    public class GoodDeed : BaseEntity
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public string? Locality { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public int? RequiredNumberOfVolunteers { get; set; }
        public int NumberOfParticipatingVolunteers { get; set; } = 0;
        public int UserId { get; set; }
        public string? ImageName { get; set; }
        public string? ImageUrl { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<GoodDeedVolunteer> GoodDeedVolunteers { get; set; }
    }
}
