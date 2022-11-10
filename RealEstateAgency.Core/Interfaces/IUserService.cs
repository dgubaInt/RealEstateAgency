using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IUserService
    {
        Task<bool> Add(IdentityUser user);
        Task<bool> Lockout(IdentityUser user);
        Task<bool> UnLockout(IdentityUser user);
        Task<IEnumerable<IdentityUser>> GetAll();
        Task<IdentityUser> GetById(string id);
        Task<bool> Update(IdentityUser user);
        Task<List<IdentityRole>> GetRolesAsync();
        Task<bool> SetRolesAsync(IdentityUser user, Dictionary<string, bool> rolesToSet);
    }
}
