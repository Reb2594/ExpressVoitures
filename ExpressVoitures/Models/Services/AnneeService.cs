using ExpressVoitures.Data;
using ExpressVoitures.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpressVoitures.Models.Services
{
    public class AnneeService : IAnneeService
    {
        private readonly ApplicationDbContext _context;

        public AnneeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Annee GetAnneeById(int id)
        {
            return _context.Annees.FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<SelectListItem> GetAllAnnees()
        {
            return _context.Annees
                .OrderBy(a => a.Id)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Valeur.ToString()
                })
                .ToList();
        }
    }

}
