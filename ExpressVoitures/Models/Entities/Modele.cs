using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models.Entities
{
    public class Modele
    {
        [Key]
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required Vehicule Vehicule { get; set; }

        //public List<ModeleMarque>? ModeleMarques { get; set; }
    }
}
