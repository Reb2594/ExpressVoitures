using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models.Entities
{
    public class Vehicule
    {
        [Key]
        public int Id { get; set; }
        public int AnneeId { get; set; }
        public required Annee Annee { get; set; }
        public int MarqueId { get; set; }
        public required Marque Marque { get; set; }
        public int ModeleId { get; set; }
        public required Modele Modele { get; set; }

        //public int ModeleMarqueId { get; set; }
        //public required ModeleMarque ModeleMarque { get; set; }
        public int FinitionId { get; set; }
        public required Finition Finition { get; set; }
        public int ReparationId { get; set; }
        public required Reparation Reparation { get; set; }
        public bool Disponible { get; set; }
        public DateTime? DateVente { get; set; }
        public DateTime DateAchat { get; set; }
        public double PrixAchat { get; set; }
        public string? Description { get; set; }
        public List<Image>? ListImage { get; set; }
        
    }
}
