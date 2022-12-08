using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Core.DTOs.BuildingType;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Service.Mappers;

namespace RealEstateAgencyMVC.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class BuildingTypesController : Controller
    {
        private readonly IBuildingTypeService _buildingTypeService;

        public BuildingTypesController(IBuildingTypeService buildingTypeService)
        {
            _buildingTypeService = buildingTypeService;
        }

        // GET: api/BuildingTypes
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> GetBuildingTypes()
        {
            try
            {
                var buildingTypes = (await _buildingTypeService.GetAllAsync())
                    .Select(buildingType => buildingType.ToDTO()).ToList();

                return Json(new { Result = "OK", Records = buildingTypes, TotalRecordCount = buildingTypes.Count });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // POST: api/BuildingTypes
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> PostBuildingType([FromForm] CreateBuildingTypeDTO postBuildingTypeDTO)
        {
            try
            {
                var buildingType = await _buildingTypeService.AddAsync(postBuildingTypeDTO);
                return Json(new { Result = "OK", Record = buildingType });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // PUT: api/BuildingTypes/{id}
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> PutBuildingType([FromForm] BuildingTypeDTO buildingTypeDTO)
        {
            try
            {
                var buildingType = await _buildingTypeService.GetByIdAsync(buildingTypeDTO.Id);
                if (buildingType != null)
                {
                    buildingType.SetValues(buildingTypeDTO);
                    await _buildingTypeService.UpdateAsync(buildingType);

                }

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // DELETE: api/BuildingTypes/{id}
        [HttpDelete("{id}"), Route("[action]")]
        public async Task<IActionResult> DeleteBuildingType([FromForm] Guid id)
        {
            try
            {
                await _buildingTypeService.DeleteAsync(id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
