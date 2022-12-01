using Microsoft.AspNetCore.Identity;
using RealEstateAgency.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.Entities
{
    public class Role : IdentityRole<Guid>, IBaseEntity
    {
    }
}
