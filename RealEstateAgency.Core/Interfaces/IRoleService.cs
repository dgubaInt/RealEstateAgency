using Microsoft.AspNetCore.Identity;
using RealEstateAgency.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IRoleService
    {
        Task<bool> AddAsync(IdentityRole<Guid> role);
        Task<bool> SetRolesAsync(AgentUser user, Dictionary<Guid, bool> rolesToSet);
        Task<bool> AddRoleAsync(Guid userId, Guid roleId);
        Task<IEnumerable<IdentityRole<Guid>>> GetAllAsync();
        Task<IEnumerable<IdentityUserRole<Guid>>> GetAllUserRoleAsync();
        Task<IdentityRole<Guid>> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(IdentityRole<Guid> role);
        Dictionary<Guid, bool> ManageUserRoles(IEnumerable<IdentityUserRole<Guid>> userRoles, Dictionary<Guid, bool> updatedUsers);
        Task<bool> RemoveRoleAsync(IdentityUserRole<Guid> userRole);
    }
}
