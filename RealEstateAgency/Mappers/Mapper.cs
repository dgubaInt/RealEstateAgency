using RealEstateAgency.Core.DTOs.BuildingPlan;
using RealEstateAgency.Core.DTOs.BuildingType;
using RealEstateAgency.Core.DTOs.Category;
using RealEstateAgency.Core.DTOs.EstateCondition;
using RealEstateAgency.Core.DTOs.EstateOption;
using RealEstateAgency.Core.DTOs.Zone;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Models;

namespace RealEstateAgencyMVC.Mappers
{
    public static class Mapper
    {
        public static BuildingTypeDTO ToDTO(this BuildingType buildingType)
        {
            return new BuildingTypeDTO
            {
                Id = buildingType.Id,
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
                Id = buildingType.Id,
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
                Id = estateOption.Id,
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
                Id = estateCondition.Id,
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
                Id = zone.Id,
                ZoneName = zone.ZoneName,
                ParentZoneId = zone.ParentZoneId

            };
        }

        public static void SetValues(this Zone zone, ZoneDTO zoneDTO)
        {
            zone.ZoneName = zoneDTO.ZoneName;
            zone.ParentZoneId = zoneDTO.ParentZoneId;
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

        public static Estate ToEntity(this AddEstateViewModel addEstateViewModel)
        {
            var estate = new Estate
            {
                Id = Guid.NewGuid(),
                EstateName = addEstateViewModel.EstateName,
                Description = addEstateViewModel.Description,
                Address = addEstateViewModel.Address,
                Tags = addEstateViewModel.Tags,
                Rooms = addEstateViewModel.Rooms,
                BathRooms = addEstateViewModel.BathRooms,
                Balconies = addEstateViewModel.Balconies,
                ParkingSpaces = addEstateViewModel.ParkingSpaces,
                TotalArea = addEstateViewModel.TotalArea,
                LivingArea = addEstateViewModel.LivingArea,
                KitchenArea = addEstateViewModel.KitchenArea,
                Price = addEstateViewModel.Price,
                Currency = addEstateViewModel.Currency,
                CreatedDate = addEstateViewModel.CreatedDate,
                CategoryId = addEstateViewModel.CategoryId,
                AgentUserId = addEstateViewModel.AgentUserId,
                BuildingPlanId = addEstateViewModel.BuildingPlanId,
                BuildingTypeId = addEstateViewModel.BuildingTypeId,
                ZoneId = addEstateViewModel.ZoneId,
                EstateConditionId = addEstateViewModel.EstateConditionId
            };

            return estate;
        }

        public static Estate ToEntity(this EditEstateViewModel editEstateViewModel)
        {
            var estate = new Estate
            {
                Id = editEstateViewModel.Id,
                EstateName = editEstateViewModel.EstateName,
                Description = editEstateViewModel.Description,
                Address = editEstateViewModel.Address,
                Tags = editEstateViewModel.Tags,
                Rooms = editEstateViewModel.Rooms,
                BathRooms = editEstateViewModel.BathRooms,
                Balconies = editEstateViewModel.Balconies,
                ParkingSpaces = editEstateViewModel.ParkingSpaces,
                TotalArea = editEstateViewModel.TotalArea,
                LivingArea = editEstateViewModel.LivingArea,
                KitchenArea = editEstateViewModel.KitchenArea,
                Price = editEstateViewModel.Price,
                Currency = editEstateViewModel.Currency,
                CreatedDate = editEstateViewModel.CreatedDate,
                CategoryId = editEstateViewModel.CategoryId,
                AgentUserId = editEstateViewModel.AgentUserId,
                BuildingPlanId = editEstateViewModel.BuildingPlanId,
                BuildingTypeId = editEstateViewModel.BuildingTypeId,
                ZoneId = editEstateViewModel.ZoneId,
                EstateConditionId = editEstateViewModel.EstateConditionId
            };

            return estate;
        }

        public static EditEstateViewModel ToEditViewModel(this Estate estate)
        {
            return new EditEstateViewModel
            {
                Id = estate.Id,
                EstateName = estate.EstateName,
                Description = estate.Description,
                Address = estate.Address,
                Tags = estate.Tags,
                Rooms = estate.Rooms,
                BathRooms = estate.BathRooms,
                Balconies = estate.Balconies,
                ParkingSpaces = estate.ParkingSpaces,
                TotalArea = estate.TotalArea,
                LivingArea = estate.LivingArea,
                KitchenArea = estate.KitchenArea,
                Price = estate.Price,
                Currency = estate.Currency,
                CreatedDate = estate.CreatedDate,
                CategoryId = estate.CategoryId,
                AgentUserId = estate.AgentUserId,
                BuildingPlanId = estate.BuildingPlanId,
                BuildingTypeId = estate.BuildingTypeId,
                ZoneId = estate.ZoneId,
                EstateConditionId = estate.EstateConditionId
            };
        }

        public static EstateDetailsViewModel ToDetailsViewModel(this Estate estate)
        {
            return new EstateDetailsViewModel
            {
                Id = estate.Id,
                EstateName = estate.EstateName,
                Description = estate.Description,
                Address = estate.Address,
                Tags = estate.Tags,
                Rooms = estate.Rooms,
                BathRooms = estate.BathRooms,
                Balconies = estate.Balconies,
                ParkingSpaces = estate.ParkingSpaces,
                TotalArea = estate.TotalArea,
                LivingArea = estate.LivingArea,
                KitchenArea = estate.KitchenArea,
                Price = estate.Price,
                Currency = estate.Currency,
                CreatedDate = estate.CreatedDate,
                CategoryName = estate.Category.CategoryName,
                AgentUserName = estate.AgentUser.UserName,
                BuildingPlanName = estate.BuildingPlan.BuildingPlanName,
                BuildingTypeName = estate.BuildingType.BuildingTypeName,
                ZoneName = estate.Zone.ZoneName,
                EstateConditionName = estate.EstateCondition.EstateConditionName
            };
        }

        public static EstateViewModel ToViewModel(this Estate estate)
        {
            return new EstateViewModel
            {
                Id = estate.Id,
                EstateName = estate.EstateName,
                Address = estate.Address,
                Agent = estate.AgentUser.UserName
            };
        }
    }
}
