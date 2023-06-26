using AutoMapper;
using System.ComponentModel.DataAnnotations;
using VolunteeringPlatform.Bll.Interfaces;
using VolunteeringPlatform.Common.Dtos.Project;
using VolunteeringPlatform.Common.Models.PagedRequest;
using VolunteeringPlatform.Dal.Interfaces;
using VolunteeringPlatform.Domain.Entities;

namespace VolunteeringPlatform.Bll.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAzureStorageService _azureStorageService;

        public ProjectService(IRepository repository, IMapper mapper, IAzureStorageService azureStorageService)
        {
            _repository = repository;
            _mapper = mapper;
            _azureStorageService = azureStorageService;
        }

        public async Task<ProjectDto> CreateProjectAsync(ProjectForCreateDto projectForCreateDto, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Project>(projectForCreateDto);

            if (projectForCreateDto.Image != null)
            {
                var imageProps = await _azureStorageService.UploadAsync(projectForCreateDto.Image, "projects", cancellationToken);
                project.ImageName = imageProps.ImageName;
                project.ImageUrl = imageProps.ImageUrl;
            }
            else
            {
                project.ImageUrl = "https://mystorageaccountfortests.blob.core.windows.net/projects/default.png";
            }
            
            _repository.Add(project);
            await _repository.SaveChangesAsync(cancellationToken);

            var projectDto = _mapper.Map<ProjectDto>(project);

            return projectDto;
        }

        public async Task DeleteProjectAsync(int id, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync<Project>(id, cancellationToken);
            if (project == null)
            {
                throw new ValidationException($"Project with id { id } not found");
            }

            if (project.ImageName != null)
            {
                await _azureStorageService.DeleteAsync(project.ImageName, "projects", cancellationToken);
            }
            
            await _repository.DeleteAsync<Project>(id, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task<PaginatedResult<ProjectListDto>> GetPagedProjectsAsync(PagedRequest pagedRequest, CancellationToken cancellationToken)
        {
            var pagedProjectsDto = await _repository.GetPagedDataAsync<Project, ProjectListDto>(pagedRequest, cancellationToken);
            return pagedProjectsDto;
        }

        public async Task<ProjectDto> GetProjectAsync(int id, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdWithIncludeAsync<Project>(id, cancellationToken, project => project.Organization);
            var projectDto = _mapper.Map<ProjectDto>(project);
            return projectDto;
        }

        public async Task UpdateProjectAsync(int id, ProjectForUpdateDto projectForUpdateDto, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync<Project>(id, cancellationToken);
            _mapper.Map(projectForUpdateDto, project);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<MyProjectsListDto>> GetMyProjectsAsync(int organizationId, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetAllWithFilter<Project>(x => x.OrganizationId == organizationId, cancellationToken);
            var projectsDto = _mapper.Map<List<Project>, List<MyProjectsListDto>>(projects);
            return projectsDto;
        }
    }
}
