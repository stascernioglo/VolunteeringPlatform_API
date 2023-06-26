using VolunteeringPlatform.Domain.Entities;

namespace VolunteeringPlatform.Domain.Auth
{
    public class Organization : User
    {
        public string? Address { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
