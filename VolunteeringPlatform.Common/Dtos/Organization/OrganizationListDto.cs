namespace VolunteeringPlatform.Common.Dtos.Organization
{
    public class OrganizationListDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Description { get; set; }
        public string? Locality { get; set; }
        public string ImageUrl { get; set; }
    }
}
