using Microsoft.EntityFrameworkCore;
using PetAdoption.Domain.DomainModels;
using PetAdoption.Repository;
using PetAdoption.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Service.Implementation
{
    public class AnimalsService : IAnimalsService
    {
        private readonly IRepository<Animal> _animalRepository;

        public AnimalsService(IRepository<Animal> AnimalRepository)
        {
            _animalRepository = AnimalRepository;
        }

        public Animal Add(Animal Animal)
        {
            return _animalRepository.Insert(Animal);
        }

        public Animal DeleteById(Guid Id)
        {
            var Animal = _animalRepository.Get(selector: x => x, predicate: y => y.Id == Id);
            return _animalRepository.Delete(Animal);
        }

        public List<Animal> GetAll()
        {
            return _animalRepository.GetAll(selector: x => x, include: x => x.Include(y => y.AdoptionForms)).ToList();
        }

        public Animal? GetById(Guid Id)
        {
            return _animalRepository.Get(selector: x => x, predicate: y => y.Id == Id);
        }

        public Animal Update(Animal Animal)
        {
            return _animalRepository.Update(Animal);
        }
        public ICollection<Animal> InsertMany(ICollection<Animal> animals)
        {
            return _animalRepository.InsertMany(animals);
        }
    }
}
