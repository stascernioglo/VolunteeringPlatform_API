using System.ComponentModel.DataAnnotations;

namespace VolunteeringPlatform.Domain.Entities
{
    public class News : BaseEntity
    {
        [MaxLength(200)]
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? ImageName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
