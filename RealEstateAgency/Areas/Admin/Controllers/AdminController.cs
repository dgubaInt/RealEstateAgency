using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgencyMVC.Areas.Admin.Models;
using RealEstateAgencyMVC.Mappers;

namespace RealEstateAgencyMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEVMMapper _eVMMapper;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminController(IUserService userService, IEVMMapper eVMMapper, SignInManager<IdentityUser> signInManager)
        {
            _userService = userService;
            _eVMMapper = eVMMapper;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userService.GetAll();

            var userViewModels = await _eVMMapper.MapToUserVMAll(users);
                
            return View(userViewModels);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userService.GetById(id);

            var editUserViewModel = await _eVMMapper.MapToEditUserVM(user);
            var roles = await _userService.GetRolesAsync();

            editUserViewModel = _eVMMapper.MapUserRoles(editUserViewModel, roles);

            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetById(editUserViewModel.UserId);

                if (user is not null)
                {
                    user = _eVMMapper.MapEditUserVMToIdentity(user, editUserViewModel);
                }
                await _userService.Update(user);

                foreach (var role in editUserViewModel.RoleViewModels)
                {
                    if (role.IsSet)
                    {
                        await _signInManager.UserManager.AddToRoleAsync(user, role.RoleName);
                    }
                    else
                    {
                        await _signInManager.UserManager.RemoveFromRoleAsync(user, role.RoleName);
                    }
                }
            }

            return RedirectToAction(nameof(ManageUsers));
        }
        
        [HttpPost]
        public async Task<IActionResult> UserLockout([FromForm] string id)
        {
            var user = await _userService.GetById(id);

            if (user is not null)
            {
                await _userService.Lockout(user);
            }
            return RedirectToAction(nameof(ManageUsers));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveLockout([FromForm] string id)
        {
            var user = await _userService.GetById(id);

            if (user is not null)
            {
                await _userService.UnLockout(user);
            }
            return RedirectToAction(nameof(ManageUsers));
        }
    }
}
