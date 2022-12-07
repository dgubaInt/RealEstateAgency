using RealEstateAgency.Core.DTOs.Category;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;

namespace RealEstateAgency.Service.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
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

        public async Task<Category> AddAsync(CreateCategoryDTO postCategoryDTO)
        {
            try
            {
                var parentCategory = await GetByIdAsync(postCategoryDTO.ParentCategoryId);

                var category = new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = postCategoryDTO.CategoryName,
                    ParentCategory = parentCategory,
                    Position = postCategoryDTO.Position,
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
