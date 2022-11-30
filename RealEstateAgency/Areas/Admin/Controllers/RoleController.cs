using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Core.Models;
using RealEstateAgencyMVC.Mappers;
using System.Data;

namespace RealEstateAgencyMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IUserRoleMapper _eVMMapper;

        public RoleController(IUserService userService, IUserRoleMapper eVMMapper, IRoleService roleService)
        {
            _userService = userService;
            _eVMMapper = eVMMapper;
            _roleService = roleService;
        }

        public async Task<IActionResult> ManageRoles()
        {
            var roles = await _roleService.GetAllAsync();
            var userRoles = await _roleService.GetAllUserRoleAsync();

            var roleViewModels = _eVMMapper.MapToRoleVMAll(roles, userRoles);

            return View(roleViewModels);
        }

        
        public async Task<IActionResult> AddRole()
        {
            var addRoleViewModel = new AddEditRoleViewModel();

            var users = await _userService.GetAllAsync();

            addRoleViewModel = _eVMMapper.MapUsersToAddRoleVM(addRoleViewModel, users);

            return View(addRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddEditRoleViewModel addRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                await _roleService.AddAsync(new IdentityRole<Guid>
                {
                    Id = addRoleViewModel.RoleId,
                    Name = addRoleViewModel.RoleName,
                    NormalizedName = addRoleViewModel.RoleName.ToUpper()
                });

                foreach (var userToRole in addRoleViewModel.UsersToRole)
                {
                    if (userToRole.IsSelected)
                    {
                        var user = await _userService.GetByIdAsync(userToRole.UserId);

                        await _roleService.AddRoleAsync(user.Id, addRoleViewModel.RoleId);
                    }
                }
            }

            return RedirectToAction(nameof(ManageRoles));
        }

        public async Task<IActionResult> EditRole(Guid id)
        {
            var role = await _roleService.GetByIdAsync(id);
            var users = await _userService.GetAllAsync();
            var userRoles = await _roleService.GetAllUserRoleAsync();

            var editRoleViewModel = new AddEditRoleViewModel();

            editRoleViewModel = _eVMMapper.MapUsersToEditRoleVM(userRoles, role, editRoleViewModel, users);

            return View(editRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(AddEditRoleViewModel editRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleService.GetByIdAsync(editRoleViewModel.RoleId);

                if (role is not null)
                {
                    role = _eVMMapper.MapAddEditRoleVMToIdentity(editRoleViewModel, role);
                }
                await _roleService.UpdateAsync(role);

                var userRoles = await _roleService.GetAllUserRoleAsync();

                Dictionary<Guid, bool> userRoleDetails = new Dictionary<Guid, bool>();

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
    }
}
