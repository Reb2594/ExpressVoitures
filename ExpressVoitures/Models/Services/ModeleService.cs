using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Data;
using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public class ModeleService : IModeleService
    {
        private readonly ApplicationDbContext _context;

        public ModeleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Modele> GetAllModele()
        {
            return _context.Modeles.ToList();
        }

        public Modele GetModeleById(int id)
        {
            return _context.Modeles.FirstOrDefault(m => m.Id == id);
        }

        public void AjouterModele(Modele modele)
        {
            _context.Modeles.Add(modele);
            _context.SaveChanges();
        }
        public void ModifierModele(Modele modele)
        {
            _context.Modeles.Update(modele);
            _context.SaveChanges();
        }
        public void SupprimerModele(int id)
        {
            var modele = _context.Modeles.FirstOrDefault(x => x.Id == id);
            if (modele != null)
            {
                _context.Modeles.Remove(modele);
                _context.SaveChanges();
            }
        }
    }
}
