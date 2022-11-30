using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Core.Models;
using RealEstateAgencyMVC.Mappers;
using System.Data;

namespace RealEstateAgencyMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IUserRoleMapper _eVMMapper;

        public UserController(IUserService userService, IUserRoleMapper eVMMapper, IRoleService roleService)
        {
            _userService = userService;
            _eVMMapper = eVMMapper;
            _roleService = roleService;
        }
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userService.GetAllAsync();
            var userRoles = await _roleService.GetAllUserRoleAsync();

            var userViewModels = _eVMMapper.MapToUserVMAll(users, userRoles);
                
            return View(userViewModels);
        }

        public async Task<IActionResult> EditUser(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            var editUserViewModel = await _eVMMapper.MapToEditUserVM(user);
            var roles = await _roleService.GetAllAsync();

            editUserViewModel = _eVMMapper.MapUserRolesToEditUserVM(editUserViewModel, roles);

            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel)
        {

            if (ModelState.IsValid)
            {
                var user = await _userService.GetByIdAsync(editUserViewModel.UserId);

                if (user is not null)
                {
                    user = _eVMMapper.MapEditUserVMToIdentity(user, editUserViewModel);
                }
                await _userService.UpdateAsync(user);

                var userRoles = await _roleService.GetAllUserRoleAsync();

                Dictionary<Guid, bool> userRoleDetails = new Dictionary<Guid, bool>();

                foreach (var userRole in editUserViewModel.RoleViewModels)
                {
                    userRoleDetails.Add(userRole.RoleId, userRole.IsSet);
                }

                userRoleDetails = _userService.ManageUserRoles(userRoles.Where(ur => ur.UserId == editUserViewModel.UserId), userRoleDetails);

                foreach (var userRole in userRoleDetails)
                {
                    if (userRole.Value)
                    {
                        await _roleService.AddRoleAsync(editUserViewModel.UserId, userRole.Key);
                    }
                    else
                    {
                        var userRoleIdentity = userRoles.Where(ur => ur.UserId == editUserViewModel.UserId && ur.RoleId == userRole.Key).FirstOrDefault();
                        await _roleService.RemoveRoleAsync(userRoleIdentity);
                    }
                }
            }

            return RedirectToAction(nameof(ManageUsers));
        }

        public async Task<IActionResult> AddUser()
        {
            var addUserViewModel = new AddUserViewModel();
            var roles = await _roleService.GetAllAsync();

            addUserViewModel = _eVMMapper.MapUserRolesToAddUserVM(addUserViewModel, roles);

            return View(addUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _eVMMapper.MapAddUserVMToIdentity(addUserViewModel);

                await _userService.AddAsync(user);

                var rolesToSet = new Dictionary<Guid, bool>();
                foreach (var role in addUserViewModel.RoleViewModels)
                {
                    rolesToSet.Add(role.RoleId, role.IsSet);
                }

                await _roleService.SetRolesAsync(user, rolesToSet);
            }

            return RedirectToAction(nameof(ManageUsers));
        }

        [HttpPost]
        public async Task<IActionResult> UserLockout([FromForm] Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user is not null)
            {
                await _userService.LockoutAsync(user);
            }
            return RedirectToAction(nameof(ManageUsers));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveLockout([FromForm] Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user is not null)
            {
                await _userService.RemoveLockoutAsync(user);
            }
            return RedirectToAction(nameof(ManageUsers));
        }
    }
}
