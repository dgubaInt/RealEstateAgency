using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Core.Models;
using RealEstateAgency.Infrastructure.Data;
using RealEstateAgency.Service.Mappers;

namespace RealEstateAgencyMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EstatesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IBuildingPlanService _buildingPlanService;
        private readonly IBuildingTypeService _buildingTypeService;
        private readonly ICategoryService _categoryService;
        private readonly IEstateConditionService _estateConditionService;
        private readonly IEstateOptionService _estateOptionService;
        private readonly IZoneService _zoneService;
        private readonly IEstateService _estateService;

        public EstatesController(ApplicationDbContext context, IUserService userService, IEstateService estateService, IBuildingPlanService buildingPlanService, IBuildingTypeService buildingTypeService, ICategoryService categoryService, IEstateConditionService estateConditionService, IZoneService zoneService, IEstateOptionService estateOptionService)
        {
            _context = context;
            _userService = userService;
            _estateService = estateService;
            _buildingPlanService = buildingPlanService;
            _buildingTypeService = buildingTypeService;
            _categoryService = categoryService;
            _estateConditionService = estateConditionService;
            _zoneService = zoneService;
            _estateOptionService = estateOptionService;
        }

        // GET: Admin/Estates
        public async Task<IActionResult> Index()
        {
            var estates = (await _estateService.GetAllAsync()).Select(e => e.ToViewModel());
            return View(estates);
        }

        // GET: Admin/Estates/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var estate = await _estateService.GetByIdAsync(id);
            if (estate == null)
            {
                return NotFound();
            }

            return View(estate.ToDetailsViewModel());
        }

        // GET: Admin/Estates/Create
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

        // POST: Admin/Estates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                await _estateService.AddAsync(estate.ToEntity(options));
                //_context.Add(estate.ToEntity());
                //await _context.SaveChangesAsync();
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

        // GET: Admin/Estates/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var estate = await _estateService.GetByIdAsync(id);
            if (estate == null)
            {
                return NotFound();
            }
            var options = await _estateOptionService.GetAllAsync();

            ViewData["AgentUserId"] = new SelectList(await _userService.GetAllAsync(), "Id", "UserName");
            ViewData["BuildingPlanId"] = new SelectList(await _buildingPlanService.GetAllAsync(), "Id", "BuildingPlanName");
            ViewData["BuildingTypeId"] = new SelectList(await _buildingTypeService.GetAllAsync(), "Id", "BuildingTypeName");
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "CategoryName");
            ViewData["EstateConditionId"] = new SelectList(await _estateConditionService.GetAllAsync(), "Id", "EstateConditionName");
            ViewData["ZoneId"] = new SelectList(await _zoneService.GetAllAsync(), "Id", "ZoneName");
            return View(estate.ToEditViewModel(options));
        }

        // POST: Admin/Estates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    await _estateService.UpdateAsync(estate);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgentUserId"] = new SelectList(_context.Set<AgentUser>(), "Id", "UserName", estate.AgentUserId);
            ViewData["BuildingPlanId"] = new SelectList(_context.Set<BuildingPlan>(), "Id", "BuildingPlanName", estate.BuildingPlanId);
            ViewData["BuildingTypeId"] = new SelectList(_context.Set<BuildingType>(), "Id", "BuildingTypeName", estate.BuildingTypeId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "CategoryName", estate.CategoryId);
            ViewData["EstateConditionId"] = new SelectList(_context.Set<EstateCondition>(), "EstateConditionId", "EstateConditionName", estate.EstateConditionId);
            ViewData["ZoneId"] = new SelectList(_context.Set<Zone>(), "Id", "ZoneName", estate.ZoneId);
            return View(estate);
        }

        // GET: Admin/Estates/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var estate = await _estateService.GetByIdAsync(id);
            if (estate == null)
            {
                return NotFound();
            }

            return View(estate.ToDetailsViewModel());
        }

        // POST: Admin/Estates/Delete/5
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
