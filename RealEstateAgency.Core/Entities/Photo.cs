using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Entities
{
    public class Photo
    {
        [Key]
        public Guid PhotoId { get; set; }

        [Required]
        [StringLength(100)]
        public string FileTitle { get; set; }
        public Guid EstateId { get; set; }
        public virtual Estate Estate { get; set; }
    }
}
