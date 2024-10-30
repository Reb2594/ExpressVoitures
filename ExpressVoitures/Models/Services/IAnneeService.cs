using ExpressVoitures.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpressVoitures.Models.Services
{
    public interface IAnneeService
    {
        IEnumerable<SelectListItem> GetAllAnnees();
    }
}
