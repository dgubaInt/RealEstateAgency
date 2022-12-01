using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.Models
{
    public class EstateViewModel
    {
        [Key]
        public Guid EstateId { get; set; }

        [Required, Display(Name = "Estate name")]
        public string EstateName { get; set; }

        [Required]
        public string Zone { get; set; }

        [Required, Display(Name = "Estate address")]
        public string EstateAddress { get; set; }

        [Required]
        public int Rooms { get; set; }

        [Required]
        public double TotalArea { get; set; }

        [Required]
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public string? Agent { get; set; }
    }
}
