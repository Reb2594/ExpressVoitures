using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models.Entities
{
    public class Reparation
    {
        [Key]
        public int Id { get; set; }
        public required string Description { get; set; }
        public double Cout { get; set; }
        public int VehiculeId { get; set; }
        public required Vehicule Vehicule { get; set; }
    }
}
