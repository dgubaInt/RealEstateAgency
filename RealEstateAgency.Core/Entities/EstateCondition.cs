using RealEstateAgency.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Entities
{
    public class EstateCondition : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string EstateConditionName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public List<Estate> Estates { get; set; }

        public EstateCondition()
        {
            Estates = new List<Estate>();
        }
    }
}
