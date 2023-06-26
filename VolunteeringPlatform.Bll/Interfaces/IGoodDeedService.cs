using VolunteeringPlatform.Common.Dtos.GoodDeed;
using VolunteeringPlatform.Common.Models.PagedRequest;

namespace VolunteeringPlatform.Bll.Interfaces
{
    public interface IGoodDeedService
    {
        Task<PaginatedResult<GoodDeedListDto>> GetPagedGoodDeedsAsync(PagedRequest pagedRequest, CancellationToken cancellationToken);

        Task<GoodDeedDto> GetGoodDeedAsync(int id, CancellationToken cancellationToken);

        Task<GoodDeedDto> CreateGoodDeedAsync(GoodDeedForCreateDto goodDeedForCreateDto, CancellationToken cancellationToken);

        Task UpdateGoodDeedAsync(int id, GoodDeedForUpdateDto goodDeedForUpdateDto, CancellationToken cancellationToken);

        Task DeleteGoodDeedAsync(int id, CancellationToken cancellationToken);
    }
}
