using RealEstateAgency.Core.DTOs.BuildingPlan;
using RealEstateAgency.Core.Entities;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IBuildingPlanService
    {
        Task<BuildingPlan> AddAsync(CreateBuildingPlanDTO postBuildingPlanDTO);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<BuildingPlan>> GetAllAsync();
        Task<BuildingPlan> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(BuildingPlan buildingPlan);
    }
}
