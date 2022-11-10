using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgencyMVC.Areas.Admin.Models;
using RealEstateAgencyMVC.Mappers;
using System.Data;

namespace RealEstateAgencyMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IEVMMapper _eVMMapper;

        public AdminController(IUserService userService, IEVMMapper eVMMapper, IRoleService roleService)
        {
            _userService = userService;
            _eVMMapper = eVMMapper;
            _roleService = roleService;
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
            var roles = await _roleService.GetRolesAsync();

            editUserViewModel = _eVMMapper.MapUserRolesToEditUserVM(editUserViewModel, roles);

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

                var rolesToSet = new Dictionary<string, bool>();
                foreach (var role in editUserViewModel.RoleViewModels)
                {
                    rolesToSet.Add(role.RoleName, role.IsSet);
                }

                await _roleService.SetRolesAsync(user, rolesToSet);
            }

            return RedirectToAction(nameof(ManageUsers));
        }

        public async Task<IActionResult> AddUser()
        {
            var addUserViewModel = new AddUserViewModel();
            var roles = await _roleService.GetRolesAsync();

            addUserViewModel = _eVMMapper.MapUserRolesToAddUserVM(addUserViewModel, roles);

            return View(addUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _eVMMapper.MapAddUserVMToIdentity(addUserViewModel);

                await _userService.Add(user);

                var rolesToSet = new Dictionary<string, bool>();
                foreach (var role in addUserViewModel.RoleViewModels)
                {
                    rolesToSet.Add(role.RoleName, role.IsSet);
                }

                await _roleService.SetRolesAsync(user, rolesToSet);
            }

            return RedirectToAction(nameof(ManageUsers));
        }

        public async Task<IActionResult> AddRole()
        {
            var addRoleViewModel = new AddRoleViewModel();

            var users = await _userService.GetAll();

            addRoleViewModel = _eVMMapper.MapUsersToAddRoleVM(addRoleViewModel, users);

            return View(addRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel addRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                await _roleService.Add(new IdentityRole{ 
                    Name = addRoleViewModel.RoleName, 
                    NormalizedName = addRoleViewModel.RoleName.ToUpper()
                });

                foreach (var userToRole in addRoleViewModel.UsersToRole)
                {
                    if (userToRole.IsSelected)
                    {
                        var user = await _userService.GetById(userToRole.UserId);

                        await _roleService.SetRoleAsync(user, addRoleViewModel.RoleName);
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
