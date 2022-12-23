using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Core.Models;
using RealEstateAgency.Service.Mappers;
using System.Data;

namespace RealEstateAgencyMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EstatesController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBuildingPlanService _buildingPlanService;
        private readonly IBuildingTypeService _buildingTypeService;
        private readonly ICategoryService _categoryService;
        private readonly IEstateConditionService _estateConditionService;
        private readonly IEstateOptionService _estateOptionService;
        private readonly IZoneService _zoneService;
        private readonly IEstateService _estateService;
        private readonly IImageService _imageService;
        private readonly UserManager<AgentUser> _userManager;

        public EstatesController(IUserService userService, IEstateService estateService, IBuildingPlanService buildingPlanService, IBuildingTypeService buildingTypeService, ICategoryService categoryService, IEstateConditionService estateConditionService, IZoneService zoneService, IEstateOptionService estateOptionService, IImageService imageService, UserManager<AgentUser> userManager)
        {
            _userService = userService;
            _estateService = estateService;
            _buildingPlanService = buildingPlanService;
            _buildingTypeService = buildingTypeService;
            _categoryService = categoryService;
            _estateConditionService = estateConditionService;
            _zoneService = zoneService;
            _estateOptionService = estateOptionService;
            _imageService = imageService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<EstateViewModel> estates = new List<EstateViewModel>();

            if (User?.Identity?.Name != null)
            {
                if (User.Identity.Name != "admin")
                {
                    estates = (await _estateService.GetAllAsync(e => e.AgentUserId == Guid.Parse(User.Claims.First().Value))).Select(e => e.ToViewModel());
                }
                else
                {
                    estates = (await _estateService.GetAllAsync()).Select(e => e.ToViewModel());
                }
            }

            return View(estates);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var estate = await _estateService.GetByIdAsync(id);
            if (estate == null)
            {
                return NotFound();
            }

            var photoNames = (await _imageService.GetAllAsync()).Where(p => p.EstateId == id).Select(p => p.FileTitle);

            List<string> photos = new List<string>();

            foreach (var photo in photoNames)
            {
                photos.Add(_imageService.DownloadImage(photo));
            }

            return View(estate.ToDetailsViewModel(photos));
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create()
        {
            var addEstateViewModel = new AddEstateViewModel();
            var options = await _estateOptionService.GetAllAsync();
            addEstateViewModel.SetValues(options);

            ViewData["AgentUserId"] = new SelectList(await _userService.GetAllAsync(), "Id", "UserName");
            ViewData["BuildingPlanId"] = new SelectList(await _buildingPlanService.GetAllAsync(), "Id", "BuildingPlanName");
            ViewData["BuildingTypeId"] = new SelectList(await _buildingTypeService.GetAllAsync(), "Id", "BuildingTypeName");
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "CategoryName");
            ViewData["EstateConditionId"] = new SelectList(await _estateConditionService.GetAllAsync(), "Id", "EstateConditionName");
            ViewData["ZoneId"] = new SelectList(await _zoneService.GetAllAsync(), "Id", "ZoneName");
            return View(addEstateViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddEstateViewModel estate)
        {
            if (ModelState.IsValid)
            {
                var options = new List<EstateOption>();

                foreach (var option in estate.EstateOptionViewModels)
                {
                    if (option.IsSet)
                    {
                        options.Add(await _estateOptionService.GetByIdAsync(option.Id));
                    }
                }

                var estateToEntity = estate.ToEntity(options);
                await _estateService.AddAsync(estateToEntity);

                foreach (var image in estate.File)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName) + DateTime.Now.ToString("-MM-dd-yyyy-hh.mm.ss-tt") + Path.GetExtension(image.FileName);
                    if (_imageService.UploadImage(image, fileName))
                    {
                        await _imageService.AddAsync(image.ToEntity(estateToEntity, fileName));
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgentUserId"] = new SelectList(await _userService.GetAllAsync(), "Id", "UserName");
            ViewData["BuildingPlanId"] = new SelectList(await _buildingPlanService.GetAllAsync(), "Id", "BuildingPlanName");
            ViewData["BuildingTypeId"] = new SelectList(await _buildingTypeService.GetAllAsync(), "Id", "BuildingTypeName");
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "CategoryName");
            ViewData["EstateConditionId"] = new SelectList(await _estateConditionService.GetAllAsync(), "Id", "EstateConditionName");
            ViewData["ZoneId"] = new SelectList(await _zoneService.GetAllAsync(), "Id", "ZoneName");
            return View(estate);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var estate = await _estateService.GetByIdAsync(id);
            if (estate == null)
            {
                return NotFound();
            }
            var options = await _estateOptionService.GetAllAsync();
            var photoNames = (await _imageService.GetAllAsync()).Where(p => p.EstateId == id).Select(p => p.FileTitle);

            List<string> photos = new List<string>();

            foreach (var photo in photoNames)
            {
                photos.Add(_imageService.DownloadImage(photo));
            }

            ViewData["AgentUserId"] = new SelectList(await _userService.GetAllAsync(), "Id", "UserName");
            ViewData["BuildingPlanId"] = new SelectList(await _buildingPlanService.GetAllAsync(), "Id", "BuildingPlanName");
            ViewData["BuildingTypeId"] = new SelectList(await _buildingTypeService.GetAllAsync(), "Id", "BuildingTypeName");
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "CategoryName");
            ViewData["EstateConditionId"] = new SelectList(await _estateConditionService.GetAllAsync(), "Id", "EstateConditionName");
            ViewData["ZoneId"] = new SelectList(await _zoneService.GetAllAsync(), "Id", "ZoneName");
            return View(estate.ToEditViewModel(options, photos, photoNames));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditEstateViewModel estate)
        {
            if (id != estate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var photosToDelete = (await _imageService.GetAllAsync()).Where(i => (!estate.PhotoNames.Contains(i.FileTitle)) && i.EstateId == id).Select(i => i.FileTitle);
                    foreach (var image in photosToDelete)
                    {
                        _imageService.DeleteImage(image);
                    }
                    await _estateService.UpdateAsync(estate);

                    var estateEntity = await _estateService.GetByIdAsync(id);
                    foreach (var image in estate.File)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(image.FileName) + DateTime.Now.ToString("-MM-dd-yyyy-hh.mm.ss-tt.") + Path.GetExtension(image.FileName);
                        if (_imageService.UploadImage(image, fileName))
                        {
                            await _imageService.AddAsync(image.ToEntity(estateEntity, fileName));
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgentUserId"] = new SelectList(await _userService.GetAllAsync(), "Id", "UserName", estate.AgentUserId);
            ViewData["BuildingPlanId"] = new SelectList(await _buildingPlanService.GetAllAsync(), "Id", "BuildingPlanName", estate.BuildingPlanId);
            ViewData["BuildingTypeId"] = new SelectList(await _buildingTypeService.GetAllAsync(), "Id", "BuildingTypeName", estate.BuildingTypeId);
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "CategoryName", estate.CategoryId);
            ViewData["EstateConditionId"] = new SelectList(await _estateConditionService.GetAllAsync(), "EstateConditionId", "EstateConditionName", estate.EstateConditionId);
            ViewData["ZoneId"] = new SelectList(await _zoneService.GetAllAsync(), "Id", "ZoneName", estate.ZoneId);
            return View(estate);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var estate = await _estateService.GetByIdAsync(id);
            if (estate == null)
            {
                return NotFound();
            }

            var photoNames = (await _imageService.GetAllAsync()).Where(p => p.EstateId == id).Select(p => p.FileTitle);

            List<string> photos = new List<string>();

            foreach (var photo in photoNames)
            {
                photos.Add(_imageService.DownloadImage(photo));
            }

            return View(estate.ToDetailsViewModel(photos));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _estateService.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
