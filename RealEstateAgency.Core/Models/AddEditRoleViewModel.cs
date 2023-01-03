using RealEstateAgency.Core.Resources;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Models
{
    public class AddEditRoleViewModel
    {
        [Required]
        public Guid RoleId { get; set; }
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        [StringLength(20, ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.LengthError), MinimumLength = 3)]
        [Display(Name = "RoleName")]
        public string RoleName { get; set; }

        public List<UserToRoleViewModel> UsersToRole { get; set; } = new List<UserToRoleViewModel>();
    }
}
