using PetAdoption.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Service.Interface
{
    public interface IAnimalsService
    {
        List<Animal> GetAll();
        Animal? GetById(Guid Id);
        Animal Update(Animal animal);
        Animal DeleteById(Guid Id);
        Animal Add(Animal animal);
        public ICollection<Animal> InsertMany(ICollection<Animal> animals);
    }
}
