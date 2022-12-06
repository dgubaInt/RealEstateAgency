using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Core.DTOs.Zone;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgencyMVC.Mappers;

namespace RealEstateAgencyMVC.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ZonesController : Controller
    {
        private readonly IZoneService _zoneService;

        public ZonesController(IZoneService zoneService)
        {
            _zoneService = zoneService;
        }

        // GET: api/Zones
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> GetZones()
        {
            try
            {
                var zones = (await _zoneService.GetAllAsync())
                    .Select(zone => zone.ToDTO()).ToList();

                return Json(new { Result = "OK", Records = zones, TotalRecordCount = zones.Count });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // POST: api/Zones
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> PostZone([FromForm] CreateZoneDTO postZoneDTO)
        {
            try
            {
                var zone = await _zoneService.AddAsync(postZoneDTO);
                return Json(new { Result = "OK", Record = zone });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // PUT: api/Zones/{id}
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> PutZone([FromForm] ZoneDTO zoneDTO)
        {
            try
            {
                var zone = await _zoneService.GetByIdAsync(zoneDTO.Id);
                if (zone != null)
                {
                    zone.SetValues(zoneDTO);
                    await _zoneService.UpdateAsync(zone);

                }

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // DELETE: api/Zones/{id}
        [HttpDelete("{id}"), Route("[action]")]
        public async Task<IActionResult> DeleteZone([FromForm] Guid id)
        {
            try
            {
                await _zoneService.DeleteAsync(id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
