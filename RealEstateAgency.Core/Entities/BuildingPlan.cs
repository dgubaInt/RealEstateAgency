using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Entities
{
    public class BuildingPlan
    {
        [Key]
        public Guid BuildingPlanId { get; set; }

        [Required]
        [StringLength(30)]
        public string BuildingPlanName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public virtual HashSet<Estate> Estates { get; set; }

        public BuildingPlan()
        {
            Estates = new HashSet<Estate>();
        }
    }
}
