using Microsoft.AspNetCore.Identity;
using RealEstateAgency.Core.Entities;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IRoleService
    {
        Task<bool> AddAsync(Role role);
        Task<bool> SetRolesAsync(AgentUser user, Dictionary<Guid, bool> rolesToSet);
        Task<bool> AddRoleAsync(Guid userId, Guid roleId);
        Task<IEnumerable<Role>> GetAllAsync();
        Task<IEnumerable<UserRole>> GetAllUserRoleAsync();
        Task<Role> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Role role);
        Dictionary<Guid, bool> ManageUserRoles(IEnumerable<UserRole> userRoles, Dictionary<Guid, bool> updatedUsers);
        Task<bool> RemoveRoleAsync(UserRole userRole);
    }
}
