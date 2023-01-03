using Microsoft.AspNetCore.Http;
using RealEstateAgency.Core.Resources;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Models
{
    public class AddEstateViewModel
    {
        [Display(Name = "EstateName")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        [StringLength(50, ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.LengthError), MinimumLength = 3)]
        public string EstateName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public string Description { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        [StringLength(100, ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.LengthError), MinimumLength = 3)]
        public string Address { get; set; }

        public string? Tags { get; set; }

        [Display(Name = "Rooms")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public int Rooms { get; set; }

        [Display(Name = "BathRooms")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public int BathRooms { get; set; }

        [Display(Name = "Balconies")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public int Balconies { get; set; }

        [Display(Name = "ParkingSpaces")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public int ParkingSpaces { get; set; }

        [Display(Name = "TotalArea")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public double TotalArea { get; set; }

        [Display(Name = "LivingArea")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public double LivingArea { get; set; }

        [Display(Name = "KitchenArea")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public double KitchenArea { get; set; }

        [RegularExpression("[0-9]+([.,][0-9]{1,3})?", ErrorMessage = nameof(UILabel.NumberError))]
        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public decimal Price { get; set; }

        [Display(Name = "Currency")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public string Currency { get; set; }

        [Display(Name = "CreatedDate")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public Guid CategoryId { get; set; }

        [Display(Name = "AgentUser")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public Guid AgentUserId { get; set; }

        [Display(Name = "BuildingPlan")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public Guid BuildingPlanId { get; set; }

        [Display(Name = "BuildingType")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public Guid BuildingTypeId { get; set; }

        [Display(Name = "Zone")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public Guid ZoneId { get; set; }

        [Display(Name = "EstateCondition")]
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        public Guid EstateConditionId { get; set; }

        public List<EstateOptionViewModel> EstateOptionViewModels { get; set; } = new List<EstateOptionViewModel>();
        public List<IFormFile> File { get; set; } = new List<IFormFile>();
    }
}
