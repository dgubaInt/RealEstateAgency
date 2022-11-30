using RealEstateAgency.Core.DTOs.Category;
using RealEstateAgency.Core.Entities;

namespace RealEstateAgency.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> AddAsync(CreateCategoryDTO postCategoryDTO);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Category category);
    }
}
