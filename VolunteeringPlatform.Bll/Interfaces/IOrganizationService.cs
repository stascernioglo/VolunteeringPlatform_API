using VolunteeringPlatform.Common.Dtos.Organization;
using VolunteeringPlatform.Common.Models.PagedRequest;

namespace VolunteeringPlatform.Bll.Interfaces
{
    public interface IOrganizationService
    {
        Task<PaginatedResult<OrganizationListDto>> GetPagedOrganizationsAsync(PagedRequest pagedRequest, CancellationToken cancellationToken);

        Task<OrganizationDto> GetOrganizationAsync(int id, CancellationToken cancellationToken);
    }
}
