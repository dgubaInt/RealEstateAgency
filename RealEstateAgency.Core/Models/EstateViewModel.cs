using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Models
{
    public class EstateViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required, Display(Name = "Estate name")]
        public string EstateName { get; set; }
        public string Address { get; set; }
        public string? Agent { get; set; }
    }
}
