using AutoMapper;
using VolunteeringPlatform.Bll.Interfaces;
using VolunteeringPlatform.Common.Dtos.Volunteer;
using VolunteeringPlatform.Common.Models.PagedRequest;
using VolunteeringPlatform.Dal.Interfaces;
using VolunteeringPlatform.Domain.Auth;
using VolunteeringPlatform.Domain.Entities;

namespace VolunteeringPlatform.Bll.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public VolunteerService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<VolunteerListDto>> GetPagedVolunteersAsync(PagedRequest pagedRequest, CancellationToken cancellationToken)
        {
            var pagedVolunteersDto = await _repository.GetPagedDataAsync<Volunteer, VolunteerListDto>(pagedRequest, cancellationToken);
            return pagedVolunteersDto;
        }

        public async Task ParticipateInProjectAsync(int projectId, int volunteerId, CancellationToken cancellationToken)
        {
            var projectVolunteer = new ProjectVolunteer() { ProjectId = projectId, VolunteerId = volunteerId };
            _repository.Add(projectVolunteer);
            var project = await _repository.GetByIdAsync<Project>(projectId, cancellationToken);
            if (project != null)
            {
                project.NumberOfParticipatingVolunteers++;
            }
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task ParticipateInGoodDeedAsync(int goodDeedId, int volunteerId, CancellationToken cancellationToken)
        {
            var goodDeedVolunteer = new GoodDeedVolunteer() { GoodDeedId = goodDeedId, VolunteerId = volunteerId };
            _repository.Add(goodDeedVolunteer);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
