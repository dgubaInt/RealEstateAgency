using Microsoft.AspNetCore.Http;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IImageHelper
    {
        bool DeleteImage(string imageName);
        string DownloadImage(string imageName);
        bool UploadImage(IFormFile file, string fileName);
    }
}
