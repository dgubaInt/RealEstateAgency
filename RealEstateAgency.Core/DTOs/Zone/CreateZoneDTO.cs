namespace RealEstateAgency.Core.DTOs.Zone
{
    public class CreateZoneDTO
    {
        public string ZoneName { get; set; }
        public Guid ParentZoneId { get; set; }
    }
}
