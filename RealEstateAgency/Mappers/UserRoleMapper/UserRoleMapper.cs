using Microsoft.AspNetCore.Identity;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Models;
using System.Data;

namespace RealEstateAgencyMVC.Mappers.UserRoleMapper
{
    public class UserRoleMapper : IUserRoleMapper
    {
        private readonly UserManager<AgentUser> _userManager;

        public UserRoleMapper(UserManager<AgentUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<EditUserViewModel> MapToEditUserVM(AgentUser user)
        {
            var editUserViewModel = new EditUserViewModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                UserRoles = (List<string>)await _userManager.GetRolesAsync(user),
                Email = user.Email
            };

            return editUserViewModel;
        }

        public List<UserViewModel> MapToUserVMAll(IEnumerable<AgentUser> users, IEnumerable<UserRole> userRoles)
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

        public List<RoleViewModel> MapToRoleVMAll(IEnumerable<Role> roles, IEnumerable<UserRole> userRoles)
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

        public AgentUser MapEditUserVMToIdentity(AgentUser user, EditUserViewModel editUserViewModel)
        {
            user.FirstName = editUserViewModel.FirstName;
            user.LastName = editUserViewModel.LastName;
            user.UserName = editUserViewModel.UserName;
            user.NormalizedUserName = editUserViewModel.UserName.ToUpper();
            user.Email = editUserViewModel.Email;
            user.NormalizedEmail = editUserViewModel.Email.ToUpper();
            if (!string.IsNullOrWhiteSpace(editUserViewModel.Password))
            {
                PasswordHasher<AgentUser> passwordHasher = new PasswordHasher<AgentUser>();
                user.PasswordHash = passwordHasher.HashPassword(user, editUserViewModel.Password);
            }

            return user;
        }

        public AgentUser MapAddUserVMToIdentity(AddUserViewModel addUserViewModel)
        {
            var user = new AgentUser
            {
                FirstName = addUserViewModel.FirstName,
                LastName = addUserViewModel.LastName,
                UserName = addUserViewModel.UserName,
                NormalizedUserName = addUserViewModel.UserName.ToUpper(),
                Email = addUserViewModel.Email,
                NormalizedEmail = addUserViewModel.Email.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (!string.IsNullOrWhiteSpace(addUserViewModel.Password))
            {
                PasswordHasher<AgentUser> passwordHasher = new PasswordHasher<AgentUser>();
                user.PasswordHash = passwordHasher.HashPassword(user, addUserViewModel.Password);
            }

            return user;
        }

        public EditUserViewModel MapUserRolesToEditUserVM(EditUserViewModel editUserViewModel, IEnumerable<Role> identityRoles)
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

        public AddUserViewModel MapUserRolesToAddUserVM(AddUserViewModel addUserViewModel, IEnumerable<Role> identityRoles)
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

        public AddEditRoleViewModel MapUsersToAddRoleVM(AddEditRoleViewModel addRoleViewModel, IEnumerable<AgentUser> identityUsers)
        {
            addRoleViewModel.RoleId = Guid.NewGuid();
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

        public AddEditRoleViewModel MapUsersToEditRoleVM(IEnumerable<UserRole> userRoles, Role role, AddEditRoleViewModel editRoleViewModel, IEnumerable<AgentUser> identityUsers)
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

        public Role MapAddEditRoleVMToIdentity(AddEditRoleViewModel editRoleViewModel, Role role)
        {
            role.Name = editRoleViewModel.RoleName;
            role.NormalizedName = editRoleViewModel.RoleName.ToUpper();

            return role;
        }
    }
}
