using Microsoft.AspNetCore.Http;
using VolunteeringPlatform.Common.Dtos.Blob;

namespace VolunteeringPlatform.Bll.Interfaces
{
    public interface IAzureStorageService
    {
        Task<BlobResponseDto> UploadAsync(IFormFile file, string containerName, CancellationToken cancellationToken);
        Task DeleteAsync(string blobFilename, string containerName, CancellationToken cancellationToken);
    }
}
