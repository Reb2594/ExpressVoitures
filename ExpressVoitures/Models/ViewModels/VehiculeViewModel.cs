using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.ViewModels
{
    public class VehiculeViewModel
    {
        internal List<Vehicule> Vehicule;

        public int Id { get; set; }

        // Liste déroulante pour l'année
        public int AnneeId { get; set; }
                
        public IEnumerable<SelectListItem> ? Annees { get; set; }

        // Liste déroulante pour la marque
        public int MarqueId { get; set; }
        public IEnumerable<SelectListItem> ? Marques { get; set; }

        // Liste déroulante pour le modèlede 
        public int ModeleId { get; set; }
        public IEnumerable<SelectListItem> ? Modeles { get; set; }

        // Liste déroulante pour la finition
        public int FinitionId { get; set; }
        public IEnumerable<SelectListItem> ? Finitions { get; set; }
        public string? NewMarque { get; set; }
        public string? NewModele { get; set; }
        public string? NewFinition { get; set; }

        // Réparation
        public int ReparationId { get; set; }
        public string? ReparationDescription { get; set; }
        public double? ReparationCout { get; set; }

        // Autres propriétés du véhicule
        public bool Disponible { get; set; }
        public DateTime? DateVente { get; set; }
        public DateTime DateAchat { get; set; }
        public double PrixAchat { get; set; }
        public string? Description { get; set; }

        // Pour gérer l'ajout d'images
        public List<string>? ImageUrls { get; set; }
        public IFormFileCollection? ImageFiles { get; set; } // Ajout pour le fichier d'image
    }
}
