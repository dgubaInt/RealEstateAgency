using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Core.DTOs.EstateCondition;
using RealEstateAgency.Core.DTOs.EstateOption;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgencyMVC.Mappers;

namespace RealEstateAgencyMVC.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class EstateConditionsController : Controller
    {
        private readonly IEstateConditionService _estateConditionService;

        public EstateConditionsController(IEstateConditionService estateConditionService)
        {
            _estateConditionService = estateConditionService;
        }

        // GET: api/EstateConditions
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> GetEstateConditions()
        {
            try
            {
                var estateConditions = (await _estateConditionService.GetAllAsync())
                    .Select(estateCondition => estateCondition.ToDTO()).ToList();
                
                return Json(new { Result = "OK", Records = estateConditions, TotalRecordCount = estateConditions.Count });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // POST: api/EstateConditions
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> PostEstateCondition([FromForm] CreateEstateConditionDTO postEstateConditionDTO)
        {
            try
            {
                var estateCondition = await _estateConditionService.AddAsync(postEstateConditionDTO);
                return Json(new { Result = "OK", Record = estateCondition });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // PUT: api/EstateConditions/{id}
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> PutEstateCondition([FromForm] EstateConditionDTO estateConditionDTO)
        {
            try
            {
                var estateCondition = await _estateConditionService.GetByIdAsync(estateConditionDTO.EstateConditionId);
                if (estateCondition != null)
                {
                    estateCondition.SetValues(estateConditionDTO);
                    await _estateConditionService.UpdateAsync(estateCondition);

                }

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // DELETE: api/EstateConditions/{id}
        [HttpDelete("{id}"), Route("[action]")]
        public async Task<IActionResult> DeleteEstateCondition([FromForm] Guid estateConditionId)
        {
            try
            {
                await _estateConditionService.DeleteAsync(estateConditionId);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
