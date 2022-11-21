using Microsoft.AspNetCore.Identity;
using RealEstateAgency.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateAgency.Core.Models;

namespace RealEstateAgencyMVC.Mappers
{
    public interface IEVMMapper
    {
        List<UserViewModel> MapToUserVMAll(IEnumerable<AgentUser> users, IEnumerable<IdentityUserRole<Guid>> userRoles);
        Task<EditUserViewModel> MapToEditUserVM(AgentUser user);
        AgentUser MapEditUserVMToIdentity(AgentUser user, EditUserViewModel editUserViewModel);
        AgentUser MapAddUserVMToIdentity(AddUserViewModel addUserViewModel);
        EditUserViewModel MapUserRolesToEditUserVM(EditUserViewModel editUserViewModel, IEnumerable<IdentityRole<Guid>> identityRoles);
        AddUserViewModel MapUserRolesToAddUserVM(AddUserViewModel addUserViewModel, IEnumerable<IdentityRole<Guid>> identityRoles);
        AddEditRoleViewModel MapUsersToAddRoleVM(AddEditRoleViewModel addRoleViewModel, IEnumerable<AgentUser> identityUser);
        List<RoleViewModel> MapToRoleVMAll(IEnumerable<IdentityRole<Guid>> roles, IEnumerable<IdentityUserRole<Guid>> userRoles);
        AddEditRoleViewModel MapUsersToEditRoleVM(IEnumerable<IdentityUserRole<Guid>> userRoles, IdentityRole<Guid> role, AddEditRoleViewModel editRoleViewModel, IEnumerable<AgentUser> identityUsers);
        IdentityRole<Guid> MapAddEditRoleVMToIdentity(AddEditRoleViewModel editRoleViewModel, IdentityRole<Guid> role);
    }
}
