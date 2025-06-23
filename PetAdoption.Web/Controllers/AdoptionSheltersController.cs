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
using PetAdoption.Service.Interface;

namespace PetAdoption.Web.Controllers
{
    public class AdoptionSheltersController : Controller
    {
        private readonly IAdoptionSheltersService _adoptionSheltersService;

        public AdoptionSheltersController(IAdoptionSheltersService adoptionSheltersService)
        {
            _adoptionSheltersService = adoptionSheltersService;
        }


        // GET: AdoptionShelters
        public IActionResult Index()
        {
            return View(_adoptionSheltersService.GetAll());
        }

        // GET: AdoptionShelters/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionShelter = _adoptionSheltersService.GetById(id.Value);
            if (adoptionShelter == null)
            {
                return NotFound();
            }

            return View(adoptionShelter);
        }

        // GET: AdoptionShelters/Create

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdoptionShelters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("Name,Address,PhoneNumber,Website,Id")] AdoptionShelter adoptionShelter)
        {
            if (ModelState.IsValid)
            {
                adoptionShelter.Id = Guid.NewGuid();
                _adoptionSheltersService.Add(adoptionShelter);
                return RedirectToAction(nameof(Index));
            }
            return View(adoptionShelter);
        }

        // GET: AdoptionShelters/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionShelter = _adoptionSheltersService.GetById(id.Value);
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
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid id, [Bind("Name,Address,PhoneNumber,Website,Id")] AdoptionShelter adoptionShelter)
        {
            if (id != adoptionShelter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _adoptionSheltersService.Update(adoptionShelter);
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
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionShelter = _adoptionSheltersService.GetById(id.Value);
            if (adoptionShelter == null)
            {
                return NotFound();
            }

            return View(adoptionShelter);
        }

        // POST: AdoptionShelters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var adoptionShelter = _adoptionSheltersService.GetById(id);
            if (adoptionShelter != null)
            {
                _adoptionSheltersService.DeleteById(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionShelterExists(Guid id)
        {
            return _adoptionSheltersService.GetById(id) != null;
        }
    }
}
