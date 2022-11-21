using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
