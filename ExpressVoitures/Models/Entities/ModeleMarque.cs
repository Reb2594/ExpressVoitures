using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models.Entities
{
    public class ModeleMarque
    {
        [Key]
        public int Id { get; set; }
        public int MarqueId { get; set; }
        public required Marque Marque { get; set; }
        public int ModeleId { get; set; }
        public required Modele Modele { get; set; }

        public required List<Vehicule> Vehicules { get; set; }
        public string NomComplet => $"{Marque.Nom} - {Modele.Nom}";
    }
}
