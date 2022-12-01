using RealEstateAgency.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAgency.Core.Entities
{
    public class Zone : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(30)]
        public string ZoneName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public Guid? ParentZoneId { get; set; }

        [ForeignKey("ParentZoneId")]
        public Zone ParentZone { get; set; }
        public HashSet<Estate> Estates { get; set; }

        public Zone()
        {
            Estates = new HashSet<Estate>();
        }
    }
}
