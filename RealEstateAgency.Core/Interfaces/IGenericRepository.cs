using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(string id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(string id);
    }
}
