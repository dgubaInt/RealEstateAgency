using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Core.DTOs.EstateOption;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Service.Mappers;

namespace RealEstateAgencyMVC.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class EstateOptionsController : Controller
    {
        private readonly IEstateOptionService _estateOptionService;

        public EstateOptionsController(IEstateOptionService estateOptionService)
        {
            _estateOptionService = estateOptionService;
        }

        // GET: api/EstateOptions
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> GetEstateOptions()
        {
            try
            {
                var estateOptions = (await _estateOptionService.GetAllAsync())
                    .Select(estateOption => estateOption.ToDTO()).ToList();

                return Json(new { Result = "OK", Records = estateOptions, TotalRecordCount = estateOptions.Count });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // POST: api/EstateOptions
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> PostEstateOption([FromForm] CreateEstateOptionDTO postEstateOptionDTO)
        {
            try
            {
                var estateOption = await _estateOptionService.AddAsync(postEstateOptionDTO);
                return Json(new { Result = "OK", Record = estateOption });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // PUT: api/EstateOptions/{id}
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> PutEstateOption([FromForm] EstateOptionDTO estateOptionDTO)
        {
            try
            {
                var estateOption = await _estateOptionService.GetByIdAsync(estateOptionDTO.Id);
                if (estateOption != null)
                {
                    estateOption.SetValues(estateOptionDTO);
                    await _estateOptionService.UpdateAsync(estateOption);

                }

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // DELETE: api/EstateOptions/{id}
        [HttpDelete("{id}"), Route("[action]")]
        public async Task<IActionResult> DeleteEstateOption([FromForm] Guid id)
        {
            try
            {
                await _estateOptionService.DeleteAsync(id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
