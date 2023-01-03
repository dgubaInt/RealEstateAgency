using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Models
{
    public class EstateViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required, Display(Name = "EstateName")]
        public string EstateName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Agent")]
        public string? Agent { get; set; }
    }
}
