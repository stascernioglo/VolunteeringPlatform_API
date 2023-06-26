namespace VolunteeringPlatform.Common.Dtos.Project
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public string? Locality { get; set; }
        public string? Address { get; set; }
        public int? RequiredNumberOfVolunteers { get; set; }
        public int NumberOfParticipatingVolunteers { get; set; }
        public string ImageUrl { get; set; }
        public int OrganizationId { get; set; }
        public string Organization { get; set; }
    }
}
