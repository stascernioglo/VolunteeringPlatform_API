using Microsoft.AspNetCore.Identity;
using VolunteeringPlatform.Domain.Entities;

namespace VolunteeringPlatform.Domain.Auth
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public string? Description { get; set; }
        public string? Locality { get; set; }
        public virtual ICollection<GoodDeed> GoodDeeds { get; set; }
        public string? ImageName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
