using System.ComponentModel.DataAnnotations;

namespace VolunteeringPlatform.Common.Dtos.Project
{
    public class ProjectForUpdateDto
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

        [Range(0, 999)]
        public int? RequiredNumberOfVolunteers { get; set; }
    }
}
