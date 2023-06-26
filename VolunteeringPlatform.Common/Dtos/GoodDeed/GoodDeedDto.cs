namespace VolunteeringPlatform.Common.Dtos.GoodDeed
{
    public class GoodDeedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public string? Locality { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public int? RequiredNumberOfVolunteers { get; set; }
        public int NumberOfParticipatingVolunteers { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
    }
}
