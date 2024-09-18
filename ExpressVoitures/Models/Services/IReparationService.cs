using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public interface IReparationService
    {
        void AjouterReparation(Reparation reparation);
        void ModifierReparation(Reparation reparation);
        void SupprimerReparation(int id);
    }
}
