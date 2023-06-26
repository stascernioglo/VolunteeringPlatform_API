using AutoMapper;
using VolunteeringPlatform.Bll.Interfaces;
using VolunteeringPlatform.Common.Dtos.Project;
using VolunteeringPlatform.Dal.Interfaces;
using VolunteeringPlatform.Domain.Entities;

namespace VolunteeringPlatform.Bll.Services
{
    public class ProjectVolunteerService : IProjectVolunteerService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ProjectVolunteerService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProjectVolunteerListDto>> GetProjectParticipants(int id, CancellationToken cancellationToken)
        {
            var projectVolunteers = await _repository.GetAllWithFilterAndInclude<ProjectVolunteer>(x => x.ProjectId == id, cancellationToken, x => x.Volunteer );
            var projectVolunteersDto = _mapper.Map<List<ProjectVolunteer>, List<ProjectVolunteerListDto>>(projectVolunteers);
            return projectVolunteersDto;
        }

        public async Task<List<ParticipationsListDto>> GetVolunteersParticipations(int id, CancellationToken cancellationToken)
        {
            var participations = await _repository.GetAllWithFilterAndInclude<ProjectVolunteer>(x => x.VolunteerId == id, cancellationToken, x => x.Project);
            var participationsDto = _mapper.Map<List<ProjectVolunteer>, List<ParticipationsListDto>>(participations);
            return participationsDto;
        }

        public async Task CancelParticipation(int id, CancellationToken cancellationToken)
        {
            var entity = await _repository.DeleteAsync<ProjectVolunteer>(id, cancellationToken);
            var project = await _repository.GetByIdAsync<Project>(entity.ProjectId, cancellationToken);
            if(project != null)
            {
                project.NumberOfParticipatingVolunteers--;
            }

            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveParticipant(int id, int organizationId, CancellationToken cancellationToken)
        {
            var participation = await _repository.GetByIdWithIncludeAsync<ProjectVolunteer>(id, cancellationToken, x => x.Project, x => x.Volunteer);

            if(participation != null && participation.Project.OrganizationId == organizationId)
            {
                if (participation.Status)
                {
                    participation.Volunteer.NumberOfParticipations--;
                }
                participation.Project.NumberOfParticipatingVolunteers--;
                await _repository.DeleteAsync<ProjectVolunteer>(id, cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<bool> CheckParticipation(int projectId, int volunteerId, CancellationToken cancellationToken)
        {
            var participationsList = await _repository.GetAllWithFilter<ProjectVolunteer>(x => x.ProjectId == projectId, cancellationToken);
            var participantsList = participationsList.Select(x => x.VolunteerId);
            if (participantsList.Contains(volunteerId))
            {
                return true;
            }
            else return false;
        }
        public async Task ConfirmParticipation(int id, int organizationId, CancellationToken cancellationToken)
        {
            var participation = await _repository.GetByIdWithIncludeAsync<ProjectVolunteer>(id, cancellationToken, x => x.Project, x => x.Volunteer);

            if (participation != null && participation.Project.OrganizationId == organizationId)
            {
                participation.Status = true;
                participation.Volunteer.NumberOfParticipations++;
                await _repository.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task CancelConfirmation(int id, int organizationId, CancellationToken cancellationToken)
        {
            var participation = await _repository.GetByIdWithIncludeAsync<ProjectVolunteer>(id, cancellationToken, x => x.Project, x => x.Volunteer);

            if (participation != null && participation.Project.OrganizationId == organizationId)
            {
                if (participation.Status)
                {
                    participation.Status = false;
                    participation.Volunteer.NumberOfParticipations--;
                }
                await _repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
