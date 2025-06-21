using PetAdoption.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Service.Interface
{
    public interface IPetfinderApiClient
    {
        Task<IReadOnlyList<PetfinderAnimal>> SearchAnimalsAsync(string type, string zip, int limit = 10);
    }
}
