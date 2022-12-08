using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Models
{
    public class AddEstateViewModel
    {
        [Required, Display(Name = "Estate Name")]
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

        [Required, Display(Name = "Bath Rooms")]
        public int BathRooms { get; set; }

        [Required]
        public int Balconies { get; set; }

        [Required, Display(Name = "Parking Spaces")]
        public int ParkingSpaces { get; set; }

        [Required, Display(Name = "Total Area")]
        public double TotalArea { get; set; }

        [Required, Display(Name = "Living Area")]
        public double LivingArea { get; set; }

        [Required, Display(Name = "Kitchen Area")]
        public double KitchenArea { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required, Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Required, Display(Name = "Category")]
        public Guid CategoryId { get; set; }

        [Required, Display(Name = "Agent")]
        public Guid AgentUserId { get; set; }

        [Required, Display(Name = "Building Plan")]
        public Guid BuildingPlanId { get; set; }

        [Required, Display(Name = "Building Type")]
        public Guid BuildingTypeId { get; set; }

        [Required, Display(Name = "Zone")]
        public Guid ZoneId { get; set; }

        [Required, Display(Name = "Estate Condition")]
        public Guid EstateConditionId { get; set; }

        public List<EstateOptionViewModel> EstateOptionViewModels { get; set; } = new List<EstateOptionViewModel>();
    }
}
