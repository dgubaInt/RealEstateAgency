using RealEstateAgency.Core.DTOs.EstateOption;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;

namespace RealEstateAgency.Service.EstateOptionService
{
    public class EstateOptionService : IEstateOptionService
    {
        private readonly IGenericRepository<EstateOption> _estateOptionRepository;

        public EstateOptionService(IGenericRepository<EstateOption> estateOptionRepository)
        {
            _estateOptionRepository = estateOptionRepository;
        }

        public async Task<IEnumerable<EstateOption>> GetAllAsync()
        {
            return await _estateOptionRepository.GetAllAsync();
        }

        public async Task<EstateOption> GetByIdAsync(Guid id)
        {
            return await _estateOptionRepository.GetByIdAsync(id);
        }

        public async Task<EstateOption> AddAsync(CreateEstateOptionDTO postEstateOptionDTO)
        {
            try
            {
                var estateOption = new EstateOption
                {
                    Id = Guid.NewGuid(),
                    EstateOptionName = postEstateOptionDTO.EstateOptionName,
                    CreatedDate = DateTime.Now
                };
                if (await _estateOptionRepository.AddAsync(estateOption))
                    return estateOption;
                return default;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(EstateOption estateOption)
        {
            return await _estateOptionRepository.UpdateAsync(estateOption);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                return await _estateOptionRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
