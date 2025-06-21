using PetAdoption.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Service.Interface
{
    public interface IExternalAnimalImportService
    {
        Task<List<Animal>> ImportAnimalsAsync(string zipCode, string type = "cat");
    }
}
