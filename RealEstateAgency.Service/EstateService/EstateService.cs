using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;

namespace RealEstateAgency.Service.EstateService
{
    public class EstateService : IEstateService
    {
        private readonly IGenericRepository<Estate> _estateRepository;
        public EstateService(IGenericRepository<Estate> estateRepository)
        {
            _estateRepository = estateRepository;
        }
        public async Task<IEnumerable<Estate>> GetAllAsync()
        {
            return await _estateRepository.GetAllAsync(
                e => e.AgentUser,
                e => e.BuildingPlan,
                e => e.BuildingType,
                e => e.Category,
                e => e.EstateCondition,
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
                e => e.Zone);
        }

        public async Task<bool> UpdateAsync(Estate estate)
        {
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
