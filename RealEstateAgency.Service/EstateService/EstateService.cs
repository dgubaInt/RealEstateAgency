using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                e => e.Map,
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
                e => e.Map,
                e => e.Zone);
        }
    }
}
