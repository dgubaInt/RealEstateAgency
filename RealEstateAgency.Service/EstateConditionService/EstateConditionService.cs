using RealEstateAgency.Core.DTOs.EstateCondition;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;

namespace RealEstateAgency.Service.EstateConditionService
{
    public class EstateConditionService : IEstateConditionService
    {
        private readonly IGenericRepository<EstateCondition> _estateConditionRepository;

        public EstateConditionService(IGenericRepository<EstateCondition> estateConditionRepository)
        {
            _estateConditionRepository = estateConditionRepository;
        }

        public async Task<IEnumerable<EstateCondition>> GetAllAsync()
        {
            return await _estateConditionRepository.GetAllAsync();
        }

        public async Task<EstateCondition> GetByIdAsync(Guid id)
        {
            return await _estateConditionRepository.GetByIdAsync(id);
        }

        public async Task<EstateCondition> AddAsync(CreateEstateConditionDTO postEstateConditionDTO)
        {
            try
            {
                var estateCondition = new EstateCondition
                {
                    Id = Guid.NewGuid(),
                    EstateConditionName = postEstateConditionDTO.EstateConditionName,
                    CreatedDate = DateTime.Now
                };
                if (await _estateConditionRepository.AddAsync(estateCondition))
                    return estateCondition;
                return default;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(EstateCondition estateCondition)
        {
            return await _estateConditionRepository.UpdateAsync(estateCondition);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                return await _estateConditionRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
