using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolunteeringPlatform.API.Infrastructure.Extentions;
using VolunteeringPlatform.Bll.Interfaces;
using VolunteeringPlatform.Common.Dtos.Project;
using VolunteeringPlatform.Dal.Constants;

namespace VolunteeringPlatform.API.Controllers
{
    [Route("api/projectvolunteers")]
    [ApiController]
    public class ProjectVolunteerController : BaseController
    {
        private readonly IProjectVolunteerService _projectVolunteerService;

        public ProjectVolunteerController(IProjectVolunteerService projectVolunteerService)
        {
            _projectVolunteerService = projectVolunteerService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<List<ProjectVolunteerListDto>> GetParticipants(int id, CancellationToken cancellationToken)
        {
            var projectVolunteerDto = await _projectVolunteerService.GetProjectParticipants(id, cancellationToken);
            return projectVolunteerDto;
        }

        [Authorize(Roles = RoleConstants.Volunteer)]
        [HttpGet("mine")]
        public async Task<List<ParticipationsListDto>> GetParticipations(CancellationToken cancellationToken)
        {
            var volunteerId = User.GetLoggedInUserId();
            var participationsDto = await _projectVolunteerService.GetVolunteersParticipations(volunteerId, cancellationToken);
            return participationsDto;
        }

        [Authorize(Roles = RoleConstants.Volunteer)]
        [HttpDelete("{id}")]
        public async Task CancelPartipation(int id, CancellationToken cancellationToken)
        {
            await _projectVolunteerService.CancelParticipation(id, cancellationToken);
        }
        
        [Authorize(Roles = RoleConstants.Organization)]
        [HttpDelete("remove/{id}")]
        public async Task RemovePartipant(int id, CancellationToken cancellationToken)
        {
            var organizationId = User.GetLoggedInUserId();
            await _projectVolunteerService.RemoveParticipant(id, organizationId, cancellationToken);
        }

        [Authorize(Roles = RoleConstants.Organization)]
        [HttpPost("confirm/{id}")]
        public async Task ConfirmParticipation(int id, CancellationToken cancellationToken)
        {
            var organizationId = User.GetLoggedInUserId();
            await _projectVolunteerService.ConfirmParticipation(id, organizationId, cancellationToken);
        }

        [Authorize(Roles = RoleConstants.Organization)]
        [HttpPost("cancel/{id}")]
        public async Task CancelConfirmation(int id, CancellationToken cancellationToken)
        {
            var organizationId = User.GetLoggedInUserId();
            await _projectVolunteerService.CancelConfirmation(id, organizationId, cancellationToken);
        }
    }
}
