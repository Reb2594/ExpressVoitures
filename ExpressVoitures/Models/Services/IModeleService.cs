using ExpressVoitures.Data;
using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public interface IModeleService
    {
        List<Modele> GetAllModele();
        void AjouterModele(Modele modele);
        void ModifierModele(Modele modele);
        void SupprimerModele(int id);
    }
}
