using RealEstateAgency.Core.DTOs.Zone;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;

namespace RealEstateAgency.Service.ZoneService
{
    public class ZoneService : IZoneService
    {
        private readonly IGenericRepository<Zone> _zoneRepository;

        public ZoneService(IGenericRepository<Zone> zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        public async Task<IEnumerable<Zone>> GetAllAsync()
        {
            return await _zoneRepository.GetAllAsync();
        }

        public async Task<Zone> GetByIdAsync(Guid id)
        {
            return await _zoneRepository.GetByIdAsync(id);
        }

        public async Task<Zone> AddAsync(CreateZoneDTO postZoneDTO)
        {
            try
            {
                var parentZone = await GetByIdAsync(postZoneDTO.ParentZoneId);

                var zone = new Zone
                {
                    Id = Guid.NewGuid(),
                    ZoneName = postZoneDTO.ZoneName,
                    ParentZone = parentZone,
                    CreatedDate = DateTime.Now
                };
                if (await _zoneRepository.AddAsync(zone))
                    return zone;
                return default;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(Zone zone)
        {
            return await _zoneRepository.UpdateAsync(zone);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                return await _zoneRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
