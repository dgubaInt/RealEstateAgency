using RealEstateAgency.Core.DTOs.BuildingPlan;
using RealEstateAgency.Core.DTOs.BuildingType;
using RealEstateAgency.Core.DTOs.Category;
using RealEstateAgency.Core.DTOs.EstateCondition;
using RealEstateAgency.Core.DTOs.EstateOption;
using RealEstateAgency.Core.DTOs.Zone;
using RealEstateAgency.Core.Entities;

namespace RealEstateAgencyMVC.Mappers
{
    public static class Mapper
    {
        public static BuildingTypeDTO ToDTO(this BuildingType buildingType)
        {
            return new BuildingTypeDTO { 
                BuildingTypeId = buildingType.Id,
                BuildingTypeName = buildingType.BuildingTypeName 
            };
        }

        public static void SetValues(this BuildingType buildingType, BuildingTypeDTO buildingTypeDTO)
        {
            buildingType.BuildingTypeName = buildingTypeDTO.BuildingTypeName;
        }

        public static BuildingPlanDTO ToDTO(this BuildingPlan buildingType)
        {
            return new BuildingPlanDTO
            {
                BuildingPlanId = buildingType.Id,
                BuildingPlanName = buildingType.BuildingPlanName
            };
        }

        public static void SetValues(this BuildingPlan buildingType, BuildingPlanDTO buildingTypeDTO)
        {
            buildingType.BuildingPlanName = buildingTypeDTO.BuildingPlanName;
        }

        public static EstateOptionDTO ToDTO(this EstateOption estateOption)
        {
            return new EstateOptionDTO
            {
                EstateOptionId = estateOption.Id,
                EstateOptionName = estateOption.EstateOptionName
            };
        }

        public static void SetValues(this EstateOption estateOption, EstateOptionDTO estateOptionDTO)
        {
            estateOption.EstateOptionName = estateOptionDTO.EstateOptionName;
        }

        public static EstateConditionDTO ToDTO(this EstateCondition estateCondition)
        {
            return new EstateConditionDTO
            {
                EstateConditionId = estateCondition.Id,
                EstateConditionName = estateCondition.EstateConditionName
            };
        }

        public static void SetValues(this EstateCondition estateCondition, EstateConditionDTO estateConditionDTO)
        {
            estateCondition.EstateConditionName = estateConditionDTO.EstateConditionName;
        }

        public static ZoneDTO ToDTO(this Zone zone)
        {
            return new ZoneDTO
            {
                ZoneId = zone.Id,
                ZoneName = zone.ZoneName
            };
        }

        public static void SetValues(this Zone zone, ZoneDTO zoneDTO)
        {
            zone.ZoneName = zoneDTO.ZoneName;
        }

        public static CategoryDTO ToDTO(this Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                ParentCategoryId = category.ParentCategoryId,
                Position = category.Position
            };
        }

        public static void SetValues(this Category category, CategoryDTO categoryDTO)
        {
            category.CategoryName = categoryDTO.CategoryName;
            category.ParentCategoryId = categoryDTO.ParentCategoryId;
            category.Position = categoryDTO.Position;
        }
    }
}
