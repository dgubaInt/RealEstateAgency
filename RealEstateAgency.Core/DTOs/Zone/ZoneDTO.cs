namespace RealEstateAgency.Core.DTOs.Zone
{
    public class ZoneDTO
    {
        public Guid Id { get; set; }
        public string ZoneName { get; set; }
        public Guid? ParentZoneId { get; set; }
    }
}
