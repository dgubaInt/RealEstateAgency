using RealEstateAgency.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAgency.Core.Entities
{
    public class Category : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Position { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public Guid? ParentCategoryId { get; set; }

        [ForeignKey("ParentCategoryId")]
        public virtual Category? ParentCategory { get; set; }
        public virtual List<Estate> Estates { get; set; }

        public Category()
        {
            Estates = new List<Estate>();
        }
    }
}
