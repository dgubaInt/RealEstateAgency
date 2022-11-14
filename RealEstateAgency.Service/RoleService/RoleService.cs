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
        private readonly IUserRoleRepository _userRoleRepository;

        public RoleService(IRoleRepository roleRepository, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, IUserRoleRepository userRoleRepository)
        {
            _roleRepository = roleRepository;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<bool> Add(IdentityRole role)
        {
            return await _roleRepository.AddAsync(role);
        }

        public async Task<IEnumerable<IdentityRole>> GetAll()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<IdentityRole> GetById(string id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task<bool> Update(IdentityRole role)
        {
            return await _roleRepository.UpdateAsync(role);
        }

        public async Task<IEnumerable<IdentityUserRole<string>>> GetAllUserRole()
        {
            return await _userRoleRepository.GetAllAsync();
        }

        public Dictionary<string, bool> ManageUserRoles(IEnumerable<IdentityUserRole<string>> userRoles, Dictionary<string, bool> updatedUsers)
        {
            var userRoleDetails = new Dictionary<string, bool>();

            foreach (var user in updatedUsers)
            {
                if ((user.Value == true && !(userRoles.Where(ur => ur.UserId == user.Key).Count() > 0)) ||
                    (user.Value == false && userRoles.Where(ur => ur.UserId == user.Key).Count() > 0))
                {
                    userRoleDetails.Add(user.Key, user.Value);
                }
            }

            return userRoleDetails;
        }

        public async Task<bool> AddRoleAsync(string userId, string roleId)
        {
            try
            {
                var userRole = new IdentityUserRole<string>
                {
                    RoleId = roleId,
                    UserId = userId
                };

                await _userRoleRepository.AddAsync(userRole);

                return true;
            }
            catch (Exception ex )
            {

                return false;
            }
        }

        public async Task<bool> RemoveRoleAsync(IdentityUserRole<string> userRole)
        {
            try
            {
                await _userRoleRepository.DeleteAsync(userRole);

                return true;
            }
            catch (Exception ex)
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
                    if (role.Value)
                    {
                        var userRole = new IdentityUserRole<string>
                        {
                            RoleId = role.Key,
                            UserId = user.Id
                        };

                        await _userRoleRepository.AddAsync(userRole);
                        //await _signInManager.UserManager.AddToRoleAsync(user, role.Key);
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SetRolesAsync(IdentityUser user, List<Tuple<string, string, bool>> updatedRoles, List<string> userRoles)
        {
            try
            {
                var roles = await GetAll();
                var userRolesDictionary = new Dictionary<string, bool>();
                foreach (var role in roles)
                {
                    if (userRoles.Contains(role.Name))
                    {
                        userRolesDictionary.Add(role.Name, true);
                    }
                    else
                    {
                        userRolesDictionary.Add(role.Name, false);
                    }
                }

                var rolesToSet = new Dictionary<string, bool>();
                foreach (var role in updatedRoles)
                {
                    foreach (var userRoleDictionaryItem in userRolesDictionary)
                    {
                        if (role.Item2 == userRoleDictionaryItem.Key)
                        {
                            if (role.Item3 != userRoleDictionaryItem.Value)
                            {
                                rolesToSet.Add(role.Item1, role.Item3);
                            }
                        }
                    }
                }
                foreach (var role in rolesToSet)
                {
                    var userRole = new IdentityUserRole<string>
                    {
                        RoleId = role.Key,
                        UserId = user.Id
                    };

                    if (role.Value is true)
                    {
                        await _userRoleRepository.AddAsync(userRole);
                        //await _signInManager.UserManager.AddToRoleAsync(user, role.Key);
                    }
                    else
                    {
                        await _userRoleRepository.DeleteAsync(userRole);
                        //await _signInManager.UserManager.RemoveFromRoleAsync(user, role.Key);
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
