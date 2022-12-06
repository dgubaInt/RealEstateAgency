using RealEstateAgency.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IEstateService
    {
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Estate>> GetAllAsync();
        Task<Estate> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Estate estate);
    }
}
