using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Models
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public bool IsLockedOut { get; set; } = false;
        public bool InRole { get; set; }
    }
}
