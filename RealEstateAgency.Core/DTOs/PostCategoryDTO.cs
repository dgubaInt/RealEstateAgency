using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.DTOs
{
    public class PostCategoryDTO
    {
        public string CategoryName { get; set; }
        public Guid ParentCategoryId { get; set; }
    }
}
