using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Data;
using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public class ModeleMarqueService : IModeleMarqueService
    {
        private readonly ApplicationDbContext _context;

        public ModeleMarqueService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ModeleMarque> GetAllModeleMarques()
        {
            return _context.ModeleMarques
                .Include(mm => mm.Marque)
                .Include(mm => mm.Modele)
                .ToList();
        }
    }

}
