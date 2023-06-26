using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteeringPlatform.Common.Dtos.Project
{
    public class ProjectVolunteerListDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
    }
}
