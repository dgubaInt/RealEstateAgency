using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Models;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IEstateService
    {
        Task<bool> AddAsync(Estate estate);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Estate>> GetAllAsync();
        Task<Estate> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(EditEstateViewModel editEstateViewModel);
    }
}
