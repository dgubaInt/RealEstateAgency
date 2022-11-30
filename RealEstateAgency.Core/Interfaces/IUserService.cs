using Microsoft.AspNetCore.Identity;
using RealEstateAgency.Core.Entities;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddAsync(AgentUser user);
        Task<bool> LockoutAsync(AgentUser user);
        Task<bool> RemoveLockoutAsync(AgentUser user);
        Task<IEnumerable<AgentUser>> GetAllAsync();
        Task<AgentUser> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(AgentUser user);
        Dictionary<Guid, bool> ManageUserRoles(IEnumerable<IdentityUserRole<Guid>> userRoles, Dictionary<Guid, bool> updatedRoles);
    }
}
