using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgencyMVC.Mappers;

namespace RealEstateAgencyMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEVMMapper _eVMMapper;

        public AdminController(IUserService userService, IEVMMapper eVMMapper)
        {
            _userService = userService;
            _eVMMapper = eVMMapper;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userService.GetAll();

            var userViewModels = await _eVMMapper.EVMMapAll(users);
                
            return View(userViewModels);
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
