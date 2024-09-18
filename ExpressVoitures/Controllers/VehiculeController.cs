using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Data;
using ExpressVoitures.Models.Entities;
using ExpressVoitures.Models.Services;

namespace ExpressVoitures.Controllers
{
    public class VehiculeController : Controller
    {
        private readonly IVehiculeService _vehiculeService;
        private readonly IAnneeService _anneeService;
        private readonly IFinitionService _finitionService;
        private readonly IReparationService _reparationService;
        private readonly IModeleMarqueService _modeleMarqueService;
        private readonly IImagesService _imagesService;

        public VehiculeController(IVehiculeService vehiculeService, IAnneeService anneeService, IFinitionService finitionService, IReparationService reparationService, IModeleMarqueService modeleMarqueService, IImagesService imagesService)
        {
            _vehiculeService = vehiculeService;
            _anneeService = anneeService;
            _finitionService = finitionService;
            _reparationService = reparationService;
            _modeleMarqueService = modeleMarqueService;
            _imagesService = imagesService;
        }

        // GET: Vehicule
        public async Task<IActionResult> Index()
        {
            var vehicules = _vehiculeService.GetAllVehicules();
            return View(vehicules);
        }

        // GET: Vehicule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicule = _vehiculeService.GetVehiculeById(id.Value);
            if (vehicule == null)
            {
                return NotFound();
            }

            return View(vehicule);
        }

        // GET: Vehicule/Create
        public IActionResult Create()
        {
            // Générer une liste des années de 1990 à l'année en cours
            var annees = Enumerable.Range(1990, DateTime.Now.Year - 1990 + 1)
                                   .Select(x => new { Id = x, Valeur = x.ToString() })
                                   .ToList();

            // Injecter la liste des années dans ViewData pour l'utiliser dans le menu déroulant
            ViewData["AnneeId"] = new SelectList(annees, "Id", "Valeur");

            ViewData["FinitionId"] = new SelectList(_finitionService.GetAllFinitions(), "Id", "Nom");
            ViewData["ModeleMarqueId"] = new SelectList(_modeleMarqueService.GetAllModeleMarques(), "Id", "NomComplet");
            return View();
        }

        // POST: Vehicule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnneeId,ModeleMarqueId,FinitionId,Reparation.Cout,Reparation.Description,Disponible,DateVente,DateAchat,PrixAchat,Description,Image.Url")] Vehicule vehicule, Image image)
        {
            if (ModelState.IsValid)
            {
                // Si une description et un coût sont fournis, créer la réparation
                if (!string.IsNullOrEmpty(vehicule.Reparation?.Description) && vehicule.Reparation.Cout > 0)
                {
                    _reparationService.AjouterReparation(vehicule.Reparation);

                    // Associe la nouvelle réparation au véhicule
                    vehicule.ReparationId = vehicule.Reparation.Id;
                }
                // Ajouter le véhicule en premier
                _vehiculeService.AjouterVehicule(vehicule);

                // Après l'ajout du véhicule, associer l'image (si elle existe)
                if (!string.IsNullOrEmpty(image?.Url))
                {
                    image.VehiculeId = vehicule.Id; // Associe l'image avec le véhicule
                    _imagesService.AjouterImage(image);
                }

                return RedirectToAction(nameof(Index));
            }

            var annees = Enumerable.Range(1990, DateTime.Now.Year - 1990 + 1)
                          .Select(x => new { Id = x, Valeur = x.ToString() })
                          .ToList();

            ViewData["AnneeId"] = new SelectList(annees, "Id", "Valeur", vehicule.AnneeId);
            ViewData["FinitionId"] = new SelectList(_finitionService.GetAllFinitions(), "Id", "Nom", vehicule.FinitionId);
            ViewData["ModeleMarqueId"] = new SelectList(_modeleMarqueService.GetAllModeleMarques(), "Id", "NomComplet", vehicule.ModeleMarqueId);
            return View();
        }

        // GET: Vehicule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicule = _vehiculeService.GetVehiculeById(id.Value);
            if (vehicule == null)
            {
                return NotFound();
            }
            var annees = Enumerable.Range(1990, DateTime.Now.Year - 1990 + 1)
                          .Select(x => new { Id = x, Valeur = x.ToString() })
                          .ToList();

            ViewData["AnneeId"] = new SelectList(annees, "Id", "Valeur", vehicule.AnneeId);
            ViewData["FinitionId"] = new SelectList(_finitionService.GetAllFinitions(), "Id", "Id", vehicule.FinitionId);
            ViewData["ModeleMarqueId"] = new SelectList(_modeleMarqueService.GetAllModeleMarques(), "Id", "Id", vehicule.ModeleMarqueId);
            return View(vehicule);
        }

        // POST: Vehicule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnneeId,ModeleMarqueId,FinitionId,ReparationId,Disponible,DateVente,DateAchat,PrixAchat,Description")] Vehicule vehicule)
        {
            if (id != vehicule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _vehiculeService.ModifierVehicule(vehicule);                 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_vehiculeService.VehiculeExists(vehicule.Id))
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
            var annees = Enumerable.Range(1990, DateTime.Now.Year - 1990 + 1)
                          .Select(x => new { Id = x, Valeur = x.ToString() })
                          .ToList();

            ViewData["AnneeId"] = new SelectList(annees, "Id", "Valeur", vehicule.AnneeId);
            ViewData["FinitionId"] = new SelectList(_finitionService.GetAllFinitions(), "Id", "Id", vehicule.FinitionId);
            ViewData["ModeleMarqueId"] = new SelectList(_modeleMarqueService.GetAllModeleMarques(), "Id", "Id", vehicule.ModeleMarqueId);
            return View(vehicule);
        }

        // GET: Vehicule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicule = _vehiculeService.GetVehiculeById(id.Value);
            if (vehicule == null)
            {
                return NotFound();
            }

            return View(vehicule);
        }

        // POST: Vehicule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _vehiculeService.SupprimerVehicule(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
