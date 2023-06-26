using VolunteeringPlatform.Common.Dtos.Volunteer;
using VolunteeringPlatform.Common.Models.PagedRequest;

namespace VolunteeringPlatform.Bll.Interfaces
{
    public interface IVolunteerService
    {
        Task<PaginatedResult<VolunteerListDto>> GetPagedVolunteersAsync(PagedRequest pagedRequest, CancellationToken cancellationToken);
        Task ParticipateInProjectAsync(int projectId, int volunteerId, CancellationToken cancellationToken);
        Task ParticipateInGoodDeedAsync(int goodDeedId, int volunteerId, CancellationToken cancellationToken);
    }
}
