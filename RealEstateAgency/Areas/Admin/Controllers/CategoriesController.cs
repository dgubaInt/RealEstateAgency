using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Core.DTOs.Category;
using RealEstateAgency.Core.Interfaces;

namespace RealEstateAgencyMVC.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> GetCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();

                var categoriesList = new List<CategoryDTO>();

                foreach (var category in categories)
                {
                    categoriesList.Add(new CategoryDTO
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName,
                        ParentCategoryId = category.ParentCategoryId,
                        Position = category.Position
                    });
                }
                return Json(new { Result = "OK", Records = categoriesList, TotalRecordCount = categoriesList.Count });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // GET: api/Categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                ParentCategoryId = category.ParentCategoryId,
                Position = category.Position
            });
        }

        // POST: api/Categories
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> PostCategory([FromForm] CreateCategoryDTO postCategoryDTO)
        {
            try
            {
                var category = await _categoryService.AddAsync(postCategoryDTO);
                return Json(new { Result = "OK", Record = category });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // PUT: api/Categories/{id}
        [HttpPost, Route("[action]")]
        public async Task<JsonResult> PutCategory([FromForm] CategoryDTO categoryDTO)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(categoryDTO.CategoryId);
                if (category != null)
                {
                    category.CategoryName = categoryDTO.CategoryName;
                    category.ParentCategoryId = categoryDTO.ParentCategoryId;
                    category.Position = categoryDTO.Position;
                    await _categoryService.UpdateAsync(category);
                }

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // DELETE: api/Categories/{id}
        [HttpDelete("{id}"), Route("[action]")]
        public async Task<JsonResult> DeleteCategory([FromForm] Guid categoryId)
        {
            try
            {
                await _categoryService.DeleteAsync(categoryId);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
