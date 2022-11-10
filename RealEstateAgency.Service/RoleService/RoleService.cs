using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Service.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public RoleService(IRoleRepository roleRepository, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            _roleRepository = roleRepository;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Add(IdentityRole role)
        {
            return await _roleRepository.AddAsync(role);
        }

        public async Task<List<IdentityRole>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<bool> SetRoleAsync(IdentityUser user, string role)
        {
            try
            {
                await _signInManager.UserManager.AddToRoleAsync(user, role);

                return true;
            }
            catch (Exception ex )
            {

                return false;
            }
        }

        public async Task<bool> SetRolesAsync(IdentityUser user, Dictionary<string, bool> rolesToSet)
        {
            try
            {
                foreach (var role in rolesToSet)
                {
                    if (role.Value is true)
                    {
                        await _signInManager.UserManager.AddToRoleAsync(user, role.Key);
                    }
                    else
                    {
                        await _signInManager.UserManager.RemoveFromRoleAsync(user, role.Key);
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
