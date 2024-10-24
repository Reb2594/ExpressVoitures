using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public interface IImagesService
    {
        Task<List<Image>> GetImagesByVehiculeId(int vehiculeId);
        void AjouterImage(Image image);
        void SupprimerImage(int id);
    }
}
