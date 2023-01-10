using RealEstateAgency.Core.Entities;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IImageService
    {
        Task<bool> AddAsync(Photo photo);
        Task<IEnumerable<Photo>> GetAllAsync();
    }
}
