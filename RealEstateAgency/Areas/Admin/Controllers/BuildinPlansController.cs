using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Core.DTOs.BuildingPlan;
using RealEstateAgency.Core.Interfaces;

namespace RealEstateAgencyMVC.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class BuildingPlansController : Controller
    {
        private readonly IBuildingPlanService _buildingPlanService;

        public BuildingPlansController(IBuildingPlanService buildingPlanService)
        {
            _buildingPlanService = buildingPlanService;
        }

        // GET: api/BuildingPlans
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> GetBuildingPlans()
        {
            try
            {
                var buildingPlans = await _buildingPlanService.GetAllAsync();

                var buildingPlansList = new List<BuildingPlanDTO>();

                foreach (var buildingPlan in buildingPlans)
                {
                    buildingPlansList.Add(new BuildingPlanDTO
                    {
                        BuildingPlanId = buildingPlan.BuildingPlanId,
                        BuildingPlanName = buildingPlan.BuildingPlanName
                    });
                }

                return Json(new { Result = "OK", Records = buildingPlansList, TotalRecordCount = buildingPlansList.Count });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // GET: api/BuildingPlans/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingPlanDTO>> GetBuildingPlan(Guid id)
        {
            var buildingPlan = await _buildingPlanService.GetByIdAsync(id);

            if (buildingPlan == null)
            {
                return NotFound();
            }

            return Ok(new BuildingPlanDTO
            {
                BuildingPlanId = buildingPlan.BuildingPlanId,
                BuildingPlanName = buildingPlan.BuildingPlanName
            });
        }

        // POST: api/BuildingPlans
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> PostBuildingPlan([FromForm] CreateBuildingPlanDTO postBuildingPlanDTO)
        {
            try
            {
                var buildingPlan = await _buildingPlanService.AddAsync(postBuildingPlanDTO);
                return Json(new { Result = "OK", Record = buildingPlan });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // PUT: api/BuildingPlans/{id}
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> PutBuildingPlan([FromForm] BuildingPlanDTO buildingPlanDTO)
        {
            try
            {
                var buildingPlan = await _buildingPlanService.GetByIdAsync(buildingPlanDTO.BuildingPlanId);
                if (buildingPlan != null)
                {
                    buildingPlan.BuildingPlanName = buildingPlanDTO.BuildingPlanName;
                    await _buildingPlanService.UpdateAsync(buildingPlan);

                }

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // DELETE: api/BuildingPlans/{id}
        [HttpDelete("{id}"), Route("[action]")]
        public async Task<IActionResult> DeleteBuildingPlan([FromForm] Guid buildingPlanId)
        {
            try
            {
                await _buildingPlanService.DeleteAsync(buildingPlanId);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
