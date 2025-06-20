using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Domain.DomainModels;
using PetAdoption.Repository;

namespace PetAdoption.Web.Controllers
{
    public class AdoptionSheltersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdoptionSheltersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdoptionShelters
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdoptionShelters.ToListAsync());
        }

        // GET: AdoptionShelters/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionShelter = await _context.AdoptionShelters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoptionShelter == null)
            {
                return NotFound();
            }

            return View(adoptionShelter);
        }

        // GET: AdoptionShelters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdoptionShelters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,PhoneNumber,Website,Id")] AdoptionShelter adoptionShelter)
        {
            if (ModelState.IsValid)
            {
                adoptionShelter.Id = Guid.NewGuid();
                _context.Add(adoptionShelter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adoptionShelter);
        }

        // GET: AdoptionShelters/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionShelter = await _context.AdoptionShelters.FindAsync(id);
            if (adoptionShelter == null)
            {
                return NotFound();
            }
            return View(adoptionShelter);
        }

        // POST: AdoptionShelters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Address,PhoneNumber,Website,Id")] AdoptionShelter adoptionShelter)
        {
            if (id != adoptionShelter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adoptionShelter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdoptionShelterExists(adoptionShelter.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(adoptionShelter);
        }

        // GET: AdoptionShelters/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionShelter = await _context.AdoptionShelters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoptionShelter == null)
            {
                return NotFound();
            }

            return View(adoptionShelter);
        }

        // POST: AdoptionShelters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var adoptionShelter = await _context.AdoptionShelters.FindAsync(id);
            if (adoptionShelter != null)
            {
                _context.AdoptionShelters.Remove(adoptionShelter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionShelterExists(Guid id)
        {
            return _context.AdoptionShelters.Any(e => e.Id == id);
        }
    }
}
