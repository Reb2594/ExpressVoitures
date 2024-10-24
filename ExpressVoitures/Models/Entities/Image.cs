using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public string? Description { get; set; }
        public int VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }
    }
}
