using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAgency.Core.Entities
{
    public class Estate
    {
        [Key]
        public Guid EstateId { get; set; }

        [Required]
        [StringLength(50)]
        public string EstateName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        public string Tags { get; set; }

        [Required]
        public int Rooms { get; set; }

        [Required]
        public int BathRooms { get; set; }

        [Required]
        public int Balconies { get; set; }

        [Required]
        public int ParkingSpaces { get; set; }

        [Required]
        public double TotalArea { get; set; }

        [Required]
        public double LivingArea { get; set; }

        [Required]
        public double KitchenArea { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public virtual HashSet<EstateOption> EstateOptions { get; set; }
        public HashSet<Photo> Photos { get; set; }
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public Guid AgentUserId { get; set; }

        [ForeignKey("AgentUserId")]
        public virtual AgentUser AgentUser { get; set; }
        public Guid BuildingPlanId { get; set; }

        [ForeignKey("BuildingPlanId")]
        public virtual BuildingPlan BuildingPlan { get; set; }
        public Guid BuildingTypeId { get; set; }

        [ForeignKey("BuildingTypeId")]
        public virtual BuildingType BuildingType { get; set; }
        public Guid ZoneId { get; set; }

        [ForeignKey("ZoneId")]
        public virtual Zone Zone { get; set; }
        public Guid MapId { get; set; }

        [ForeignKey("MapId")]
        public virtual Map Map { get; set; }
        public Guid EstateConditionId { get; set; }

        [ForeignKey("EstateConditionId")]
        public virtual EstateCondition EstateCondition { get; set; }

        public Estate()
        {
            EstateOptions = new HashSet<EstateOption>();
            Photos = new HashSet<Photo>();
        }
    }
}
