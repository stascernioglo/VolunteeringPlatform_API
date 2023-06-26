using AutoMapper;
using System.ComponentModel.DataAnnotations;
using VolunteeringPlatform.Bll.Interfaces;
using VolunteeringPlatform.Common.Dtos.GoodDeed;
using VolunteeringPlatform.Common.Models.PagedRequest;
using VolunteeringPlatform.Dal.Interfaces;
using VolunteeringPlatform.Domain.Entities;

namespace VolunteeringPlatform.Bll.Services
{
    public class GoodDeedService : IGoodDeedService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAzureStorageService _azureStorageService;

        public GoodDeedService(IRepository repository, IMapper mapper, IAzureStorageService azureStorageService)
        {
            _repository = repository;
            _mapper = mapper;
            _azureStorageService = azureStorageService;
        }

        public async Task<GoodDeedDto> CreateGoodDeedAsync(GoodDeedForCreateDto goodDeedForCreateDto, CancellationToken cancellationToken)
        {
            var goodDeed = _mapper.Map<GoodDeed>(goodDeedForCreateDto);
            
            if (goodDeedForCreateDto.Image != null)
            {
                var imageProps = await _azureStorageService.UploadAsync(goodDeedForCreateDto.Image, "gooddeeds", cancellationToken);
                goodDeed.ImageName = imageProps.ImageName;
                goodDeed.ImageUrl = imageProps.ImageUrl;
            }
            else
            {
                goodDeed.ImageUrl = "https://utmstorageaccount.blob.core.windows.net/gooddeeds/default.png";
            }

            _repository.Add(goodDeed);
            await _repository.SaveChangesAsync(cancellationToken);

            var goodDeedDto = _mapper.Map<GoodDeedDto>(goodDeed);

            return goodDeedDto;
        }

        public async Task DeleteGoodDeedAsync(int id, CancellationToken cancellationToken)
        {
            var goodDeed = await _repository.GetByIdAsync<GoodDeed>(id, cancellationToken);
            if (goodDeed == null)
            {
                throw new ValidationException($"Good deed with id { id } not found");
            }

            if (goodDeed.ImageName != null)
            {
                await _azureStorageService.DeleteAsync(goodDeed.ImageName, "gooddeeds", cancellationToken);
            }

            await _repository.DeleteAsync<GoodDeed>(id, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task<GoodDeedDto> GetGoodDeedAsync(int id, CancellationToken cancellationToken)
        {
            var goodDeed = await _repository.GetByIdWithIncludeAsync<GoodDeed>(id, cancellationToken, gd => gd.User);
            var goodDeedDto = _mapper.Map<GoodDeedDto>(goodDeed);
            return goodDeedDto;
        }

        public async Task<PaginatedResult<GoodDeedListDto>> GetPagedGoodDeedsAsync(PagedRequest pagedRequest, CancellationToken cancellationToken)
        {
            var pagedGoodDeedsDto = await _repository.GetPagedDataAsync<GoodDeed, GoodDeedListDto>(pagedRequest, cancellationToken);
            return pagedGoodDeedsDto;
        }

        public async Task UpdateGoodDeedAsync(int id, GoodDeedForUpdateDto goodDeedForUpdateDto, CancellationToken cancellationToken)
        {
            var goodDeed = await _repository.GetByIdAsync<GoodDeed>(id, cancellationToken);
            _mapper.Map(goodDeedForUpdateDto, goodDeed);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
