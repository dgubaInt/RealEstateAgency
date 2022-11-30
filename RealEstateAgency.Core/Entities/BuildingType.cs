using System.ComponentModel.DataAnnotations;

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
