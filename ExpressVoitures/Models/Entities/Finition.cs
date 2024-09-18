﻿using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models.Entities
{
    public class Finition
    {
        [Key]
        public int Id { get; set; }
        public required string Nom { get; set; }
        public Vehicule Vehicules { get; set; }
    }
}