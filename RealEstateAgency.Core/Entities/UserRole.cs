using Microsoft.AspNetCore.Identity;
using RealEstateAgency.Core.Interfaces;

namespace RealEstateAgency.Core.Entities
{
    public class UserRole : IdentityUserRole<Guid>, IBaseEntity
    {
        public Guid Id { get; set; }
    }
}
