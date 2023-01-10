using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using System.Data;

namespace RealEstateAgency.Service.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<UserRole> _userRoleRepository;

        public RoleService(IGenericRepository<Role> roleRepository, IGenericRepository<UserRole> userRoleRepository)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<bool> AddAsync(Role role)
        {
            return await _roleRepository.AddAsync(role);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Role role)
        {
            return await _roleRepository.UpdateAsync(role);
        }

        public async Task<IEnumerable<UserRole>> GetAllUserRoleAsync()
        {
            return await _userRoleRepository.GetAllAsync();
        }

        public Dictionary<Guid, bool> ManageUserRoles(IEnumerable<UserRole> userRoles, Dictionary<Guid, bool> updatedUsers)
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
                var userRole = new UserRole
                {
                    RoleId = roleId,
                    UserId = userId
                };

                return await _userRoleRepository.AddAsync(userRole);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> RemoveRoleAsync(UserRole userRole)
        {
            try
            {
                return await _userRoleRepository.DeleteAsync(userRole);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task SetRolesAsync(AgentUser user, Dictionary<Guid, bool> rolesToSet)
        {
            foreach (var role in rolesToSet)
            {
                if (role.Value)
                {
                    var userRole = new UserRole
                    {
                        RoleId = role.Key,
                        UserId = user.Id
                    };

                    await _userRoleRepository.AddAsync(userRole);
                }
            }
        }
    }
}
