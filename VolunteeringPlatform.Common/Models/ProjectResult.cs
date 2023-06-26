using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteeringPlatform.Common.Dtos.Project;

namespace VolunteeringPlatform.Common.Models
{
    public class ProjectResult
    {
        public bool Participation { get; set; }
        public ProjectDto Project { get; set; }
    }
}
