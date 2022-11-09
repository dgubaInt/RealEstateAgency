using RealEstateAgency.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RealEstateAgency.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> Add(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Lockout(IdentityUser user)
        {
            user.LockoutEnd = DateTime.Now.AddYears(1);
            return await _userRepository.Update(user);
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<IdentityUser> GetById(string id)
        {
            return await _userRepository.GetById(id);
        }

        public Task<bool> Update(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UnLockout(IdentityUser user)
        {
            user.LockoutEnd = null;
            return await _userRepository.Update(user);
        }
    }
}
