using RealEstateAgency.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using RealEstateAgency.Core.Entities;

namespace RealEstateAgency.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AddAsync(AgentUser user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<bool> LockoutAsync(AgentUser user)
        {
            user.LockoutEnd = DateTime.Now.AddYears(20);
            user.LockoutEnabled = true;
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<IEnumerable<AgentUser>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<AgentUser> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public Dictionary<Guid, bool> ManageUserRoles(IEnumerable<IdentityUserRole<Guid>> userRoles, Dictionary<Guid, bool> updatedRoles)
        {
            var userRoleDetails = new Dictionary<Guid, bool>();

            foreach (var role in updatedRoles)
            {
                if ((role.Value == true && !(userRoles.Where(ur => ur.RoleId == role.Key).Count() > 0)) ||
                    (role.Value == false && userRoles.Where(ur => ur.RoleId == role.Key).Count() > 0))
                {
                    userRoleDetails.Add(role.Key, role.Value);
                }
            }

            return userRoleDetails;
        }

        public async Task<bool> UpdateAsync(AgentUser user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> RemoveLockoutAsync(AgentUser user)
        {
            user.LockoutEnd = null;
            user.LockoutEnabled = false;
            return await _userRepository.UpdateAsync(user);
        }
    }
}
