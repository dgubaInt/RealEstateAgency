using RealEstateAgency.Core.DTOs.EstateCondition;
using RealEstateAgency.Core.Entities;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IEstateConditionService
    {
        Task<EstateCondition> AddAsync(CreateEstateConditionDTO postEstateConditionDTO);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<EstateCondition>> GetAllAsync();
        Task<EstateCondition> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(EstateCondition estateCondition);
    }
}
