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
using System.Security.Policy;

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

            var vehicule = _vehiculeService.GetVehiculeById(id.Value);
            if (vehicule == null)
            {
                return NotFound();
            }

            var viewModel = new VehiculeViewModel
            {
                Id = vehicule.Id,
                AnneeId = vehicule.AnneeId,
                MarqueId = vehicule.MarqueId,
                ModeleId = vehicule.ModeleId,
                FinitionId = vehicule.FinitionId,
                ReparationId = vehicule.ReparationId,
                Disponible = vehicule.Disponible,
                DateVente = vehicule.DateVente,
                DateAchat = vehicule.DateAchat,
                PrixAchat = vehicule.PrixAchat,
                PrixVente = vehicule.PrixVente,
                Description = vehicule.Description,

                // Charger les URLs des images
                ImageUrls = vehicule.Images.Select(img => img.Url).ToList()
            };

            return View(vehicule);
        }

        // GET: Vehicule/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var viewModel = new VehiculeViewModel
            {
                DateAchat = DateTime.Today,
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
        // POST: Vehicule/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(VehiculeViewModel viewModel, string action)
        {
            // Ajout d'une nouvelle marque
            if (action == "AddMarque")
            {
                ModelState.Clear();

                if (!string.IsNullOrWhiteSpace(viewModel.NewMarque))
                {
                    var nouvelleMarque = new Marque { Nom = viewModel.NewMarque };
                    _marqueService.AjouterMarque(nouvelleMarque);
                    TempData["Message"] = "Nouvelle marque ajoutée avec succès.";
                }
                else
                {
                    TempData["Error"] = "Le nom de la marque ne peut pas être vide.";
                }

                // Recharger les données sans effacer les sélections actuelles
                viewModel.Annees = _anneeService.GetAllAnnees();
                viewModel.Marques = new SelectList(_marqueService.GetAllMarques(), "Id", "Nom", viewModel.MarqueId);

                // Charger les modèles en fonction de la marque sélectionnée
                viewModel.Modeles = new SelectList(_modeleService
                    .GetAllModele()
                    .Where(m => m.MarqueId == viewModel.MarqueId), "Id", "Nom", viewModel.ModeleId);

                viewModel.Finitions = new SelectList(_finitionService.GetAllFinition(), "Id", "Nom", viewModel.FinitionId);

                return View(viewModel);
            }

            // Ajout d'un nouveau modèle
            if (action == "AddModele")
            {
                ModelState.Clear();

                if (!string.IsNullOrWhiteSpace(viewModel.NewModele) && viewModel.MarqueId > 0)
                {
                    var marque = _marqueService.GetMarqueById(viewModel.MarqueId.Value);
                    if (marque != null)
                    {
                        var modeleExistant = _modeleService.GetAllModele()
    .FirstOrDefault(m => m.Nom == viewModel.NewModele && m.MarqueId == viewModel.MarqueId);

                        if (modeleExistant == null)
                        {
                            var nouveauModele = new Modele
                            {
                                Nom = viewModel.NewModele,
                                MarqueId = viewModel.MarqueId.Value
                            };
                            _modeleService.AjouterModele(nouveauModele);
                            TempData["Message"] = "Nouveau modèle ajouté avec succès.";
                        }
                        else
                        {
                            TempData["Error"] = "Ce modèle existe déjà pour cette marque.";
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Marque non trouvée.";
                    }
                }

                // Recharger les données sans effacer les sélections actuelles
                viewModel.Annees = _anneeService.GetAllAnnees();
                viewModel.Marques = new SelectList(_marqueService.GetAllMarques(), "Id", "Nom", viewModel.MarqueId);

                // Charger les modèles en fonction de la marque sélectionnée
                viewModel.Modeles = new SelectList(_modeleService
                .GetAllModele()
                .Where(m => m.MarqueId == viewModel.MarqueId), "Id", "Nom", viewModel.ModeleId);



                viewModel.Finitions = new SelectList(_finitionService.GetAllFinition(), "Id", "Nom", viewModel.FinitionId);

                return View(viewModel);
            }


            // Ajouter une finition
            if (action == "AddFinition")
            {
                ModelState.Clear();

                if (!string.IsNullOrWhiteSpace(viewModel.NewFinition))
                {
                    var nouvelleFinition = new Finition { Nom = viewModel.NewFinition };
                    _finitionService.AjouterFinition(nouvelleFinition);
                    TempData["Message"] = "Nouvelle finition ajoutée avec succès.";
                }
                else
                {
                    TempData["Error"] = "Le nom de la finition ne peut pas être vide.";
                }

                // Recharger les données sans effacer les sélections actuelles
                viewModel.Annees = _anneeService.GetAllAnnees();
                viewModel.Marques = new SelectList(_marqueService.GetAllMarques(), "Id", "Nom", viewModel.MarqueId);

                // Recharger les modèles de la marque sélectionnée
                if (viewModel.MarqueId > 0)
                {
                    viewModel.Modeles = new SelectList(_modeleService
                        .GetAllModele()
                        .Where(m => m.MarqueId == viewModel.MarqueId), "Id", "Nom", viewModel.ModeleId);
                }
                else
                {
                    viewModel.Modeles = new SelectList(Enumerable.Empty<SelectListItem>());
                }

                viewModel.Finitions = new SelectList(_finitionService.GetAllFinition(), "Id", "Nom", viewModel.FinitionId);

                
                // Réinitialiser TempData pour recharger les modèles via JavaScript si nécessaire
                //TempData["ReloadModels"] = true;

                return View(viewModel);
            }

            // Enregistrer le véhicule
            if (action == "CreateVehicule")
            {
                if (!viewModel.AnneeId.HasValue)
                {
                    ModelState.AddModelError(nameof(viewModel.AnneeId), "L'année est obligatoire.");
                }

                if (!viewModel.MarqueId.HasValue)
                {
                    ModelState.AddModelError(nameof(viewModel.MarqueId), "La marque est obligatoire.");
                }

                if (!viewModel.ModeleId.HasValue)
                {
                    ModelState.AddModelError(nameof(viewModel.ModeleId), "Le modèle est obligatoire.");
                }

                if (!viewModel.FinitionId.HasValue)
                {
                    ModelState.AddModelError(nameof(viewModel.FinitionId), "La finition est obligatoire.");
                }

                if (!viewModel.DateAchat.HasValue)
                {
                    ModelState.AddModelError(nameof(viewModel.DateAchat), "La date d'achat est obligatoire.");
                }
                else
                {                    
                    if (viewModel.DateAchat.Value.Date >= DateTime.Today)
                    {
                        ModelState.AddModelError(nameof(viewModel.DateAchat), "La date d'achat ne peut pas être dans le futur.");
                    }

                    if (viewModel.DateAchat.Value.Date < new DateTime(1980, 1, 1))
                    {
                        ModelState.AddModelError(nameof(viewModel.DateAchat), "La date d'achat ne peut pas être antérieure au 1er janvier 1980.");
                    }
                }


                if (!viewModel.PrixAchat.HasValue || viewModel.PrixAchat <= 0)
                {
                    ModelState.AddModelError(nameof(viewModel.PrixAchat), "Le prix d'achat doit être supérieur à 0.");
                }

                if (string.IsNullOrWhiteSpace(viewModel.ReparationDescription))
                {
                    ModelState.AddModelError(nameof(viewModel.ReparationDescription), "Veuillez renseigner une description de la réparation.");
                }

                if (!viewModel.ReparationCout.HasValue || viewModel.ReparationCout <=0)
                {
                    ModelState.AddModelError(nameof(viewModel.ReparationCout), "Veuillez renseigner le coût de la réparation.");
                }

                if (!viewModel.PrixVente.HasValue || viewModel.PrixVente <= (viewModel.PrixAchat + viewModel.ReparationCout))
                {
                    ModelState.AddModelError(nameof(viewModel.PrixVente), "Le prix de vente doit être supérieur à la somme du prix d'achat et du montant des coûts de réparation.");
                }

                if (viewModel.DateVente < viewModel.DateAchat)
                {
                    ModelState.AddModelError(nameof(viewModel.DateVente), "La date de vente doit être postérieure à la date d'achat.");
                }

                if (ModelState.IsValid)
                {
                    var vehicule = new Vehicule
                    {
                        AnneeId = viewModel.AnneeId.Value,
                        MarqueId = viewModel.MarqueId.Value,
                        ModeleId = viewModel.ModeleId.Value,
                        FinitionId = viewModel.FinitionId.Value,
                        DateAchat = viewModel.DateAchat.Value,
                        DateVente = viewModel.DateVente,
                        PrixAchat = viewModel.PrixAchat.Value,
                        PrixVente = viewModel.PrixVente.Value,
                        Disponible = viewModel.Disponible,
                        Description = viewModel.Description
                    };

                    
                    _vehiculeService.AjouterVehicule(vehicule);
                    var vehiculeId = vehicule.Id;

                    // Gestion de la réparation
                    if (!string.IsNullOrEmpty(viewModel.ReparationDescription) && viewModel.ReparationCout > 0)
                    {
                        // Créez une nouvelle réparation si aucune n'existe
                        var nouvelleReparation = new Reparation
                        {
                            Description = viewModel.ReparationDescription,
                            Cout = viewModel.ReparationCout.Value,
                            VehiculeId = vehicule.Id
                        };
                        _reparationService.AjouterReparation(nouvelleReparation);
                    }
                    
                    // Enregistrer les images
                    if (viewModel.ImageFiles != null && viewModel.ImageFiles.Count > 0)
                    {
                        var urls = await _imagesService.SauvegarderImagesAsync(viewModel.ImageFiles);
                        foreach (var url in urls)
                        {
                            await _imagesService.AjouterImage(vehiculeId, url);
                        }
                    }                   
                       

                    return View("AddConfirmation");
                    //return RedirectToAction(nameof(Index));
                }

                // Si le modèle n'est pas valide, recharger les données
                viewModel.Annees = _anneeService.GetAllAnnees();
                viewModel.Marques = new SelectList(_marqueService.GetAllMarques(), "Id", "Nom", viewModel.MarqueId);
                // Charger les modèles de la marque sélectionnée
                if (viewModel.MarqueId.HasValue)
                {
                    viewModel.Modeles = new SelectList(_modeleService
                        .GetAllModele()
                        .Where(m => m.MarqueId == viewModel.MarqueId), "Id", "Nom", viewModel.ModeleId);
                }
                else
                {
                    viewModel.Modeles = new SelectList(Enumerable.Empty<SelectListItem>());
                }
                viewModel.Finitions = new SelectList(_finitionService.GetAllFinition(), "Id", "Nom", viewModel.FinitionId);

                TempData["Error"] = "Veuillez vérifier les champs obligatoires.";

                return View(viewModel);
            }

            return BadRequest("Action inconnue.");
        }




        // GET: Vehicule/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var vehicule = _vehiculeService.GetVehiculeById(id);
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
                ReparationDescription = vehicule.Reparation.Description,
                ReparationCout = vehicule.Reparation.Cout,
                Disponible = vehicule.Disponible,
                DateAchat = vehicule.DateAchat,
                DateVente = vehicule.DateVente,
                PrixAchat = vehicule.PrixAchat,
                PrixVente = vehicule.PrixVente,
                Description = vehicule.Description,
                ImageUrls = _imagesService.GetImagesUrlsByVehiculeId(id) // Charge les images existantes
            };

            // Charger les listes déroulantes
            viewModel.Annees = _anneeService.GetAllAnnees();
            viewModel.Marques = new SelectList(_marqueService.GetAllMarques(), "Id", "Nom");
            viewModel.Modeles = new SelectList(_modeleService
                    .GetAllModele()
                    .Where(m => m.MarqueId == viewModel.MarqueId), "Id", "Nom", viewModel.ModeleId);
            viewModel.Finitions = new SelectList(_finitionService.GetAllFinition(), "Id", "Nom");

            return View(viewModel);
        }

        // POST: Vehicule/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, VehiculeViewModel viewModel, string action)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            // Ajout d'une nouvelle marque
            if (action == "AddMarque")
            {
                ModelState.Clear();

                if (!string.IsNullOrWhiteSpace(viewModel.NewMarque))
                {
                    var nouvelleMarque = new Marque { Nom = viewModel.NewMarque };
                    _marqueService.AjouterMarque(nouvelleMarque);
                    TempData["Message"] = "Nouvelle marque ajoutée avec succès.";
                }
                else
                {
                    TempData["Error"] = "Le nom de la marque ne peut pas être vide.";
                }

                // Recharger les données sans effacer les sélections actuelles
                viewModel.Annees = _anneeService.GetAllAnnees();
                viewModel.Marques = new SelectList(_marqueService.GetAllMarques(), "Id", "Nom", viewModel.MarqueId);

                // Charger les modèles en fonction de la marque sélectionnée
                viewModel.Modeles = new SelectList(_modeleService
                    .GetAllModele()
                    .Where(m => m.MarqueId == viewModel.MarqueId), "Id", "Nom", viewModel.ModeleId);

                viewModel.Finitions = new SelectList(_finitionService.GetAllFinition(), "Id", "Nom", viewModel.FinitionId);

                return View(viewModel);
            }

            // Ajout d'un nouveau modèle
            if (action == "AddModele")
            {
                ModelState.Clear();

                if (!string.IsNullOrWhiteSpace(viewModel.NewModele) && viewModel.MarqueId > 0)
                {
                    var marque = _marqueService.GetMarqueById(viewModel.MarqueId.Value);
                    if (marque != null)
                    {
                        var modeleExistant = _modeleService.GetAllModele()
    .FirstOrDefault(m => m.Nom == viewModel.NewModele && m.MarqueId == viewModel.MarqueId);

                        if (modeleExistant == null)
                        {
                            var nouveauModele = new Modele
                            {
                                Nom = viewModel.NewModele,
                                MarqueId = viewModel.MarqueId.Value
                            };
                            _modeleService.AjouterModele(nouveauModele);
                            TempData["Message"] = "Nouveau modèle ajouté avec succès.";
                        }
                        else
                        {
                            TempData["Error"] = "Ce modèle existe déjà pour cette marque.";
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Marque non trouvée.";
                    }
                }

                // Recharger les données sans effacer les sélections actuelles
                viewModel.Annees = _anneeService.GetAllAnnees();
                viewModel.Marques = new SelectList(_marqueService.GetAllMarques(), "Id", "Nom", viewModel.MarqueId);

                // Charger les modèles en fonction de la marque sélectionnée
                viewModel.Modeles = new SelectList(_modeleService
                .GetAllModele()
                .Where(m => m.MarqueId == viewModel.MarqueId), "Id", "Nom", viewModel.ModeleId);



                viewModel.Finitions = new SelectList(_finitionService.GetAllFinition(), "Id", "Nom", viewModel.FinitionId);

                return View(viewModel);
            }


            // Ajouter une finition
            if (action == "AddFinition")
            {
                ModelState.Clear();

                if (!string.IsNullOrWhiteSpace(viewModel.NewFinition))
                {
                    var nouvelleFinition = new Finition { Nom = viewModel.NewFinition };
                    _finitionService.AjouterFinition(nouvelleFinition);
                    TempData["Message"] = "Nouvelle finition ajoutée avec succès.";
                }
                else
                {
                    TempData["Error"] = "Le nom de la finition ne peut pas être vide.";
                }

                // Recharger les données sans effacer les sélections actuelles
                viewModel.Annees = _anneeService.GetAllAnnees();
                viewModel.Marques = new SelectList(_marqueService.GetAllMarques(), "Id", "Nom", viewModel.MarqueId);

                // Recharger les modèles de la marque sélectionnée
                if (viewModel.MarqueId > 0)
                {
                    viewModel.Modeles = new SelectList(_modeleService
                        .GetAllModele()
                        .Where(m => m.MarqueId == viewModel.MarqueId), "Id", "Nom", viewModel.ModeleId);
                }
                else
                {
                    viewModel.Modeles = new SelectList(Enumerable.Empty<SelectListItem>());
                }

                viewModel.Finitions = new SelectList(_finitionService.GetAllFinition(), "Id", "Nom", viewModel.FinitionId);


                // Réinitialiser TempData pour recharger les modèles via JavaScript si nécessaire
                //TempData["ReloadModels"] = true;

                return View(viewModel);
            }

            if (action == "EditVehicule")
            {
                if (!viewModel.AnneeId.HasValue)
                {
                    ModelState.AddModelError(nameof(viewModel.AnneeId), "L'année est obligatoire.");
                }

                if (!viewModel.MarqueId.HasValue)
                {
                    ModelState.AddModelError(nameof(viewModel.MarqueId), "La marque est obligatoire.");
                }

                if (!viewModel.ModeleId.HasValue)
                {
                    ModelState.AddModelError(nameof(viewModel.ModeleId), "Le modèle est obligatoire.");
                }

                if (!viewModel.FinitionId.HasValue)
                {
                    ModelState.AddModelError(nameof(viewModel.FinitionId), "La finition est obligatoire.");
                }

                if (!viewModel.DateAchat.HasValue)
                {
                    ModelState.AddModelError(nameof(viewModel.DateAchat), "La date d'achat est obligatoire.");
                }
                else
                {
                    if (viewModel.DateAchat.Value.Date >= DateTime.Today)
                    {
                        ModelState.AddModelError(nameof(viewModel.DateAchat), "La date d'achat ne peut pas être dans le futur.");
                    }

                    if (viewModel.DateAchat.Value.Date < new DateTime(1980, 1, 1))
                    {
                        ModelState.AddModelError(nameof(viewModel.DateAchat), "La date d'achat ne peut pas être antérieure au 1er janvier 1980.");
                    }
                }

                if (!viewModel.PrixAchat.HasValue || viewModel.PrixAchat <= 0)
                {
                    ModelState.AddModelError(nameof(viewModel.PrixAchat), "Le prix d'achat doit être supérieur à 0.");
                }

                if (string.IsNullOrWhiteSpace(viewModel.ReparationDescription))
                {
                    ModelState.AddModelError(nameof(viewModel.ReparationDescription), "Veuillez renseigner une description de la réparation.");
                }

                if (!viewModel.ReparationCout.HasValue || viewModel.ReparationCout <= 0)
                {
                    ModelState.AddModelError(nameof(viewModel.ReparationCout), "Veuillez renseigner le coût de la réparation.");
                }

                if (!viewModel.PrixVente.HasValue || viewModel.PrixVente <= (viewModel.PrixAchat + viewModel.ReparationCout))
                {
                    ModelState.AddModelError(nameof(viewModel.PrixVente), "Le prix de vente doit être supérieur à la somme du prix d'achat et du montant des coûts de réparation.");
                }

                if (viewModel.DateVente < viewModel.DateAchat)
                {
                    ModelState.AddModelError(nameof(viewModel.DateVente), "La date de vente doit être postérieure à la date d'achat.");
                }

                if (ModelState.IsValid)
                {      
                    // Mettre à jour l'objet Vehicule à partir du ViewModel
                    var vehicule = _vehiculeService.GetVehiculeById(id);
                    if (vehicule == null)
                    {
                        return NotFound();
                    }

                        vehicule.AnneeId = viewModel.AnneeId.Value;
                        vehicule.MarqueId = viewModel.MarqueId.Value;
                        vehicule.ModeleId = viewModel.ModeleId.Value;
                        vehicule.FinitionId = viewModel.FinitionId.Value;
                        vehicule.DateAchat = viewModel.DateAchat.Value;
                        vehicule.PrixAchat = viewModel.PrixAchat.Value;
                        vehicule.PrixVente = viewModel.PrixVente.Value;
                        vehicule.Disponible = viewModel.Disponible;
                        vehicule.Description = viewModel.Description;
                        vehicule.DateVente = viewModel.DateVente;                    

                    // Gestion de la réparation
                    if (!string.IsNullOrEmpty(viewModel.ReparationDescription) && viewModel.ReparationCout > 0)
                    {
                        // Vérifiez si une réparation est déjà associée au véhicule
                        var reparationExistante = _reparationService.GetReparationByVehiculeId(id);

                        if (reparationExistante == null)
                        {
                            // Créez une nouvelle réparation si aucune n'existe
                            var nouvelleReparation = new Reparation
                            {
                                Description = viewModel.ReparationDescription,
                                Cout = (double)viewModel.ReparationCout,
                                VehiculeId = id
                            };
                            _reparationService.AjouterReparation(nouvelleReparation);
                            vehicule.ReparationId = nouvelleReparation.Id;
                        }
                        else
                        {
                            // Mettez à jour la réparation existante
                            reparationExistante.Description = viewModel.ReparationDescription;
                            reparationExistante.Cout = (double)viewModel.ReparationCout;
                            _reparationService.ModifierReparation(reparationExistante);
                        }
                    }

                    // Mettre à jour le véhicule
                    _vehiculeService.ModifierVehicule(vehicule);

                    // Gestion des nouvelles images
                    if (viewModel.ImageFiles != null && viewModel.ImageFiles.Any())
                    {
                        var newImageUrls = await _imagesService.SauvegarderImagesAsync(viewModel.ImageFiles);
                        foreach (var url in newImageUrls)
                        {
                            await _imagesService.AjouterImage(vehicule.Id, url);
                        }
                    }
                    // Mettre à jour les URLs d'images pour les afficher dans la vue
                    viewModel.ImageUrls = _imagesService.GetImagesUrlsByVehiculeId(vehicule.Id);

                    ChargerLibelles(viewModel);

                    return View("EditConfirmation", viewModel);
                }                

                // Si le modèle n'est pas valide, recharger les données
                viewModel.Annees = _anneeService.GetAllAnnees();
                viewModel.Marques = new SelectList(_marqueService.GetAllMarques(), "Id", "Nom", viewModel.MarqueId);
                // Charger les modèles de la marque sélectionnée
                if (viewModel.MarqueId.HasValue)
                {
                    viewModel.Modeles = new SelectList(_modeleService
                        .GetAllModele()
                        .Where(m => m.MarqueId == viewModel.MarqueId), "Id", "Nom", viewModel.ModeleId);
                }
                else
                {
                    viewModel.Modeles = new SelectList(Enumerable.Empty<SelectListItem>());
                }
                viewModel.Finitions = new SelectList(_finitionService.GetAllFinition(), "Id", "Nom", viewModel.FinitionId);

                TempData["Error"] = "Veuillez vérifier les champs obligatoires.";

                return View(viewModel);
            }
        
            return BadRequest("Action inconnue.");
        }

        private void ChargerLibelles(VehiculeViewModel viewModel)
        {
            // Charger le libellé correspondant à l'année
            viewModel.Annees = new List<SelectListItem>
    {
        new SelectListItem
        {
            Value = viewModel.AnneeId.ToString(),
            Text = _anneeService.GetAnneeById(viewModel.AnneeId.Value)?.Valeur.ToString() ?? "Année inconnue"
        }
    };

            // Charger le libellé correspondant à la marque
            viewModel.Marques = new List<SelectListItem>
    {
        new SelectListItem
        {
            Value = viewModel.MarqueId.ToString(),
            Text = _marqueService.GetMarqueById(viewModel.MarqueId.Value)?.Nom ?? "Marque inconnue"
        }
    };

            // Charger le libellé correspondant au modèle
            viewModel.Modeles = new List<SelectListItem>
    {
        new SelectListItem
        {
            Value = viewModel.ModeleId.ToString(),
            Text = _modeleService.GetModeleById(viewModel.ModeleId.Value)?.Nom ?? "Modèle inconnu"
        }
    };

            // Charger le libellé correspondant à la finition
            viewModel.Finitions = new List<SelectListItem>
    {
        new SelectListItem
        {
            Value = viewModel.FinitionId.ToString(),
            Text = _finitionService.GetFinitionById(viewModel.FinitionId.Value)?.Nom ?? "Finition inconnue"
        }
    };
        }


        [HttpGet]
        public JsonResult GetModelesByMarque(int marqueId)
        {            
            var modeles = _modeleService.GetAllModele()
                .Where(m => m.MarqueId == marqueId)
                .Select(m => new { m.Id, m.Nom }) 
                .ToList();

            return Json(modeles);
        }       

        [HttpGet]
        public async Task<IActionResult> DeleteImage(int id, string imageUrl)
        {
           
            if (id == 0 || string.IsNullOrEmpty(imageUrl))
            {
                return BadRequest("Paramètres invalides.");
            }

            var vehicule = _vehiculeService.GetVehiculeById(id);          
                
                
            if (vehicule == null)
            {
                return NotFound("Véhicule introuvable");
            }                                 
        
            _imagesService.SupprimerImage(id, imageUrl);
            
            _vehiculeService.ModifierVehicule(vehicule);                  
            

            return RedirectToAction("Edit", new {id});
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
            // Recharger le véhicule avec ses relations avant de l'utiliser
            var vehicule = _vehiculeService.GetVehiculeById(id);

            if (vehicule == null)
            {
                return NotFound(); // Si le véhicule n'existe pas, retourner une erreur 404
            }

            // Supprimer le véhicule
            _vehiculeService.SupprimerVehicule(id);

            // Passer le modèle chargé à la vue DeleteConfirmation
            return View("DeleteConfirmation", vehicule);
        }

    }
}
