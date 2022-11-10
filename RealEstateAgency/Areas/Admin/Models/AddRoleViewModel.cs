using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RealEstateAgencyMVC.Areas.Admin.Models
{
    public class AddRoleViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }

        public List<UserToRoleViewModel> UsersToRole { get; set; } = new List<UserToRoleViewModel>();
    }
}
