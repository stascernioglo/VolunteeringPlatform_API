namespace VolunteeringPlatform.Common.Dtos.GoodDeed
{
    public class GoodDeedListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public string? Locality { get; set; }
        public string ImageUrl { get; set; }
        public int? RequiredNumberOfVolunteers { get; set; }
        public int NumberOfParticipatingVolunteers { get; set; }

    }
}
