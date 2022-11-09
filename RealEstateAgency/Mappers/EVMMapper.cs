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
                    UserRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty,
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
                    UserRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty,
                    Email = user.Email,
                    IsLockedOut = (user.LockoutEnd is null) ? false : true
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

            PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, editUserViewModel.Password);

            return user;
        }
    }
}
