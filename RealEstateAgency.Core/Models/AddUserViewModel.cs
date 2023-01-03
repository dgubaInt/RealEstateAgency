using RealEstateAgency.Core.Resources;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Models
{
    public class AddUserViewModel
    {
        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        [StringLength(30, ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.LengthError), MinimumLength = 3)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        [StringLength(30, ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.LengthError), MinimumLength = 3)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        [StringLength(20, ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.LengthError), MinimumLength = 3)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.LengthError), MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = nameof(UILabel.FieldRequired))]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceType = typeof(UILabel), ErrorMessageResourceName = "PasswordConfirmationError")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        public List<RoleViewModel> RoleViewModels { get; set; } = new List<RoleViewModel>();
    }
}
