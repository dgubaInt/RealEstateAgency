using RealEstateAgency.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace RealEstateAgency.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserService(IUserRepository userRepository, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            _userRepository = userRepository;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Add(IdentityUser user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<bool> Lockout(IdentityUser user)
        {
            user.LockoutEnd = DateTime.Now.AddYears(1);
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<IdentityUser> GetById(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<bool> Update(IdentityUser user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> UnLockout(IdentityUser user)
        {
            user.LockoutEnd = null;
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<List<IdentityRole>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<bool> SetRolesAsync(IdentityUser user, Dictionary<string, bool> rolesToSet)
        {
            try
            {
                foreach (var role in rolesToSet)
                {
                    if (role.Value)
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
