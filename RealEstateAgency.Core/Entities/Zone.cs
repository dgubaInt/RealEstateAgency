using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.Entities
{
    public class Zone
    {
        [Key]
        public Guid ZoneId { get; set; }

        [Required]
        [StringLength(30)]
        public string ZoneName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("ParentZoneId")]
        public HashSet<Zone> Zones { get; set; }
        public HashSet<Estate> Estates { get; set; }

        public Zone()
        {
            Zones = new HashSet<Zone>();
            Estates = new HashSet<Estate>();
        }
    }
}
