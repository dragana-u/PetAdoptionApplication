using PetAdoption.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Service.Interface
{
    public interface IAdoptionFormsService
    {
        List<AdoptionForm> GetAll();
        AdoptionForm? GetById(Guid Id);
        AdoptionForm Update(AdoptionForm adoptionForm);
        AdoptionForm DeleteById(Guid Id);
        AdoptionForm Add(AdoptionForm adoptionForm);
    }
}
