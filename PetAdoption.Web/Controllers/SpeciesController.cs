using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Domain.DomainModels;
using PetAdoption.Repository;
using PetAdoption.Service.Implementation;
using PetAdoption.Service.Interface;

namespace PetAdoption.Web.Controllers
{
    public class SpeciesController : Controller
    {
        private readonly ISpeciesService _speciesService;

        public SpeciesController(ISpeciesService speciesService)
        {
            this._speciesService = speciesService;
        }

        // GET: Species
        public IActionResult Index()
        {
            return View(_speciesService.GetAll());
        }

        // GET: Species/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var species = _speciesService.GetById(id.Value);
            if (species == null)
            {
                return NotFound();
            }

            return View(species);
        }

        // GET: Species/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Species/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("Name,Id")] Species species)
        {
            if (ModelState.IsValid)
            {
                species.Id = Guid.NewGuid();
                _speciesService.Add(species);
                return RedirectToAction(nameof(Index));
            }
            return View(species);
        }

        // GET: Species/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var species = _speciesService.GetById(id.Value);
            if (species == null)
            {
                return NotFound();
            }
            return View(species);
        }

        // POST: Species/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid id, [Bind("Name,Id")] Species species)
        {
            if (id != species.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _speciesService.Update(species);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeciesExists(species.Id))
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
            return View(species);
        }

        // GET: Species/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var species = _speciesService.GetById(id.Value);
            if (species == null)
            {
                return NotFound();
            }

            return View(species);
        }

        // POST: Species/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var species = _speciesService.GetById(id);
            if (species != null)
            {
                _speciesService.DeleteById(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SpeciesExists(Guid id)
        {
            return _speciesService.GetById(id) != null;
        }
    }
}
