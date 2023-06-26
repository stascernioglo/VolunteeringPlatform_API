using VolunteeringPlatform.Common.Dtos.Project;

namespace VolunteeringPlatform.Bll.Interfaces
{
    public interface IProjectVolunteerService
    {
        public Task<List<ProjectVolunteerListDto>> GetProjectParticipants(int Id, CancellationToken cancellationToken);
        public Task<List<ParticipationsListDto>> GetVolunteersParticipations(int Id, CancellationToken cancellationToken);
        public Task CancelParticipation(int Id, CancellationToken cancellationToken);
        public Task RemoveParticipant(int Id, int organizationId, CancellationToken cancellationToken);
        public Task ConfirmParticipation(int Id, int organizationId, CancellationToken cancellationToken);
        public Task CancelConfirmation(int Id, int organizationId, CancellationToken cancellationToken);
        public Task<bool> CheckParticipation(int projectId, int volunteerId, CancellationToken cancellationToken);
    }
}
