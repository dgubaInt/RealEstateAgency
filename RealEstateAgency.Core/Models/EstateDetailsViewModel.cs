using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Models
{
    public class EstateDetailsViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Estate Name")]
        public string EstateName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Tags { get; set; }
        public int Rooms { get; set; }

        [Display(Name = "Bath Rooms")]
        public int BathRooms { get; set; }
        public int Balconies { get; set; }

        [Display(Name = "Parking Spaces")]
        public int ParkingSpaces { get; set; }

        [Display(Name = "Total Area")]
        public double TotalArea { get; set; }

        [Display(Name = "Living Area")]
        public double LivingArea { get; set; }

        [Display(Name = "Kitchen Area")]
        public double KitchenArea { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Agent")]
        public string AgentUserName { get; set; }

        [Display(Name = "Building Plan")]
        public string BuildingPlanName { get; set; }

        [Display(Name = "Building Type")]
        public string BuildingTypeName { get; set; }

        [Display(Name = "Zone")]
        public string ZoneName { get; set; }

        [Display(Name = "Estate Condition")]
        public string EstateConditionName { get; set; }

        [Display(Name = "Estate Options")]
        public string EstateOptions { get; set; }
    }
}