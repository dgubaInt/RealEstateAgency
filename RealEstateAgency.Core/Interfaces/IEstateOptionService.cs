using RealEstateAgency.Core.DTOs.EstateOption;
using RealEstateAgency.Core.Entities;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IEstateOptionService
    {
        Task<EstateOption> AddAsync(CreateEstateOptionDTO postEstateOptionDTO);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<EstateOption>> GetAllAsync();
        Task<EstateOption> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(EstateOption estateOption);
    }
}
