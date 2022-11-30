using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Entities
{
    public class EstateOption
    {
        [Key]
        public Guid EstateOptionId { get; set; }

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
