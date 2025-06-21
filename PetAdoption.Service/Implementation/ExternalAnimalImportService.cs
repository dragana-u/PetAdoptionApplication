using PetAdoption.Domain.DomainModels;
using PetAdoption.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Service.Implementation
{
    public class ExternalAnimalImportService : IExternalAnimalImportService
    {
        private readonly IPetfinderApiClient _api;
        private readonly IAnimalsService _animalService;
        private readonly ISpeciesService _speciesService;

        public ExternalAnimalImportService(IPetfinderApiClient api,
                                           IAnimalsService animalService,
                                           ISpeciesService speciesService)
        {
            _api = api;
            _animalService = animalService;
            _speciesService = speciesService;
        }

        public async Task<List<Animal>> ImportAnimalsAsync(string zipCode, string type = "cat")
        {
            var external = await _api.SearchAnimalsAsync(type, zipCode, 20);
            var species = _speciesService.FindByName(type) ?? null;
            var animals = external
                .Where(x => x.status == "adoptable")
                .Select(x => new Animal
                {
                    Id = Guid.NewGuid(),
                    Name = x.name,
                    Age = MapAge(x.age),
                    Gender = x.gender,
                    Breed = x.breeds.primary,
                    Size = x.size,
                    IntakeDate = x.published_at != null
                        ? DateTime.Parse(x.published_at) 
                        : DateTime.MinValue,
                    ImageUrl = x.photos.FirstOrDefault()?.medium,
                    Status = "Available",
                    SpeciesId = species.Id,
                    Shelter = null
                })
                .ToList();

            _animalService.InsertMany(animals);
            return animals;
        }

        private static int MapAge(string? age) => age switch
        {
            "Baby" => 0,
            "Young" => 1,
            "Adult" => 3,
            "Senior" => 8,
            _ => 0
        };
    }
}
