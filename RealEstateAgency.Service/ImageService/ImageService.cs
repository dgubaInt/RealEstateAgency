using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;

namespace RealEstateAgency.Service.ImageService
{
    public class ImageService : IImageService
    {
        private readonly IGenericRepository<Photo> _photoRepository;

        public ImageService(IGenericRepository<Photo> photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public async Task<bool> AddAsync(Photo photo)
        {
            return await _photoRepository.AddAsync(photo);
        }

        public async Task<IEnumerable<Photo>> GetAllAsync()
        {
            return await _photoRepository.GetAllAsync();
        }
    }
}
