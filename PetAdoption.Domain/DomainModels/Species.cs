using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Domain.DomainModels
{
    public class Species : BaseEntity
    {
        [Required]
        public string? Name { get; set; }

        public virtual ICollection<Animal>? Animals { get; set; }
    }
}
