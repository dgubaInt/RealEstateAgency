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
        Task<bool> Add(IdentityRole<Guid> role);
        Task<bool> SetRolesAsync(AgentUser user, List<Tuple<Guid, string, bool>> updatedRoles, List<string> userRoles);
        Task<bool> SetRolesAsync(AgentUser user, Dictionary<Guid, bool> rolesToSet);
        Task<bool> AddRoleAsync(Guid userId, Guid roleId);
        Task<IEnumerable<IdentityRole<Guid>>> GetAll();
        Task<IEnumerable<IdentityUserRole<Guid>>> GetAllUserRole();
        Task<IdentityRole<Guid>> GetById(Guid id);
        Task<bool> Update(IdentityRole<Guid> role);
        Dictionary<Guid, bool> ManageUserRoles(IEnumerable<IdentityUserRole<Guid>> userRoles, Dictionary<Guid, bool> updatedUsers);
        Task<bool> RemoveRoleAsync(IdentityUserRole<Guid> userRole);
    }
}
