using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Core.DTOs;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Service.CategoryService;
using System.Data;

namespace RealEstateAgencyMVC.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();

            var categoriesList = new List<CategoryDTO>();

            foreach (var category in categories)
            {
                categoriesList.Add(new CategoryDTO
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    ParentCategoryId = category.ParentCategoryId
                });
            }

            return Ok(categoriesList);
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
                ParentCategoryId = category.ParentCategoryId
            });
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostCategory([FromForm] PostCategoryDTO postCategoryDTO)
        {
            var category = await _categoryService.AddAsync(postCategoryDTO);
            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId}, new CategoryDTO { 
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                ParentCategoryId = category.ParentCategoryId
            });
        }

        // PUT: api/Categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
            {
                return BadRequest();
            }

            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            category.CategoryName = categoryDTO.CategoryName;
            category.ParentCategoryId = categoryDTO.ParentCategoryId;
            await _categoryService.UpdateAsync(category);

            return NoContent();
        }

        // DELETE: api/Categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }

            return NoContent();
        }
    }
}
