using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.Entities
{
    public class BuildingType
    {
        [Key]
        public Guid BuildingTypeId { get; set; }

        [Required]
        [StringLength(30)]
        public string BuildingTypeName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public virtual HashSet<Estate> Estates { get; set; }

        public BuildingType()
        {
            Estates = new HashSet<Estate>();
        }
    }
}
