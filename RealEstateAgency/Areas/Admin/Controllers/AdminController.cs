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
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(IUserService userService, IEVMMapper eVMMapper, IRoleService roleService, UserManager<IdentityUser> userManager)
        {
            _userService = userService;
            _eVMMapper = eVMMapper;
            _roleService = roleService;
            _userManager = userManager;
        }
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userService.GetAll();
            var userRoles = await _roleService.GetAllUserRole();

            var userViewModels = _eVMMapper.MapToUserVMAll(users, userRoles);
                
            return View(userViewModels);
        }

        public async Task<IActionResult> ManageRoles()
        {
            var roles = await _roleService.GetAll();
            var userRoles = await _roleService.GetAllUserRole();

            var roleViewModels = _eVMMapper.MapToRoleVMAll(roles, userRoles);

            return View(roleViewModels);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userService.GetById(id);

            var editUserViewModel = await _eVMMapper.MapToEditUserVM(user);
            var roles = await _roleService.GetAll();

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

                var userRoles = (List<string>)await _userManager.GetRolesAsync(user);

                var updatedRoles = new List<Tuple<string, string, bool>>();

                foreach (var role in editUserViewModel.RoleViewModels)
                {
                    updatedRoles.Add(new Tuple<string, string, bool>(role.RoleId, role.RoleName, role.IsSet));
                }

                await _roleService.SetRolesAsync(user, updatedRoles, userRoles);
            }

            return RedirectToAction(nameof(ManageUsers));
        }

        public async Task<IActionResult> AddUser()
        {
            var addUserViewModel = new AddUserViewModel();
            var roles = await _roleService.GetAll();

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
                    rolesToSet.Add(role.RoleId, role.IsSet);
                }

                await _roleService.SetRolesAsync(user, rolesToSet);
            }

            return RedirectToAction(nameof(ManageUsers));
        }

        public async Task<IActionResult> AddRole()
        {
            var addRoleViewModel = new AddEditRoleViewModel();

            var users = await _userService.GetAll();

            addRoleViewModel = _eVMMapper.MapUsersToAddRoleVM(addRoleViewModel, users);

            return View(addRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddEditRoleViewModel addRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                await _roleService.Add(new IdentityRole{
                    Id = addRoleViewModel.RoleId,
                    Name = addRoleViewModel.RoleName, 
                    NormalizedName = addRoleViewModel.RoleName.ToUpper()
                });

                foreach (var userToRole in addRoleViewModel.UsersToRole)
                {
                    if (userToRole.IsSelected)
                    {
                        var user = await _userService.GetById(userToRole.UserId);

                        await _roleService.AddRoleAsync(user.Id, addRoleViewModel.RoleId);
                    }
                }
            }

            return RedirectToAction(nameof(ManageRoles));
        }

        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleService.GetById(id);
            var users = await _userService.GetAll();
            var userRoles = await _roleService.GetAllUserRole();

            var editRoleViewModel = new AddEditRoleViewModel();

            editRoleViewModel = _eVMMapper.MapUsersToEditRoleVM(userRoles, role, editRoleViewModel, users);

            return View(editRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(AddEditRoleViewModel editRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleService.GetById(editRoleViewModel.RoleId);

                if (role is not null)
                {
                    role = _eVMMapper.MapAddEditRoleVMToIdentity(editRoleViewModel, role);
                }
                await _roleService.Update(role);

                var userRoles = await _roleService.GetAllUserRole();

                Dictionary<string, bool> userRoleDetails = new Dictionary<string, bool>();

                foreach (var userRole in editRoleViewModel.UsersToRole)
                {
                    userRoleDetails.Add(userRole.UserId, userRole.IsSelected);
                }

                userRoleDetails = _roleService.ManageUserRoles(userRoles.Where(ur => ur.RoleId == editRoleViewModel.RoleId), userRoleDetails);
                
                foreach (var userRole in userRoleDetails)
                {
                    if (userRole.Value)
                    {
                        await _roleService.AddRoleAsync(userRole.Key, editRoleViewModel.RoleId);
                    }
                    else
                    {
                        var userRoleIdentity = userRoles.Where(ur => ur.RoleId == editRoleViewModel.RoleId && ur.UserId == userRole.Key).FirstOrDefault();
                        await _roleService.RemoveRoleAsync(userRoleIdentity);
                    }
                }
            }

            return RedirectToAction(nameof(ManageRoles));
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
