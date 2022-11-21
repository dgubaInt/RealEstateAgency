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
