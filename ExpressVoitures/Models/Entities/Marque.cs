using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models.Entities
{
    public class Marque
    {
        [Key]
        public int Id { get; set; }

        public string Nom { get; set; }

        // Propriété de navigation pour les modèles associés
        public List<Modele> Modeles { get; set; }
        public List<Vehicule> Vehicules { get; set; }
    }
}
