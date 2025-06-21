using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Domain.DomainModels;
using PetAdoption.Repository;
using PetAdoption.Service.Implementation;
using PetAdoption.Service.Interface;

namespace PetAdoption.Web.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IAnimalsService _animalsService;
        private readonly ISpeciesService _speciesService;

        public AnimalsController(IAnimalsService animalsService, ISpeciesService speciesService)
        {
            _animalsService = animalsService;
            _speciesService = speciesService;
        }

        // GET: Animals
        public IActionResult Index()
        {
            return View(_animalsService.GetAll());
        }

        // GET: Animals/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = _animalsService.GetById(id.Value);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            ViewData["SpeciesId"] = new SelectList(_speciesService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description,Age,Gender,Breed,Size,IntakeDate,ImageUrl,Status,SpeciesId,Id")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                animal.Id = Guid.NewGuid();
                _animalsService.Add(animal);
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpeciesId"] = new SelectList(_speciesService.GetAll(), "Id", "Name");
            return View(animal);
        }

        // GET: Animals/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = _animalsService.GetById(id.Value);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["SpeciesId"] = new SelectList(_speciesService.GetAll(), "Id", "Name");
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Age,Gender,Breed,Size,IntakeDate,ImageUrl,Status,SpeciesId,Id")] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _animalsService.Update(animal);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
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
            ViewData["SpeciesId"] = new SelectList(_speciesService.GetAll(), "Id", "Name");
            return View(animal);
        }

        // GET: Animals/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = _animalsService.GetById(id.Value);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var animal = _animalsService.GetById(id);
            if (animal != null)
            {
                _animalsService.DeleteById(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(Guid id)
        {
            return _animalsService.GetById(id) != null;
        }
    }
}
