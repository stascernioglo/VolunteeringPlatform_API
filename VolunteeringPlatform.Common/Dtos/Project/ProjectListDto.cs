namespace VolunteeringPlatform.Common.Dtos.Project
{
    public class ProjectListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime? Date { get; set; }
        public string? Locality { get; set; }
        public int? RequiredNumberOfVolunteers { get; set; }
        public int NumberOfParticipatingVolunteers { get; set; }
        public string Organization { get; set; }
        public string ImageUrl { get; set; }
    }
}
