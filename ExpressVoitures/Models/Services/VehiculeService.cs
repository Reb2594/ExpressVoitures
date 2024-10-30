using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Data;
using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public class VehiculeService : IVehiculeService
    {
        private readonly ApplicationDbContext _context;

        public VehiculeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicule> GetVehiculeById(int id)
        {
            return await _context.Vehicules
                .Include(v => v.Annee)
                .Include(v => v.Marque)
                .Include(v => v.Modele)
                .Include(v => v.Finition)
                .Include(v => v.ListImage)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public List<Vehicule> GetAllVehicules()
        {
            return _context.Vehicules
                .Include(v => v.Annee)
                .Include(v => v.Marque)
                .Include(v => v.Modele)
                .Include(v => v.Finition) 
                .Include(v => v.ListImage)
                .ToList() ?? new List<Vehicule>();
        }

        public void AjouterVehicule(Vehicule vehicule)
        {
            _context.Vehicules.Add(vehicule);
            _context.SaveChanges();
        }

        public void ModifierVehicule(Vehicule vehicule)
        {
            _context.Vehicules.Update(vehicule);
            _context.SaveChanges();
        }

        public void SupprimerVehicule(int id)
        {
            var vehicule = _context.Vehicules.Find(id);
            if (vehicule != null)
            {
                _context.Vehicules.Remove(vehicule);
                _context.SaveChanges();
            }
        }
        public bool VehiculeExists(int id)
        {
            return _context.Vehicules.Any(v => v.Id == id);
        }
    }

}
