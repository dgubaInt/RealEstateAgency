using RealEstateAgency.Core.Entities;
using RealEstateAgency.Infrastructure.Data;
using RealEstateAgency.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RealEstateAgency.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<AgentUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
