using RealEstateAgency.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Entities
{
    public class Photo : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FileTitle { get; set; }
        public Guid EstateId { get; set; }
        public virtual Estate Estate { get; set; }
    }
}
