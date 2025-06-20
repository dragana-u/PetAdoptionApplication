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
    public class AdoptionSheltersService : IAdoptionSheltersService
    {
        private readonly IRepository<AdoptionShelter> _adoptionShelterRepository;

        public AdoptionSheltersService(IRepository<AdoptionShelter> adoptionShelterRepository)
        {
            _adoptionShelterRepository = adoptionShelterRepository;
        }

        public AdoptionShelter Add(AdoptionShelter adoptionShelter)
        {
            return _adoptionShelterRepository.Insert(adoptionShelter);
        }

        public AdoptionShelter DeleteById(Guid Id)
        {
            var adoptionShelter = _adoptionShelterRepository.Get(selector: x => x, predicate: y => y.Id == Id);
            return _adoptionShelterRepository.Delete(adoptionShelter);
        }

        public List<AdoptionShelter> GetAll()
        {
            return _adoptionShelterRepository.GetAll(selector: x => x, include: x => x.Include(y => y.Animals)).ToList();
        }

        public AdoptionShelter? GetById(Guid Id)
        {
            return _adoptionShelterRepository.Get(selector: x => x, predicate: y => y.Id == Id);
        }

        public AdoptionShelter Update(AdoptionShelter adoptionShelter)
        {
            return _adoptionShelterRepository.Update(adoptionShelter);
        }
    }
}
