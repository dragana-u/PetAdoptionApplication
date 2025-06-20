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
    public class AdoptionFormsService : IAdoptionFormsService
    {
        private readonly IRepository<AdoptionForm> _adoptionFormsRepository;

        public AdoptionFormsService(IRepository<AdoptionForm> adoptionFormsRepository)
        {
            _adoptionFormsRepository = adoptionFormsRepository;
        }

        public AdoptionForm Add(AdoptionForm adoptionForm)
        {
            return _adoptionFormsRepository.Insert(adoptionForm);
        }

        public AdoptionForm DeleteById(Guid Id)
        {
            var adoptionForm = _adoptionFormsRepository.Get(selector: x => x, predicate: y => y.Id == Id);
            return _adoptionFormsRepository.Delete(adoptionForm);
        }

        public List<AdoptionForm> GetAll()
        {
            return _adoptionFormsRepository.GetAll(selector: x => x).ToList();
        }

        public AdoptionForm? GetById(Guid Id)
        {
            return _adoptionFormsRepository.Get(selector: x => x, predicate: y => y.Id == Id);
        }

        public AdoptionForm Update(AdoptionForm adoptionForm)
        {
           return _adoptionFormsRepository.Update(adoptionForm);
        }
    }
}
