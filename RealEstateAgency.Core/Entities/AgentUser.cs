using Microsoft.AspNetCore.Identity;
using RealEstateAgency.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Core.Entities
{
    public class AgentUser : IdentityUser<Guid>, IBaseEntity
    {
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        public virtual List<Estate> Estates { get; set; }

        public AgentUser()
        {
            Estates = new List<Estate>();
        }
    }
}
