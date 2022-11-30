using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Core.DTOs.EstateCondition;
using RealEstateAgency.Core.Interfaces;

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
                var estateConditions = await _estateConditionService.GetAllAsync();

                var estateConditionsList = new List<EstateConditionDTO>();

                foreach (var estateCondition in estateConditions)
                {
                    estateConditionsList.Add(new EstateConditionDTO
                    {
                        EstateConditionId = estateCondition.EstateConditionId,
                        EstateConditionName = estateCondition.EstateConditionName
                    });
                }

                return Json(new { Result = "OK", Records = estateConditionsList, TotalRecordCount = estateConditionsList.Count });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // GET: api/EstateConditions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EstateConditionDTO>> GetEstateCondition(Guid id)
        {
            var estateCondition = await _estateConditionService.GetByIdAsync(id);

            if (estateCondition == null)
            {
                return NotFound();
            }

            return Ok(new EstateConditionDTO
            {
                EstateConditionId = estateCondition.EstateConditionId,
                EstateConditionName = estateCondition.EstateConditionName
            });
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
                    estateCondition.EstateConditionName = estateConditionDTO.EstateConditionName;
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
