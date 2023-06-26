using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolunteeringPlatform.API.Infrastructure.Extentions;
using VolunteeringPlatform.Bll.Interfaces;
using VolunteeringPlatform.Common.Dtos.Project;
using VolunteeringPlatform.Common.Models;
using VolunteeringPlatform.Common.Models.PagedRequest;
using VolunteeringPlatform.Dal.Constants;

namespace VolunteeringPlatform.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly IProjectVolunteerService _projectVolunteerService;

        public ProjectsController(IProjectService projectService, IProjectVolunteerService projectVolunteerService)
        {
            _projectService = projectService;
            _projectVolunteerService = projectVolunteerService;
        }

        [AllowAnonymous]
        [HttpPost("paginated-search")]
        public async Task<PaginatedResult<ProjectListDto>> GetPagedProjects(PagedRequest pagedRequest, CancellationToken cancellationToken)
        {
            var pagedProjectsDto = await _projectService.GetPagedProjectsAsync(pagedRequest, cancellationToken);
            return pagedProjectsDto;
        }


        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ProjectResult> GetProject(int id, CancellationToken cancellationToken)
        {
            var projectDto = await _projectService.GetProjectAsync(id, cancellationToken);
            var loggedUserId = User.GetLoggedInUserId();
            var participation = false;
            if(loggedUserId != 0)
            {
                participation = await _projectVolunteerService.CheckParticipation(id, loggedUserId, cancellationToken);
            }
            return new ProjectResult()
            {
                Participation = participation,
                Project = projectDto
            };
        }

        [Authorize(Roles = RoleConstants.Organization)]
        [HttpGet("mine")]
        public async Task<List<MyProjectsListDto>> GetMyProjects(CancellationToken cancellationToken)
        {
            var organizationId = User.GetLoggedInUserId();
            var projectDto = await _projectService.GetMyProjectsAsync(organizationId, cancellationToken);
            return projectDto;
        }

        [Authorize(Roles = RoleConstants.Organization)]
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromForm]ProjectForCreateDto projectForCreateDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var organizationId = User.GetLoggedInUserId();
            projectForCreateDto.OrganizationId = organizationId;

            var projectDto = await _projectService.CreateProjectAsync(projectForCreateDto, cancellationToken);

            return CreatedAtAction(nameof(GetProject), new { id = projectDto.Id }, projectDto);
        }

        [Authorize(Roles = RoleConstants.Organization)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectForUpdateDto projectForUpdateDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _projectService.UpdateProjectAsync(id, projectForUpdateDto, cancellationToken);
            return Ok();
        }

        [Authorize(Roles = RoleConstants.Organization)]
        [HttpDelete("{id}")]
        public async Task DeleteProject(int id, CancellationToken cancellationToken)
        {
            await _projectService.DeleteProjectAsync(id, cancellationToken);
        }
    }
}
