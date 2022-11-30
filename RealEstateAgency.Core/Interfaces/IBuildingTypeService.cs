using RealEstateAgency.Core.DTOs.BuildingType;
using RealEstateAgency.Core.Entities;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IBuildingTypeService
    {
        Task<BuildingType> AddAsync(CreateBuildingTypeDTO postBuildingTypeDTO);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<BuildingType>> GetAllAsync();
        Task<BuildingType> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(BuildingType buildingPlan);
    }
}
