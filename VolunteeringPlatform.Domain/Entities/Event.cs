using VolunteeringPlatform.Domain.Auth;

namespace VolunteeringPlatform.Domain.Entities
{
    public class Event : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Locality { get; set; }
        public string? Address { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public string? ImageName { get; set; }
        public string? ImageUrl { get; set; }
        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
