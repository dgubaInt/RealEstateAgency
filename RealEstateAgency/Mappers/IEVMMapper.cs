using Microsoft.AspNetCore.Identity;
using RealEstateAgencyMVC.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgencyMVC.Mappers
{
    public interface IEVMMapper
    {
        List<UserViewModel> MapToUserVMAll(IEnumerable<IdentityUser> users, IEnumerable<IdentityUserRole<string>> userRoles);
        Task<EditUserViewModel> MapToEditUserVM(IdentityUser users);
        IdentityUser MapEditUserVMToIdentity(IdentityUser user, EditUserViewModel editUserViewModel);
        IdentityUser MapAddUserVMToIdentity(AddUserViewModel addUserViewModel);
        EditUserViewModel MapUserRolesToEditUserVM(EditUserViewModel editUserViewModel, IEnumerable<IdentityRole> identityRoles);
        AddUserViewModel MapUserRolesToAddUserVM(AddUserViewModel addUserViewModel, IEnumerable<IdentityRole> identityRoles);
        AddRoleViewModel MapUsersToAddRoleVM(AddRoleViewModel addRoleViewModel, IEnumerable<IdentityUser> identityUser);
        List<RoleViewModel> MapToRoleVMAll(IEnumerable<IdentityRole> roles, IEnumerable<IdentityUserRole<string>> userRoles);
    }
}
