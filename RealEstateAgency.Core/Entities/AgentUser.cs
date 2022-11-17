using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Core.Entities
{
    public class AgentUser : IdentityUser<Guid>
    {
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        public virtual HashSet<Estate> Estates { get; set; }

        public AgentUser()
        {
            Estates = new HashSet<Estate>();
        }
    }
}
