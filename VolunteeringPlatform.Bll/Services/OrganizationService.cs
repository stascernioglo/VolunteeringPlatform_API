using AutoMapper;
using VolunteeringPlatform.Bll.Interfaces;
using VolunteeringPlatform.Common.Dtos.Organization;
using VolunteeringPlatform.Common.Models.PagedRequest;
using VolunteeringPlatform.Dal.Interfaces;
using VolunteeringPlatform.Domain.Auth;

namespace VolunteeringPlatform.Bll.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public OrganizationService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrganizationDto> GetOrganizationAsync(int id, CancellationToken cancellationToken)
        {
            var organization = await _repository.GetByIdAsync<Organization>(id, cancellationToken);
            var organizationDto = _mapper.Map<OrganizationDto>(organization);
            return organizationDto;
        }

        public async Task<PaginatedResult<OrganizationListDto>> GetPagedOrganizationsAsync(PagedRequest pagedRequest, CancellationToken cancellationToken)
        {
            var pagedOrganizationsDto = await _repository.GetPagedDataAsync<Organization, OrganizationListDto>(pagedRequest, cancellationToken);
            return pagedOrganizationsDto;
        }
    }
}
