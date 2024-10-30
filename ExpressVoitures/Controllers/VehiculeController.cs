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
using ExpressVoitures.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ExpressVoitures.Controllers
{
    public class VehiculeController : Controller
    {
        private readonly IVehiculeService _vehiculeService;
        private readonly IAnneeService _anneeService;
        private readonly IFinitionService _finitionService;
        private readonly IReparationService _reparationService;
        private readonly IModeleService _modeleService;
        private readonly IMarqueService _marqueService;
        private readonly IImagesService _imagesService;

        public VehiculeController(IVehiculeService vehiculeService, IAnneeService anneeService, IFinitionService finitionService, IReparationService reparationService, IMarqueService marqueService, IModeleService modeleService, IImagesService imagesService)
        {
            _vehiculeService = vehiculeService;
            _anneeService = anneeService;
            _finitionService = finitionService;
            _reparationService = reparationService;
            _modeleService = modeleService;
            _marqueService = marqueService;
            _imagesService = imagesService;
        }

        // GET: Vehicule
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var vehicules = _vehiculeService.GetAllVehicules();
            return View(vehicules);
        }

        // GET: Vehicule/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicule = await _vehiculeService.GetVehiculeById(id.Value);
            if (vehicule == null)
            {
                return NotFound();
            }

            // Récupérer les images associées au véhicule
            vehicule.ListImage = await _imagesService.GetImagesByVehiculeId(id.Value);

            return View(vehicule);
        }

        // GET: Vehicule/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var viewModel = new VehiculeViewModel
            {
                Annees = _anneeService.GetAllAnnees(),
                Marques = new SelectList(_marqueService.GetAllMarques(), "Id", "Nom"),
                Modeles = new SelectList(_modeleService.GetAllModele(), "Id", "Nom"),
                Finitions = new SelectList(_finitionService.GetAllFinition(), "Id", "Nom"),
            };

            return View(viewModel);
        }

        // POST: Vehicule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(VehiculeViewModel viewModel)
        {           
            if (ModelState.IsValid)
            {
                // Ajout de la nouvelle marque si elle est fournie
                if (!string.IsNullOrEmpty(viewModel.NewMarque))
                {
                    var nouvelleMarque = new Marque { Nom = viewModel.NewMarque };
                    _marqueService.AjouterMarque(nouvelleMarque);
                    viewModel.MarqueId = nouvelleMarque.Id; // Associer la nouvelle marque
                }

                // Ajout du nouveau modèle si il est fourni
                if (!string.IsNullOrEmpty(viewModel.NewModele))
                {
                    var nouveauModele = new Modele { Nom = viewModel.NewModele };
                    _modeleService.AjouterModele(nouveauModele);
                    viewModel.ModeleId = nouveauModele.Id; // Associer le nouveau modèle
                }

                // Ajout de la nouvelle finition si elle est fournie
                if (!string.IsNullOrEmpty(viewModel.NewFinition))
                {
                    var nouvelleFinition = new Finition { Nom = viewModel.NewFinition };
                    _finitionService.AjouterFinition(nouvelleFinition);
                    viewModel.FinitionId = nouvelleFinition.Id; // Associer la nouvelle finition
                }

                // Créer l'objet Vehicule à partir du ViewModel
                var vehicule = new Vehicule
                {
                    AnneeId = viewModel.AnneeId,
                    MarqueId = viewModel.MarqueId,
                    ModeleId = viewModel.ModeleId,
                    FinitionId = viewModel.FinitionId,
                    Disponible = viewModel.Disponible,
                    DateAchat = viewModel.DateAchat,
                    DateVente = viewModel.DateVente,
                    PrixAchat = viewModel.PrixAchat,
                    Description = viewModel.Description
                };

                // Gestion de la réparation
                if (!string.IsNullOrEmpty(viewModel.ReparationDescription) && viewModel.ReparationCout > 0)
                {
                    var reparation = new Reparation
                    {
                        Description = viewModel.ReparationDescription,
                        Cout = (double)viewModel.ReparationCout
                    };

                    _reparationService.AjouterReparation(reparation);
                    vehicule.ReparationId = reparation.Id;
                }

                // Ajouter le véhicule
                _vehiculeService.AjouterVehicule(vehicule);

                // Gestion des images
                if (viewModel.ImageFiles != null && viewModel.ImageFiles.Count > 0)
                {
                    foreach (var file in viewModel.ImageFiles)
                    {
                        if (file.Length > 0) // Vérifiez si le fichier a une taille valide
                        {
                            var filePath = Path.Combine("wwwroot/images", file.FileName); // Chemin où enregistrer le fichier
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream); // Copiez le fichier dans le chemin
                            }

                            var image = new Image
                            {
                                Url = $"/images/{file.FileName}", // Mettez à jour l'URL pour pointer vers l'emplacement du fichier
                                VehiculeId = vehicule.Id // Associer l'image au véhicule
                            };

                            _imagesService.AjouterImage(image);
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Si ModelState n'est pas valide, recharger les listes déroulantes et retourner à la vue
            viewModel.Annees = _anneeService.GetAllAnnees();
            viewModel.Marques = new SelectList(_marqueService.GetAllMarques(), "Id", "Nom");
            viewModel.Modeles = new SelectList(_modeleService.GetAllModele(), "Id", "Nom");
            viewModel.Finitions = new SelectList(_finitionService.GetAllFinition(), "Id", "Nom");

            return View(viewModel);
        }

        // GET: Vehicule/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var vehicule = await _vehiculeService.GetVehiculeById(id);
            if (vehicule == null)
            {
                return NotFound();
            }

            // Remplir le ViewModel avec les données du véhicule
            var viewModel = new VehiculeViewModel
            {
                Id = vehicule.Id,
                AnneeId = vehicule.AnneeId,
                MarqueId = vehicule.MarqueId,
                ModeleId = vehicule.ModeleId,
                FinitionId = vehicule.FinitionId,
                ReparationId = vehicule.ReparationId,
                ReparationDescription = vehicule.Reparation?.Description,
                ReparationCout = vehicule.Reparation?.Cout,
                Disponible = vehicule.Disponible,
                DateAchat = vehicule.DateAchat,
                DateVente = vehicule.DateVente,
                PrixAchat = vehicule.PrixAchat,
                Description = vehicule.Description,
                ImageUrls = (await _imagesService.GetImagesByVehiculeId(id)).Select(i => i.Url).ToList() // Charge les images existantes
            };

            // Charger les listes déroulantes
            viewModel.Annees = _anneeService.GetAllAnnees();
            viewModel.Marques = new SelectList(_marqueService.GetAllMarques(), "Id", "Nom");
            viewModel.Modeles = new SelectList(_modeleService.GetAllModele(), "Id", "Nom");
            viewModel.Finitions = new SelectList(_finitionService.GetAllFinition(), "Id", "Nom");

            return View(viewModel);
        }

        // POST: Vehicule/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, VehiculeViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Ajout de la nouvelle marque si elle est fournie
                if (!string.IsNullOrEmpty(viewModel.NewMarque))
                {
                    var nouvelleMarque = new Marque { Nom = viewModel.NewMarque };
                    _marqueService.AjouterMarque(nouvelleMarque);
                    viewModel.MarqueId = nouvelleMarque.Id; // Associer la nouvelle marque
                }

                // Ajout du nouveau modèle si il est fourni
                if (!string.IsNullOrEmpty(viewModel.NewModele))
                {
                    var nouveauModele = new Modele { Nom = viewModel.NewModele };
                    _modeleService.AjouterModele(nouveauModele);
                    viewModel.ModeleId = nouveauModele.Id; // Associer le nouveau modèle
                }

                // Ajout de la nouvelle finition si elle est fournie
                if (!string.IsNullOrEmpty(viewModel.NewFinition))
                {
                    var nouvelleFinition = new Finition { Nom = viewModel.NewFinition };
                    _finitionService.AjouterFinition(nouvelleFinition);
                    viewModel.FinitionId = nouvelleFinition.Id; // Associer la nouvelle finition
                }

                // Mettre à jour l'objet Vehicule à partir du ViewModel
                var vehicule = await _vehiculeService.GetVehiculeById(id);
                if (vehicule == null)
                {
                    return NotFound();
                }

                vehicule.AnneeId = viewModel.AnneeId;
                vehicule.MarqueId = viewModel.MarqueId;
                vehicule.ModeleId = viewModel.ModeleId;
                vehicule.FinitionId = viewModel.FinitionId;
                vehicule.Disponible = viewModel.Disponible;
                vehicule.DateAchat = viewModel.DateAchat;
                vehicule.DateVente = viewModel.DateVente;
                vehicule.PrixAchat = viewModel.PrixAchat;
                vehicule.Description = viewModel.Description;

                // Gestion de la réparation
                if (!string.IsNullOrEmpty(viewModel.ReparationDescription) && viewModel.ReparationCout > 0)
                {
                    var reparation = new Reparation
                    {
                        Description = viewModel.ReparationDescription,
                        Cout = (double)viewModel.ReparationCout
                    };

                    _reparationService.AjouterReparation(reparation);
                    vehicule.ReparationId = reparation.Id;
                }

                // Mettre à jour le véhicule
                _vehiculeService.ModifierVehicule(vehicule);

                // Gestion des images
                if (viewModel.ImageFiles != null && viewModel.ImageFiles.Count > 0)
                {
                    // Supprimer les images existantes (si nécessaire)
                    var existingImages = await _imagesService.GetImagesByVehiculeId(id);
                    foreach (var image in existingImages)
                    {
                        _imagesService.SupprimerImage(image.Id);
                    }

                    foreach (var file in viewModel.ImageFiles)
                    {
                        if (file.Length > 0)
                        {
                            var filePath = Path.Combine("wwwroot/images", file.FileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var image = new Image
                            {
                                Url = $"/images/{file.FileName}",
                                VehiculeId = vehicule.Id
                            };

                            _imagesService.AjouterImage(image);
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            // Si ModelState n'est pas valide, recharger les listes déroulantes et retourner à la vue
            viewModel.Annees = _anneeService.GetAllAnnees();
            viewModel.Marques = new SelectList(_marqueService.GetAllMarques(), "Id", "Nom");
            viewModel.Modeles = new SelectList(_modeleService.GetAllModele(), "Id", "Nom");
            viewModel.Finitions = new SelectList(_finitionService.GetAllFinition(), "Id", "Nom");

            return View(viewModel);
        }


        // GET: Vehicule/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _vehiculeService.SupprimerVehicule(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
