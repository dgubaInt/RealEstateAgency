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

        public async Task<List<UserViewModel>> EVMMapAll(IEnumerable<IdentityUser> users)
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
    }
}
