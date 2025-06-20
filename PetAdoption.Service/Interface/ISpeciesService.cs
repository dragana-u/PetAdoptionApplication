using PetAdoption.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Service.Interface
{
    public interface ISpeciesService
    {
        List<Species> GetAll();
        Species? GetById(Guid Id);
        Species Update(Species species);
        Species DeleteById(Guid Id);
        Species Add(Species species);
    }
}
