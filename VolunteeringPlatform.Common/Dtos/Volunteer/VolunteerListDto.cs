namespace VolunteeringPlatform.Common.Dtos.Volunteer
{
    public class VolunteerListDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Locality { get; set; }
        public DateTime BirthDate { get; set; }
        public int NumberOfParticipations { get; set; }
        public string ImageUrl { get; set; }
    }
}
