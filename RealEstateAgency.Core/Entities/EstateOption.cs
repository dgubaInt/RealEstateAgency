using RealEstateAgency.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Entities
{
    public class EstateOption : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(30)]
        public string EstateOptionName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public virtual HashSet<Estate> Estates { get; set; }

        public EstateOption()
        {
            Estates = new HashSet<Estate>();
        }
    }
}
