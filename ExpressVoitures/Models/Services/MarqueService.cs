using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Data;
using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public class MarqueService : IMarqueService
    {
        private readonly ApplicationDbContext _context;

        public MarqueService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Marque> GetAllMarques()
        {
            return _context.Marques.ToList();
        }

        public Marque GetMarqueById(int id)
        {
            return _context.Marques.FirstOrDefault(m => m.Id == id);
        }
        public void AjouterMarque(Marque marque)
        {
            _context.Marques.Add(marque);
            _context.SaveChanges();
        }
        public void ModifierMarque(Marque marque)
        {
            _context.Marques.Update(marque);
            _context.SaveChanges();
        }
        public void SupprimerMarque(int id)
        {
            var marque = _context.Marques.FirstOrDefault(x => x.Id == id);
            if (marque != null)
            {
                _context.Marques.Remove(marque);
                _context.SaveChanges();
            }
        }
    }

}
