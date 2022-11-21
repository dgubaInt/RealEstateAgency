using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace RealEstateAgency.Core.Models
{
    public class EditUserViewModel
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name ="User name")]
        public string UserName { get; set; }

        public List<string> UserRoles { get; set; } = new List<string>();

        public List<RoleViewModel> RoleViewModels { get; set; } = new List<RoleViewModel>();

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
