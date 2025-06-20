using PetAdoption.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Service.Interface
{
    public interface IAdoptionSheltersService
    {
        List<AdoptionShelter> GetAll();
        AdoptionShelter? GetById(Guid Id);
        AdoptionShelter Update(AdoptionShelter adoptionShelter);
        AdoptionShelter DeleteById(Guid Id);
        AdoptionShelter Add(AdoptionShelter adoptionShelter);
    }
}
