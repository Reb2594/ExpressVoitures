using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public interface IReparationService
    {
        Reparation GetReparationByVehiculeId(int vehiculeId);
        void AjouterReparation(Reparation reparation);
        void ModifierReparation(Reparation reparation);
        void SupprimerReparation(int id);
    }
}
