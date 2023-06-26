using VolunteeringPlatform.Common.Dtos.Project;
using VolunteeringPlatform.Common.Models.PagedRequest;

namespace VolunteeringPlatform.Bll.Interfaces
{
    public interface IProjectService
    {
        Task<PaginatedResult<ProjectListDto>> GetPagedProjectsAsync(PagedRequest pagedRequest, CancellationToken cancellationToken);

        Task<ProjectDto> GetProjectAsync(int id, CancellationToken cancellationToken);

        Task<ProjectDto> CreateProjectAsync(ProjectForCreateDto projectForCreateDto, CancellationToken cancellationToken);

        Task UpdateProjectAsync(int id, ProjectForUpdateDto projectForUpdateDto, CancellationToken cancellationToken);

        Task DeleteProjectAsync(int id, CancellationToken cancellationToken);
        public Task<List<MyProjectsListDto>> GetMyProjectsAsync(int organizationId, CancellationToken cancellationToken);
    }
}
