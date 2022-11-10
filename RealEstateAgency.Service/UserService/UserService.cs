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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Add(IdentityUser user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<bool> Lockout(IdentityUser user)
        {
            user.LockoutEnd = DateTime.Now.AddYears(20);
            //await _userManager.UpdateSecurityStampAsync(user);
            //user.SecurityStamp = Convert.ToString(Guid.NewGuid());
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
    }
}
