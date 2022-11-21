using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.DTOs
{
    public class CategoryDTO
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
