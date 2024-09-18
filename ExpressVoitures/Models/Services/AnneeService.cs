using ExpressVoitures.Data;
using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public class AnneeService : IAnneeService
    {
        private readonly ApplicationDbContext _context;

        public AnneeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Annee> GetAllAnnees()
        {
            return _context.Annees.ToList();
        }
    }

}
