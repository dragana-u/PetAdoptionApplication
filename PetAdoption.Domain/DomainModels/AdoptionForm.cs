using PetAdoption.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Domain.DomainModels
{
    public class AdoptionForm : BaseEntity
    {
        public Guid ? AnimalId { get; set; }
        public virtual Animal? Animal { get; set; }

        public string? ApplicantId { get; set; }
        public virtual PetAdoptionApplicationUser? Applicant { get; set; }

        public DateTime? SubmittedOn { get; set; }

        public string? Status { get; set; }

        public string? Message { get; set; }
    }
}
