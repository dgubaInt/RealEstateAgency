using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Entities
{
    public class EstateCondition
    {
        [Key]
        public Guid EstateConditionId { get; set; }

        [Required]
        [StringLength(50)]
        public string EstateConditionName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public HashSet<Estate> Estates { get; set; }

        public EstateCondition()
        {
            Estates = new HashSet<Estate>(); 
        }
    }
}
