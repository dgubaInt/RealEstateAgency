using RealEstateAgency.Core.DTOs.BuildingPlan;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;

namespace RealEstateAgency.Service.BuildingPlanService
{
    public class BuildingPlanService : IBuildingPlanService
    {
        private readonly IGenericRepository<BuildingPlan> _buildingPlanRepository;

        public BuildingPlanService(IGenericRepository<BuildingPlan> buildingPlanRepository)
        {
            _buildingPlanRepository = buildingPlanRepository;
        }

        public async Task<IEnumerable<BuildingPlan>> GetAllAsync()
        {
            return await _buildingPlanRepository.GetAllAsync();
        }

        public async Task<BuildingPlan> GetByIdAsync(Guid id)
        {
            return await _buildingPlanRepository.GetByIdAsync(id);
        }

        public async Task<BuildingPlan> AddAsync(CreateBuildingPlanDTO postBuildingPlanDTO)
        {
            try
            {
                var buildingPlan = new BuildingPlan
                {
                    Id = Guid.NewGuid(),
                    BuildingPlanName = postBuildingPlanDTO.BuildingPlanName,
                    CreatedDate = DateTime.Now
                };
                if (await _buildingPlanRepository.AddAsync(buildingPlan))
                    return buildingPlan;
                return default;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(BuildingPlan buildingPlan)
        {
            return await _buildingPlanRepository.UpdateAsync(buildingPlan);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                return await _buildingPlanRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
