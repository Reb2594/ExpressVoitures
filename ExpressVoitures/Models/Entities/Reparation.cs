using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models.Entities
{
    public class Reparation
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public double Cout { get; set; }
        public int VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }
    }
}
