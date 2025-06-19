using PetAdoption.Domain.Identity;
using PetAdoption.Domain.DomainModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace PetAdoption.Domain.DomainModels
{
    public class AnimalForAdoption : BaseEntity
    {
        public string ? Name { get; set; }
        public string ? Species { get; set; }

        public string? Breed { get; set; }

        public int? AgeInMonths { get; set; }

        public string? Description { get; set; }

        public string? PhotoUrl { get; set; }

        public string? OwnerId { get; set; }
        public PetAdoptionApplicationUser? Owner { get; set; }
    }
}
