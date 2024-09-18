using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Data;
using ExpressVoitures.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpressVoitures.Models.Services
{
    public class FinitionService : IFinitionService
    {
        private readonly ApplicationDbContext _context;

        public FinitionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AjouterFinition(Finition finition)
        {
            _context.Finitions.Add(finition);
            _context.SaveChanges();
        }
        public void ModifierFinition(Finition finition)
        {
            _context.Finitions.Update(finition);
            _context.SaveChanges();
        }
        public void SupprimerFinition(int id)
        {
            var finition = _context.Finitions.FirstOrDefault(x => x.Id == id);
            if (finition != null)
            {
                _context.Finitions.Remove(finition);
                _context.SaveChanges();
            }
        }

    }
}
