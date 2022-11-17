using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Core.Entities;
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
        private readonly IUserRoleRepository _userRoleRepository;

        public RoleService(IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<bool> Add(IdentityRole<Guid> role)
        {
            return await _roleRepository.AddAsync(role);
        }

        public async Task<IEnumerable<IdentityRole<Guid>>> GetAll()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<IdentityRole<Guid>> GetById(Guid id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task<bool> Update(IdentityRole<Guid> role)
        {
            return await _roleRepository.UpdateAsync(role);
        }

        public async Task<IEnumerable<IdentityUserRole<Guid>>> GetAllUserRole()
        {
            return await _userRoleRepository.GetAllAsync();
        }

        public Dictionary<Guid, bool> ManageUserRoles(IEnumerable<IdentityUserRole<Guid>> userRoles, Dictionary<Guid, bool> updatedUsers)
        {
            var userRoleDetails = new Dictionary<Guid, bool>();

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

        public async Task<bool> AddRoleAsync(Guid userId, Guid roleId)
        {
            try
            {
                var userRole = new IdentityUserRole<Guid>
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

        public async Task<bool> RemoveRoleAsync(IdentityUserRole<Guid> userRole)
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

        public async Task<bool> SetRolesAsync(AgentUser user, Dictionary<Guid, bool> rolesToSet)
        {
            try
            {
                foreach (var role in rolesToSet)
                {
                    if (role.Value)
                    {
                        var userRole = new IdentityUserRole<Guid>
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

        public async Task<bool> SetRolesAsync(AgentUser user, List<Tuple<Guid, string, bool>> updatedRoles, List<string> userRoles)
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

                var rolesToSet = new Dictionary<Guid, bool>();
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
                    var userRole = new IdentityUserRole<Guid>
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
