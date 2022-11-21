using Microsoft.AspNetCore.Identity;

namespace RealEstateAgency.Core.Models
{
    public class RoleViewModel
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSet { get; set; }
    }
}
