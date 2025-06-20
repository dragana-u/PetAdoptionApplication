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
    public class AdoptionFormsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdoptionFormsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdoptionForms
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AdoptionForms.Include(a => a.Animal).Include(a => a.Applicant);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdoptionForms/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionForm = await _context.AdoptionForms
                .Include(a => a.Animal)
                .Include(a => a.Applicant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoptionForm == null)
            {
                return NotFound();
            }

            return View(adoptionForm);
        }

        // GET: AdoptionForms/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Name");
            ViewData["ApplicantId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: AdoptionForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalId,ApplicantId,SubmittedOn,Status,Message,Id")] AdoptionForm adoptionForm)
        {
            if (ModelState.IsValid)
            {
                adoptionForm.Id = Guid.NewGuid();
                _context.Add(adoptionForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Name", adoptionForm.AnimalId);
            ViewData["ApplicantId"] = new SelectList(_context.Users, "Id", "Id", adoptionForm.ApplicantId);
            return View(adoptionForm);
        }

        // GET: AdoptionForms/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionForm = await _context.AdoptionForms.FindAsync(id);
            if (adoptionForm == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Name", adoptionForm.AnimalId);
            ViewData["ApplicantId"] = new SelectList(_context.Users, "Id", "Id", adoptionForm.ApplicantId);
            return View(adoptionForm);
        }

        // POST: AdoptionForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AnimalId,ApplicantId,SubmittedOn,Status,Message,Id")] AdoptionForm adoptionForm)
        {
            if (id != adoptionForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adoptionForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdoptionFormExists(adoptionForm.Id))
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
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Name", adoptionForm.AnimalId);
            ViewData["ApplicantId"] = new SelectList(_context.Users, "Id", "Id", adoptionForm.ApplicantId);
            return View(adoptionForm);
        }

        // GET: AdoptionForms/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionForm = await _context.AdoptionForms
                .Include(a => a.Animal)
                .Include(a => a.Applicant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoptionForm == null)
            {
                return NotFound();
            }

            return View(adoptionForm);
        }

        // POST: AdoptionForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var adoptionForm = await _context.AdoptionForms.FindAsync(id);
            if (adoptionForm != null)
            {
                _context.AdoptionForms.Remove(adoptionForm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionFormExists(Guid id)
        {
            return _context.AdoptionForms.Any(e => e.Id == id);
        }
    }
}
