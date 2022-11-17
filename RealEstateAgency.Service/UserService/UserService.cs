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

        public async Task<bool> Add(AgentUser user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<bool> Lockout(AgentUser user)
        {
            user.LockoutEnd = DateTime.Now.AddYears(20);
            user.LockoutEnabled = true;
            //await _userManager.UpdateSecurityStampAsync(user);
            //user.SecurityStamp = Convert.ToString(Guid.NewGuid());
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<IEnumerable<AgentUser>> GetAll()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<AgentUser> GetById(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<bool> Update(AgentUser user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> UnLockout(AgentUser user)
        {
            user.LockoutEnd = null;
            user.LockoutEnabled = false;
            return await _userRepository.UpdateAsync(user);
        }
    }
}
