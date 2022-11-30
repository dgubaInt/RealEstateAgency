using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Core.DTOs.BuildingType;
using RealEstateAgency.Core.Interfaces;

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
                var buildingTypes = await _buildingTypeService.GetAllAsync();

                var buildingTypesList = new List<BuildingTypeDTO>();

                foreach (var buildingType in buildingTypes)
                {
                    buildingTypesList.Add(new BuildingTypeDTO
                    {
                        BuildingTypeId = buildingType.BuildingTypeId,
                        BuildingTypeName = buildingType.BuildingTypeName
                    });
                }

                return Json(new { Result = "OK", Records = buildingTypesList, TotalRecordCount = buildingTypesList.Count });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // GET: api/BuildingTypes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingTypeDTO>> GetBuildingType(Guid id)
        {
            var buildingType = await _buildingTypeService.GetByIdAsync(id);

            if (buildingType == null)
            {
                return NotFound();
            }

            return Ok(new BuildingTypeDTO
            {
                BuildingTypeId = buildingType.BuildingTypeId,
                BuildingTypeName = buildingType.BuildingTypeName
            });
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
                var buildingType = await _buildingTypeService.GetByIdAsync(buildingTypeDTO.BuildingTypeId);
                if (buildingType != null)
                {
                    buildingType.BuildingTypeName = buildingTypeDTO.BuildingTypeName;
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
        public async Task<IActionResult> DeleteBuildingType([FromForm] Guid buildingTypeId)
        {
            try
            {
                await _buildingTypeService.DeleteAsync(buildingTypeId);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
