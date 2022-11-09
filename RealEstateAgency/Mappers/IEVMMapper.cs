using Microsoft.AspNetCore.Identity;
using RealEstateAgencyMVC.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgencyMVC.Mappers
{
    public interface IEVMMapper
    {
        Task<List<UserViewModel>> EVMMapAll(IEnumerable<IdentityUser> users);
    }
}
