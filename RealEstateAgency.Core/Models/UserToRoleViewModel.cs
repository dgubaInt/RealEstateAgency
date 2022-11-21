namespace RealEstateAgency.Core.Models
{
    public class UserToRoleViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
