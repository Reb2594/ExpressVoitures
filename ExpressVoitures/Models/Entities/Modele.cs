using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpressVoitures.Models.Entities
{
    public class Modele
    {
        [Key]
        public int Id { get; set; }

        public string Nom { get; set; }

        public int MarqueId { get; set; }

        [ForeignKey("MarqueId")]
        public Marque Marque { get; set; }
        public List<Vehicule> Vehicules { get; set; }
    }
}
