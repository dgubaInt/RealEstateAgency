using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Core.DTOs.Zone;
using RealEstateAgency.Core.Interfaces;

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
                var zones = await _zoneService.GetAllAsync();

                var zonesList = new List<ZoneDTO>();

                foreach (var zone in zones)
                {
                    zonesList.Add(new ZoneDTO
                    {
                        ZoneId = zone.ZoneId,
                        ZoneName = zone.ZoneName
                    });
                }

                return Json(new { Result = "OK", Records = zonesList, TotalRecordCount = zonesList.Count });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // GET: api/Zones/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ZoneDTO>> GetZone(Guid id)
        {
            var zone = await _zoneService.GetByIdAsync(id);

            if (zone == null)
            {
                return NotFound();
            }

            return Ok(new ZoneDTO
            {
                ZoneId = zone.ZoneId,
                ZoneName = zone.ZoneName
            });
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
                var zone = await _zoneService.GetByIdAsync(zoneDTO.ZoneId);
                if (zone != null)
                {
                    zone.ZoneName = zoneDTO.ZoneName;
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
        public async Task<IActionResult> DeleteZone([FromForm] Guid zoneId)
        {
            try
            {
                await _zoneService.DeleteAsync(zoneId);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
