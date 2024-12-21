using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models.Entities
{
    public class Vehicule
    {
        [Key]
        public int Id { get; set; }
        public int AnneeId { get; set; }
        public Annee Annee { get; set; }
        public int MarqueId { get; set; }
        public Marque Marque { get; set; }
        public int ModeleId { get; set; }
        public Modele Modele { get; set; }
        public int FinitionId { get; set; }
        public Finition Finition { get; set; }
        public int ReparationId { get; set; }
        public Reparation Reparation { get; set; }
        public bool Disponible { get; set; }
        public DateTime? DateVente { get; set; }
        public DateTime DateAchat { get; set; }
        public double PrixAchat { get; set; }
        public double PrixVente { get; set; }
        public string? Description { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();

        // Propriété calculée pour retourner les URLs des images
        public List<string> ImageUrls => Images.Select(img => img.Url).ToList();
    }
}
