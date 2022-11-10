using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IRoleService
    {
        Task<bool> Add(IdentityRole role);
        Task<List<IdentityRole>> GetRolesAsync();
        Task<bool> SetRolesAsync(IdentityUser user, Dictionary<string, bool> rolesToSet);
        Task<bool> SetRoleAsync(IdentityUser user, string role);
    }
}
