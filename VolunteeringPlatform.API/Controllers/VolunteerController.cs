using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolunteeringPlatform.API.Infrastructure.Extentions;
using VolunteeringPlatform.Bll.Interfaces;
using VolunteeringPlatform.Common.Dtos.Volunteer;
using VolunteeringPlatform.Common.Models.PagedRequest;
using VolunteeringPlatform.Dal.Constants;
using VolunteeringPlatform.Domain.Auth;

namespace VolunteeringPlatform.API.Controllers
{
    [Route("api/volunteers")]
    public class VolunteerController : BaseController
    {
        private readonly IVolunteerService _volunteerService;

        public VolunteerController(IVolunteerService volunteerService)
        {
            _volunteerService = volunteerService;
        }

        [AllowAnonymous]
        [HttpPost("paginated-search")]
        public async Task<PaginatedResult<VolunteerListDto>> GetPagedVolunteers(PagedRequest pagedRequest, CancellationToken cancellationToken)
        {
            var pagedVolunteersDto = await _volunteerService.GetPagedVolunteersAsync(pagedRequest, cancellationToken);
            return pagedVolunteersDto;
        }

        [Authorize(Roles = RoleConstants.Volunteer)]
        [HttpPost("participateproj/{projectId}")]
        public async Task<IActionResult> ParticipateInProject(int projectId, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var volunteerId = User.GetLoggedInUserId();

            await _volunteerService.ParticipateInProjectAsync(projectId, volunteerId, cancellationToken);
            return Ok();
        }

        [Authorize(Roles = RoleConstants.Volunteer)]
        [HttpPost("participategoodd/{goodDeedId}")]
        public async Task<IActionResult> ParticipateInGoodDeed(int goodDeedId, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var volunteerId = User.GetLoggedInUserId();

            await _volunteerService.ParticipateInProjectAsync(goodDeedId, volunteerId, cancellationToken);
            return Ok();
        }
    }
}
