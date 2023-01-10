using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Core.DTOs.BuildingPlan;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Service.Mappers;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RealEstateAgency.UnitTests")]
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
                var buildingPlans = (await _buildingPlanService.GetAllAsync())
                    .Select(buildingPlan => buildingPlan.ToDTO()).ToList();

                return Json(new { Result = "OK", Records = buildingPlans, TotalRecordCount = buildingPlans.Count });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // POST: api/BuildingPlans
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> PostBuildingPlan([FromForm] CreateBuildingPlanDTO postBuildingPlanDTO)
        {
            try
            {
                var buildingPlan = await _buildingPlanService.AddAsync(postBuildingPlanDTO);

                if (buildingPlan is not null)
                {
                    return Json(new { Result = "OK", Message = "OK" });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = "ERROR" });
                }
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
                var buildingPlan = await _buildingPlanService.GetByIdAsync(buildingPlanDTO.Id);
                if (buildingPlan != null)
                {
                    buildingPlan.SetValues(buildingPlanDTO);
                    await _buildingPlanService.UpdateAsync(buildingPlan);
                    return Json(new { Result = "OK" });
                }
                return Json(new { Result = "ERROR", Message = "ERROR" });

            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // DELETE: api/BuildingPlans/{id}
        [HttpDelete("{id}"), Route("[action]")]
        public async Task<JsonResult> DeleteBuildingPlan([FromForm] Guid id)
        {
            try
            {
                if (await _buildingPlanService.DeleteAsync(id))
                    return Json(new { Result = "OK" });
                return Json(new { Result = "ERROR", Message = "ERROR" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
