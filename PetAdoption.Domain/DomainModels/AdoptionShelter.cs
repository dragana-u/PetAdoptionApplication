using PetAdoption.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Domain.DomainModels
{
    public class AdoptionShelter : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Website { get; set; }
        public virtual ICollection<Animal>? Animals { get; set; }
    }
}
