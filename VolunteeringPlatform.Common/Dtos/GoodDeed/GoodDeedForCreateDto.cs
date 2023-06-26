using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace VolunteeringPlatform.Common.Dtos.GoodDeed
{
    public class GoodDeedForCreateDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Category { get; set; }
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string? Locality { get; set; }
        public string? Address { get; set; }

        [StringLength(30, MinimumLength = 5)]
        public string? PhoneNumber { get; set; }

        [Range(0, 999)]
        public int? RequiredNumberOfVolunteers { get; set; }
        public IFormFile? Image { get; set; }
        public int? UserId { get; set; }
    }
}
