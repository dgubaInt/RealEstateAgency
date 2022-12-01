using RealEstateAgency.Core.Entities;
using System.Linq.Expressions;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class, IBaseEntity
    {
        Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteAsync(T entity);
    }
}
