using Microsoft.AspNetCore.Identity;
using RealEstateAgency.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.Interfaces
{
    public interface IUserService
    {
        Task<bool> Add(AgentUser user);
        Task<bool> Lockout(AgentUser user);
        Task<bool> UnLockout(AgentUser user);
        Task<IEnumerable<AgentUser>> GetAll();
        Task<AgentUser> GetById(Guid id);
        Task<bool> Update(AgentUser user);
    }
}
