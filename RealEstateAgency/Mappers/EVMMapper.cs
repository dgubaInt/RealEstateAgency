using Microsoft.AspNetCore.Identity;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgencyMVC.Areas.Admin.Models;
using System.Data;

namespace RealEstateAgencyMVC.Mappers
{
    public class EVMMapper : IEVMMapper
    {
        private readonly UserManager<IdentityUser> _userManager;

        public EVMMapper(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<EditUserViewModel> MapToEditUserVM(IdentityUser user)
        {
                var editUserViewModel = new EditUserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserRoles = (List<string>) await _userManager.GetRolesAsync(user),
                    Email = user.Email
                };

            return editUserViewModel;
        }

        public List<UserViewModel> MapToUserVMAll(IEnumerable<IdentityUser> users, IEnumerable<IdentityUserRole<string>> userRoles)
        {
            var viewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var userViewModel = new UserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsLockedOut = user.LockoutEnd is not null,
                    InRole = userRoles.Where(ur => ur.UserId == user.Id).Count() > 0
                };

                viewModels.Add(userViewModel);
            }

            return viewModels;
        }

        public List<RoleViewModel> MapToRoleVMAll(IEnumerable<IdentityRole> roles, IEnumerable<IdentityUserRole<string>> userRoles)
        {
            var viewModels = new List<RoleViewModel>();

            foreach (var role in roles)
            {
                var roleViewModel = new RoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSet = userRoles.Where(ur => ur.RoleId == role.Id).Count() > 0
                };

                viewModels.Add(roleViewModel);
            }

            return viewModels;
        }

        public IdentityUser MapEditUserVMToIdentity(IdentityUser user, EditUserViewModel editUserViewModel)
        {
            user.UserName = editUserViewModel.UserName;
            user.NormalizedUserName = editUserViewModel.UserName.ToUpper();
            user.Email = editUserViewModel.Email;
            user.NormalizedEmail = editUserViewModel.Email.ToUpper();
            if (!string.IsNullOrWhiteSpace(editUserViewModel.Password))
            {
                PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
                user.PasswordHash = passwordHasher.HashPassword(user, editUserViewModel.Password);
            }

            return user;
        }

        public IdentityUser MapAddUserVMToIdentity(AddUserViewModel addUserViewModel)
        {
            var user = new IdentityUser
            {

                UserName = addUserViewModel.UserName,
                NormalizedUserName = addUserViewModel.UserName.ToUpper(),
                Email = addUserViewModel.Email,
                NormalizedEmail = addUserViewModel.Email.ToUpper()
            };

            if (!string.IsNullOrWhiteSpace(addUserViewModel.Password))
            {
                PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
                user.PasswordHash = passwordHasher.HashPassword(user, addUserViewModel.Password);
            }

            return user;
        }

        public EditUserViewModel MapUserRolesToEditUserVM(EditUserViewModel editUserViewModel, IEnumerable<IdentityRole> identityRoles)
        {
            if (identityRoles is not null)
            {
                foreach (var role in identityRoles)
                {
                    var roleViewModel = new RoleViewModel
                    {
                        RoleId = role.Id,
                        RoleName = role.Name,
                        IsSet = editUserViewModel.UserRoles.Contains(role.Name)
                    };

                    editUserViewModel.RoleViewModels.Add(roleViewModel);
                }
            }

            return editUserViewModel;
        }

        public AddUserViewModel MapUserRolesToAddUserVM(AddUserViewModel addUserViewModel, IEnumerable<IdentityRole> identityRoles)
        {
            if (identityRoles is not null)
            {
                foreach (var role in identityRoles)
                {
                    var roleViewModel = new RoleViewModel
                    {
                        RoleId = role.Id,
                        RoleName = role.Name,
                        IsSet = false
                    };

                    addUserViewModel.RoleViewModels.Add(roleViewModel);
                }
            }

            return addUserViewModel;
        }

        public AddEditRoleViewModel MapUsersToAddRoleVM(AddEditRoleViewModel addRoleViewModel, IEnumerable<IdentityUser> identityUsers)
        {
            addRoleViewModel.RoleId = Guid.NewGuid().ToString();
            if (identityUsers is not null)
            {
                foreach (var user in identityUsers)
                {
                    var userToRoleViewModel = new UserToRoleViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        IsSelected = false
                    };

                    addRoleViewModel.UsersToRole.Add(userToRoleViewModel);
                }
            }

            return addRoleViewModel;
        }

        public AddEditRoleViewModel MapUsersToEditRoleVM(IEnumerable<IdentityUserRole<string>> userRoles, IdentityRole role, AddEditRoleViewModel editRoleViewModel, IEnumerable<IdentityUser> identityUsers)
        {
            editRoleViewModel.RoleId = role.Id;
            editRoleViewModel.RoleName = role.Name;

            if (identityUsers is not null)
            {
                foreach (var user in identityUsers)
                {
                    var userToRoleViewModel = new UserToRoleViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        IsSelected = userRoles.Where(ur => ur.UserId == user.Id && ur.RoleId == role.Id).Count() > 0
                    };

                    editRoleViewModel.UsersToRole.Add(userToRoleViewModel);
                }
            }

            return editRoleViewModel;
        }

        public IdentityRole MapAddEditRoleVMToIdentity(AddEditRoleViewModel editRoleViewModel, IdentityRole role)
        {
            role.Name = editRoleViewModel.RoleName;
            role.NormalizedName = editRoleViewModel.RoleName.ToUpper();

            return role;
        }
    }
}
