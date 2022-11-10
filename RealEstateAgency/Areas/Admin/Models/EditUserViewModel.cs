using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace RealEstateAgencyMVC.Areas.Admin.Models
{
    public class EditUserViewModel
    {
        [Key]
        public string UserId { get; set; }

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
        public string? Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
