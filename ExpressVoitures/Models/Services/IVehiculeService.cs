using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public interface IVehiculeService
    {
        Task <Vehicule> GetVehiculeById(int id);
        List<Vehicule> GetAllVehicules();
        void AjouterVehicule(Vehicule vehicule);
        void ModifierVehicule(Vehicule vehicule);
        void SupprimerVehicule(int id);
        bool VehiculeExists(int id);
    }

}
