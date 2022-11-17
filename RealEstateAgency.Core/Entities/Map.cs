using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.Entities
{
    public class Map
    {
        [Key]
        public Guid MapId { get; set; }

        [Required]
        [StringLength(50)]
        public string MapName { get; set; }

        [Required]
        public string EstateAddress { get; set; }
        public string Description { get; set; }
    }
}
