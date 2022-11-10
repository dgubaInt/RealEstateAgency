using Microsoft.AspNetCore.Identity;

namespace RealEstateAgencyMVC.Areas.Admin.Models
{
    public class RoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSet { get; set; }
    }
}
