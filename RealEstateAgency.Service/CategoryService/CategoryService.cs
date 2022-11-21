using RealEstateAgency.Core.DTOs;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Service.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> AddAsync(PostCategoryDTO postCategoryDTO)
        {
            try
            {
                var parentCategory = await _categoryRepository.GetByIdAsync(postCategoryDTO.ParentCategoryId);

                var category = new Category
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = postCategoryDTO.CategoryName,
                    ParentCategory = parentCategory,
                    CreatedDate = DateTime.Now
                }; 
                await _categoryRepository.AddAsync(category);
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            return await _categoryRepository.UpdateAsync(category);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                return await _categoryRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
