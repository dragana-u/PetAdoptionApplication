using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Domain.DomainModels
{
    public class Animal : BaseEntity
    {
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Age { get; set; }

        public string? Gender { get; set; }

        public string? Breed { get; set; }

        public string? Size { get; set; }

        public DateTime IntakeDate { get; set; }

        public string? ImageUrl { get; set; }

        public string? Status { get; set; }

        public Guid? SpeciesId { get; set; }
        public virtual Species? Species { get; set; }

        public virtual ICollection<AdoptionForm>? AdoptionForms { get; set; }
        public  AdoptionShelter? Shelter { get; set; }
    }
}
