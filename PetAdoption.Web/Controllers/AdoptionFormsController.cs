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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PetAdoption.Web.Controllers
{
    public class AdoptionFormsController : Controller
    {
        private readonly IAdoptionFormsService _adoptionFormsService;
        private readonly IAnimalsService _animalsService;


        public AdoptionFormsController(IAdoptionFormsService adoptionFormsService, IAnimalsService animalsService)
        {
            _adoptionFormsService = adoptionFormsService;
            _animalsService = animalsService;
        }

        // GET: AdoptionForms
        public IActionResult Index()
        {
            return View(_adoptionFormsService.GetAll());
        }

        // GET: AdoptionForms/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionForm = _adoptionFormsService.GetById(id.Value);
            if (adoptionForm == null)
            {
                return NotFound();
            }

            return View(adoptionForm);
        }

        // GET: AdoptionForms/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_animalsService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: AdoptionForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("AnimalId,SubmittedOn,Status,Message,Id")] AdoptionForm adoptionForm)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }

            adoptionForm.ApplicantId = userIdString;

            if (ModelState.IsValid)
            {
                adoptionForm.Id = Guid.NewGuid();
                _adoptionFormsService.Add(adoptionForm);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_animalsService.GetAll(), "Id", "Name");
            return View(adoptionForm);
        }

        // GET: AdoptionForms/Edit/5
        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionForm = _adoptionFormsService.GetById(id.Value);
            if (adoptionForm == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_animalsService.GetAll(), "Id", "Name");
            return View(adoptionForm);
        }

        // POST: AdoptionForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(Guid id, [Bind("AnimalId,SubmittedOn,Status,Message,Id")] AdoptionForm adoptionForm)
        {
            if (id != adoptionForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _adoptionFormsService.Update(adoptionForm);
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
            ViewData["AnimalId"] = new SelectList(_animalsService.GetAll(), "Id", "Name");
            return View(adoptionForm);
        }

        // GET: AdoptionForms/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionForm = _adoptionFormsService.GetById(id.Value);
            if (adoptionForm == null)
            {
                return NotFound();
            }

            return View(adoptionForm);
        }

        // POST: AdoptionForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var adoptionForm = _adoptionFormsService.GetById(id);
            if (adoptionForm != null)
            {
                _adoptionFormsService.DeleteById(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionFormExists(Guid id)
        {
            return _adoptionFormsService.GetById(id) != null;
        }
    }
}
