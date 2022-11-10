using Microsoft.AspNetCore.Identity;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgencyMVC.Areas.Admin.Models;

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

        public async Task<List<UserViewModel>> MapToUserVMAll(IEnumerable<IdentityUser> users)
        {
            var viewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var userViewModel = new UserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsLockedOut = user.LockoutEnd is not null
                };

                viewModels.Add(userViewModel);
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

        public EditUserViewModel MapUserRolesToEditUserVM(EditUserViewModel editUserViewModel, List<IdentityRole> identityRoles)
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

        public AddUserViewModel MapUserRolesToAddUserVM(AddUserViewModel addUserViewModel, List<IdentityRole> identityRoles)
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

        public AddRoleViewModel MapUsersToAddRoleVM(AddRoleViewModel addRoleViewModel, IEnumerable<IdentityUser> identityUser)
        {
            if (identityUser is not null)
            {
                foreach (var user in identityUser)
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
    }
}
