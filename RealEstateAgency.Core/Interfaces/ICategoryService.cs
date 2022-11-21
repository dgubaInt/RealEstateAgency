using RealEstateAgency.Core.DTOs;
using RealEstateAgency.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> AddAsync(PostCategoryDTO categoryViewModel);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Category category);
    }
}
