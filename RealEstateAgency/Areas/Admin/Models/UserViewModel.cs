using System.ComponentModel.DataAnnotations;

namespace RealEstateAgencyMVC.Areas.Admin.Models
{
    public class UserViewModel
    {
        [Key]
        public string UserId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        public string UserRole { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsLockedOut { get; set; } = false;
    }
}
