using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public interface IImagesService
    {
        void AjouterImage(Image image);
        void SupprimerImage(int id);
    }
}
