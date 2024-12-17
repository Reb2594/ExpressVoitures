using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public interface IImagesService
    {
        Task<List<string>> SauvegarderImagesAsync(List<IFormFile> fichiers);
        List<string> GetImagesUrlsByVehiculeId(int vehiculeId);
        Task AjouterImage(int vehiculeId, string imageUrl);
        void SupprimerImage(int vehiculeId, string imageUrl);
    }
}
