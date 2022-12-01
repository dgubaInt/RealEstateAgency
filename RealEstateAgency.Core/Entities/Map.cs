using RealEstateAgency.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Entities
{
    public class Map : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string MapName { get; set; }

        [Required]
        public string EstateAddress { get; set; }
        public string Description { get; set; }
    }
}
