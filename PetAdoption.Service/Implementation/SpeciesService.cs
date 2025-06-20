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
    public class SpeciesService : ISpeciesService
    {
        private readonly IRepository<Species> _SpeciesRepository;

        public SpeciesService(IRepository<Species> SpeciesRepository)
        {
            _SpeciesRepository = SpeciesRepository;
        }

        public Species Add(Species Species)
        {
            return _SpeciesRepository.Insert(Species);
        }

        public Species DeleteById(Guid Id)
        {
            var Species = _SpeciesRepository.Get(selector: x => x, predicate: y => y.Id == Id);
            return _SpeciesRepository.Delete(Species);
        }

        public List<Species> GetAll()
        {
            return _SpeciesRepository.GetAll(selector: x => x, include: x => x.Include(y => y.Animals)).ToList();
        }

        public Species? GetById(Guid Id)
        {
            return _SpeciesRepository.Get(selector: x => x, predicate: y => y.Id == Id);
        }

        public Species Update(Species Species)
        {
            return _SpeciesRepository.Update(Species);
        }
    }
}
