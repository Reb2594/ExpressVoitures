using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models.Entities
{
    public class Annee
    {
        [Key]
        public int Id { get; set; }
        public int Valeur { get; set; }
        public List<Vehicule>? Vehicules { get; set; }
    }
}
