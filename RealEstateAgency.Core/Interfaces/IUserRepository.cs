using Microsoft.AspNetCore.Identity;
using RealEstateAgency.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<IdentityUser>
    {
    }
}
