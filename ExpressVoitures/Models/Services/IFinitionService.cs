using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public interface IFinitionService
    {
        void AjouterFinition(Finition finition) ;
        void ModifierFinition(Finition finition);
        void SupprimerFinition(int id);
    }
}
