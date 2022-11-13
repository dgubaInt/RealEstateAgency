﻿using Microsoft.AspNetCore.Identity;
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
        Task<bool> SetRolesAsync(IdentityUser user, List<Tuple<string, string, bool>> updatedRoles, List<string> userRoles);
        Task<bool> SetRolesAsync(IdentityUser user, Dictionary<string, bool> rolesToSet);
        Task<bool> SetRoleAsync(IdentityUser user, string role);
        Task<IEnumerable<IdentityRole>> GetAll();
        Task<IEnumerable<IdentityUserRole<string>>> GetAllUserRole();
    }
}
