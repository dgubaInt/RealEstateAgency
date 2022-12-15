using Microsoft.AspNetCore.Http;
using RealEstateAgency.Core.Entities;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IImageService
    {
        Task AddAsync(Photo photo);
        bool DeleteImage(string imageName);
        string DownloadImage(string imageName);
        Task<IEnumerable<Photo>> GetAllAsync();
        bool UploadImage(IFormFile file);
    }
}
