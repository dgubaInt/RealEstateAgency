using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Infrastructure.Data;

namespace RealEstateAgencyMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EstatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Estates
        public async Task<IActionResult> Index()
        {
            var realEstateAgencyMVCContext = _context.Estates.Include(e => e.AgentUser).Include(e => e.BuildingPlan).Include(e => e.BuildingType).Include(e => e.Category).Include(e => e.EstateCondition).Include(e => e.Map).Include(e => e.Zone);
            return View(await realEstateAgencyMVCContext.ToListAsync());
        }

        // GET: Admin/Estates/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Estates == null)
            {
                return NotFound();
            }

            var estate = await _context.Estates
                .Include(e => e.AgentUser)
                .Include(e => e.BuildingPlan)
                .Include(e => e.BuildingType)
                .Include(e => e.Category)
                .Include(e => e.EstateCondition)
                .Include(e => e.Map)
                .Include(e => e.Zone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estate == null)
            {
                return NotFound();
            }

            return View(estate);
        }

        // GET: Admin/Estates/Create
        public IActionResult Create()
        {
            ViewData["AgentUserId"] = new SelectList(_context.Set<AgentUser>(), "Id", "FirstName");
            ViewData["BuildingPlanId"] = new SelectList(_context.Set<BuildingPlan>(), "BuildingPlanId", "BuildingPlanName");
            ViewData["BuildingTypeId"] = new SelectList(_context.Set<BuildingType>(), "BuildingTypeId", "BuildingTypeName");
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryName");
            ViewData["EstateConditionId"] = new SelectList(_context.Set<EstateCondition>(), "EstateConditionId", "EstateConditionName");
            ViewData["MapId"] = new SelectList(_context.Set<Map>(), "MapId", "Description");
            ViewData["ZoneId"] = new SelectList(_context.Set<Zone>(), "ZoneId", "ZoneName");
            return View();
        }

        // POST: Admin/Estates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstateId,EstateName,Description,Address,Tags,Rooms,BathRooms,Balconies,ParkingSpaces,TotalArea,LivingArea,KitchenArea,Price,Currency,CreatedDate,CategoryId,AgentUserId,BuildingPlanId,BuildingTypeId,ZoneId,MapId,EstateConditionId")] Estate estate)
        {
            if (ModelState.IsValid)
            {
                estate.Id = Guid.NewGuid();
                _context.Add(estate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgentUserId"] = new SelectList(_context.Set<AgentUser>(), "Id", "FirstName", estate.AgentUserId);
            ViewData["BuildingPlanId"] = new SelectList(_context.Set<BuildingPlan>(), "BuildingPlanId", "BuildingPlanName", estate.BuildingPlanId);
            ViewData["BuildingTypeId"] = new SelectList(_context.Set<BuildingType>(), "BuildingTypeId", "BuildingTypeName", estate.BuildingTypeId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryName", estate.CategoryId);
            ViewData["EstateConditionId"] = new SelectList(_context.Set<EstateCondition>(), "EstateConditionId", "EstateConditionName", estate.EstateConditionId);
            ViewData["MapId"] = new SelectList(_context.Set<Map>(), "MapId", "Description", estate.MapId);
            ViewData["ZoneId"] = new SelectList(_context.Set<Zone>(), "ZoneId", "ZoneName", estate.ZoneId);
            return View(estate);
        }

        // GET: Admin/Estates/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Estates == null)
            {
                return NotFound();
            }

            var estate = await _context.Estates.FindAsync(id);
            if (estate == null)
            {
                return NotFound();
            }
            ViewData["AgentUserId"] = new SelectList(_context.Set<AgentUser>(), "Id", "FirstName", estate.AgentUserId);
            ViewData["BuildingPlanId"] = new SelectList(_context.Set<BuildingPlan>(), "BuildingPlanId", "BuildingPlanName", estate.BuildingPlanId);
            ViewData["BuildingTypeId"] = new SelectList(_context.Set<BuildingType>(), "BuildingTypeId", "BuildingTypeName", estate.BuildingTypeId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryName", estate.CategoryId);
            ViewData["EstateConditionId"] = new SelectList(_context.Set<EstateCondition>(), "EstateConditionId", "EstateConditionName", estate.EstateConditionId);
            ViewData["MapId"] = new SelectList(_context.Set<Map>(), "MapId", "Description", estate.MapId);
            ViewData["ZoneId"] = new SelectList(_context.Set<Zone>(), "ZoneId", "ZoneName", estate.ZoneId);
            return View(estate);
        }

        // POST: Admin/Estates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EstateId,EstateName,Description,Address,Tags,Rooms,BathRooms,Balconies,ParkingSpaces,TotalArea,LivingArea,KitchenArea,Price,Currency,CreatedDate,CategoryId,AgentUserId,BuildingPlanId,BuildingTypeId,ZoneId,MapId,EstateConditionId")] Estate estate)
        {
            if (id != estate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstateExists(estate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgentUserId"] = new SelectList(_context.Set<AgentUser>(), "Id", "FirstName", estate.AgentUserId);
            ViewData["BuildingPlanId"] = new SelectList(_context.Set<BuildingPlan>(), "BuildingPlanId", "BuildingPlanName", estate.BuildingPlanId);
            ViewData["BuildingTypeId"] = new SelectList(_context.Set<BuildingType>(), "BuildingTypeId", "BuildingTypeName", estate.BuildingTypeId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryName", estate.CategoryId);
            ViewData["EstateConditionId"] = new SelectList(_context.Set<EstateCondition>(), "EstateConditionId", "EstateConditionName", estate.EstateConditionId);
            ViewData["MapId"] = new SelectList(_context.Set<Map>(), "MapId", "Description", estate.MapId);
            ViewData["ZoneId"] = new SelectList(_context.Set<Zone>(), "ZoneId", "ZoneName", estate.ZoneId);
            return View(estate);
        }

        // GET: Admin/Estates/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Estates == null)
            {
                return NotFound();
            }

            var estate = await _context.Estates
                .Include(e => e.AgentUser)
                .Include(e => e.BuildingPlan)
                .Include(e => e.BuildingType)
                .Include(e => e.Category)
                .Include(e => e.EstateCondition)
                .Include(e => e.Map)
                .Include(e => e.Zone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estate == null)
            {
                return NotFound();
            }

            return View(estate);
        }

        // POST: Admin/Estates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Estates == null)
            {
                return Problem("Entity set 'RealEstateAgencyMVCContext.Estate'  is null.");
            }
            var estate = await _context.Estates.FindAsync(id);
            if (estate != null)
            {
                _context.Estates.Remove(estate);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstateExists(Guid id)
        {
          return _context.Estates.Any(e => e.Id == id);
        }
    }
}
