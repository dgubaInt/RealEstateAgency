using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Models;

namespace RealEstateAgency.Service.Mappers.UserRoleMapper
{
    public interface IUserRoleMapper
    {
        List<UserViewModel> MapToUserVMAll(IEnumerable<AgentUser> users, IEnumerable<UserRole> userRoles);
        Task<EditUserViewModel> MapToEditUserVM(AgentUser user);
        AgentUser MapEditUserVMToIdentity(AgentUser user, EditUserViewModel editUserViewModel);
        AgentUser MapAddUserVMToIdentity(AddUserViewModel addUserViewModel);
        EditUserViewModel MapUserRolesToEditUserVM(EditUserViewModel editUserViewModel, IEnumerable<Role> identityRoles);
        AddUserViewModel MapUserRolesToAddUserVM(AddUserViewModel addUserViewModel, IEnumerable<Role> identityRoles);
        AddEditRoleViewModel MapUsersToAddRoleVM(AddEditRoleViewModel addRoleViewModel, IEnumerable<AgentUser> identityUser);
        List<RoleViewModel> MapToRoleVMAll(IEnumerable<Role> roles, IEnumerable<UserRole> userRoles);
        AddEditRoleViewModel MapUsersToEditRoleVM(IEnumerable<UserRole> userRoles, Role role, AddEditRoleViewModel editRoleViewModel, IEnumerable<AgentUser> identityUsers);
        Role MapAddEditRoleVMToIdentity(AddEditRoleViewModel editRoleViewModel, Role role);
    }
}
