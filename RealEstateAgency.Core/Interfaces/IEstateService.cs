using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Models;
using System.Linq.Expressions;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IEstateService
    {
        Task<bool> AddAsync(Estate estate);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Estate>> GetAllAsync();
        Task<IEnumerable<Estate>> GetAllAsync(Expression<Func<Estate, bool>> filter);
        Task<Estate> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(EditEstateViewModel editEstateViewModel);
    }
}
