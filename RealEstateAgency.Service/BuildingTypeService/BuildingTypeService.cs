using RealEstateAgency.Core.DTOs.BuildingType;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;

namespace RealEstateAgency.Service.BuildingTypeService
{
    public class BuildingTypeService : IBuildingTypeService
    {
        private readonly IGenericRepository<BuildingType> _buildingTypeRepository;

        public BuildingTypeService(IGenericRepository<BuildingType> buildingTypeRepository)
        {
            _buildingTypeRepository = buildingTypeRepository;
        }

        public async Task<IEnumerable<BuildingType>> GetAllAsync()
        {
            return await _buildingTypeRepository.GetAllAsync();
        }

        public async Task<BuildingType> GetByIdAsync(Guid id)
        {
            return await _buildingTypeRepository.GetByIdAsync(id);
        }

        public async Task<BuildingType> AddAsync(CreateBuildingTypeDTO postBuildingTypeDTO)
        {
            try
            {
                var buildingType = new BuildingType
                {
                    BuildingTypeId = Guid.NewGuid(),
                    BuildingTypeName = postBuildingTypeDTO.BuildingTypeName,
                    CreatedDate = DateTime.Now
                }; 
                await _buildingTypeRepository.AddAsync(buildingType);
                return buildingType;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(BuildingType buildingType)
        {
            return await _buildingTypeRepository.UpdateAsync(buildingType);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                return await _buildingTypeRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
