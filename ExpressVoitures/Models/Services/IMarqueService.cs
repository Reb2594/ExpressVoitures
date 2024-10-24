using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public interface IMarqueService
    {
        List<Marque> GetAllMarques();
        void AjouterMarque(Marque marque);
        void ModifierMarque(Marque marque);
        void SupprimerMarque(int id);
    }
}
