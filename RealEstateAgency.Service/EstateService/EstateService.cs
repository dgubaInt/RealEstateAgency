using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Core.Models;
using RealEstateAgency.Service.Mappers;

namespace RealEstateAgency.Service.EstateService
{
    public class EstateService : IEstateService
    {
        private readonly IGenericRepository<Estate> _estateRepository;
        private readonly IGenericRepository<EstateOption> _estateOptionRepository;
        private readonly IGenericRepository<Photo> _imageRepository;
        public EstateService(IGenericRepository<Estate> estateRepository, IGenericRepository<EstateOption> estateOptionRepository, IGenericRepository<Photo> imageRepository)
        {
            _estateRepository = estateRepository;
            _estateOptionRepository = estateOptionRepository;
            _imageRepository = imageRepository;
        }
        public async Task<IEnumerable<Estate>> GetAllAsync()
        {
            return await _estateRepository.GetAllAsync(
                e => e.AgentUser,
                e => e.BuildingPlan,
                e => e.BuildingType,
                e => e.Category,
                e => e.EstateCondition,
                e => e.EstateOptions,
                e => e.Photos,
                e => e.Zone);
        }

        public async Task<Estate> GetByIdAsync(Guid id)
        {
            return await _estateRepository.GetByIdAsync(id,
                e => e.AgentUser,
                e => e.BuildingPlan,
                e => e.BuildingType,
                e => e.Category,
                e => e.EstateCondition,
                e => e.EstateOptions,
                e => e.Photos,
                e => e.Zone);
        }

        public async Task<bool> AddAsync(Estate estate)
        {
            return await _estateRepository.AddAsync(estate);
        }

        public async Task<bool> UpdateAsync(EditEstateViewModel editEstateViewModel)
        {
            var setIds = editEstateViewModel.EstateOptionViewModels.Where(eo => eo.IsSet == true).Select(eo => eo.Id);

            var estate = await GetByIdAsync(editEstateViewModel.Id);
            estate.EstateOptions = (List<EstateOption>)await _estateOptionRepository.GetAllAsync(o => setIds.Contains(o.Id));
            estate.Photos = (List<Photo>)await _imageRepository.GetAllAsync(i => editEstateViewModel.PhotoNames.Contains(i.FileTitle));
            estate.SetValues(editEstateViewModel);

            return await _estateRepository.UpdateAsync(estate);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                return await _estateRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
