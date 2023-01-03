using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Models
{
    public class EstateDetailsViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "EstateName")]
        public string EstateName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Tags")]
        public string Tags { get; set; }

        [Display(Name = "Rooms")]
        public int Rooms { get; set; }

        [Display(Name = "BathRooms")]
        public int BathRooms { get; set; }

        [Display(Name = "Balconies")]
        public int Balconies { get; set; }

        [Display(Name = "ParkingSpaces")]
        public int ParkingSpaces { get; set; }

        [Display(Name = "TotalArea")]
        public double TotalArea { get; set; }

        [Display(Name = "LivingArea")]
        public double LivingArea { get; set; }

        [Display(Name = "KitchenArea")]
        public double KitchenArea { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "AgentUser")]
        public string AgentUserName { get; set; }

        [Display(Name = "BuildingPlan")]
        public string BuildingPlanName { get; set; }

        [Display(Name = "BuildingType")]
        public string BuildingTypeName { get; set; }

        [Display(Name = "Zone")]
        public string ZoneName { get; set; }

        [Display(Name = "EstateCondition")]
        public string EstateConditionName { get; set; }

        [Display(Name = "EstateOptions")]
        public string EstateOptions { get; set; }
        public List<string> Photos { get; set; } = new List<string>();
    }
}