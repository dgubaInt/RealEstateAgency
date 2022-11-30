using RealEstateAgency.Core.DTOs.Zone;
using RealEstateAgency.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IZoneService
    {
        Task<Zone> AddAsync(CreateZoneDTO postZoneDTO);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Zone>> GetAllAsync();
        Task<Zone> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Zone zone);
    }
}
