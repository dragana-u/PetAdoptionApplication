using Microsoft.EntityFrameworkCore;
using PetAdoption.Domain.DomainModels;
using PetAdoption.Repository;
using PetAdoption.Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Service.Implementation
{
    public class AdoptionFormsService : IAdoptionFormsService
    {
        private readonly IRepository<AdoptionForm> _adoptionFormsRepository;
        private readonly IRepository<Animal> _animalRepository;

        public AdoptionFormsService(IRepository<AdoptionForm> adoptionFormsRepository, IRepository<Animal> animalRepository)
        {
            _adoptionFormsRepository = adoptionFormsRepository;
            _animalRepository = animalRepository;
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
            return _adoptionFormsRepository.GetAll(selector: x => x, include: x => x.Include(y => y.Animal).Include(y => y.Applicant)).ToList();
        }

        public AdoptionForm? GetById(Guid Id)
        {
            return _adoptionFormsRepository.Get(selector: x => x, predicate: y => y.Id == Id, include: x => x.Include(y => y.Animal).Include(y => y.Applicant));
        }

        public AdoptionForm Update(AdoptionForm adoptionForm)
        {
            var existing = _adoptionFormsRepository.Get(selector: x => x, predicate: x => x.Id == adoptionForm.Id);
            if (existing == null)
                throw new InvalidOperationException("Entity not found");
            existing.AnimalId = adoptionForm.AnimalId;
            existing.SubmittedOn = adoptionForm.SubmittedOn;
            existing.Status = adoptionForm.Status;
            existing.Message = adoptionForm.Message;
            return _adoptionFormsRepository.Update(existing);
        }

        public bool Adopt(Guid animalId, string applicantId, string? message)
        {
            var animal = _animalRepository.Get(
                selector: a => a,
                predicate: a => a.Id == animalId,
                include: a => a.Include(x => x.AdoptionForms));

            if (animal == null)
                throw new ArgumentException("Animal not found.", nameof(animalId));

            if (animal.Status != "Available")
                return false;

            bool alreadyPending = animal.AdoptionForms?
                .Any(f => f.ApplicantId == applicantId && f.Status == "Pending") ?? false;

            if (alreadyPending)
                return false;

            var form = new AdoptionForm
            {
                Id = Guid.NewGuid(),
                AnimalId = animal.Id,
                ApplicantId = applicantId,
                SubmittedOn = DateTime.UtcNow,
                Status = "Pending",
                Message = message
            };
            _adoptionFormsRepository.Insert(form);

            animal.Status = "Reserved";
            _animalRepository.Update(animal);

            return true;
        }

    }
}
