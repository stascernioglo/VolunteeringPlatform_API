using VolunteeringPlatform.Domain.Auth;

namespace VolunteeringPlatform.Domain.Entities
{
    public class GoodDeedVolunteer : BaseEntity
    {
        public int VolunteerId { get; set; }
        public int GoodDeedId { get; set; }
        public bool Status { get; set; } = false;
        public virtual Volunteer Volunteer { get; set; }
        public virtual GoodDeed GoodDeed { get; set; }
    }
}
