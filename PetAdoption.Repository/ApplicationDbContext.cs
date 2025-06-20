using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Domain.DomainModels;
using PetAdoption.Domain.Identity;

namespace PetAdoption.Repository;

public class ApplicationDbContext : IdentityDbContext<PetAdoptionApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Species> Species { get; set; }
    public DbSet<AdoptionForm> AdoptionForms { get; set; }
    public DbSet<AdoptionShelter> AdoptionShelters { get; set; }
}
