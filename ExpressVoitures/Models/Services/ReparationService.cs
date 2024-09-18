using ExpressVoitures.Data;
using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public class ReparationService : IReparationService
    {
        private readonly ApplicationDbContext _context;

        public ReparationService(ApplicationDbContext context) 
        {
            _context = context; 
        }

        public void AjouterReparation(Reparation reparation)
        {
            _context.Reparations.Add(reparation);
            _context.SaveChanges();
        }
        public void ModifierReparation(Reparation reparation)
        {
            _context.Reparations.Update(reparation);
            _context.SaveChanges();
        }
        public void SupprimerReparation(int id)
        {
            var reparation = _context.Reparations.FirstOrDefault(x => x.Id == id);
            if (reparation != null)
            {
                _context.Reparations.Remove(reparation);
                _context.SaveChanges();
            }
        }
    }
}
