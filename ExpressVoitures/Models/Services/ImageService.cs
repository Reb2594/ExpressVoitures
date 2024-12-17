using ExpressVoitures.Data;
using ExpressVoitures.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ExpressVoitures.Models.Services
{
    public class ImageService : IImagesService
    {
        private readonly ApplicationDbContext _context;

        public ImageService(ApplicationDbContext context)
        {
            _context = context;
        }

      
        public async Task<List<string>> SauvegarderImagesAsync(List<IFormFile> fichiers)
        {
            List<string> urls = new();

            foreach (var file in fichiers)
            {
                if (file.Length > 0)
                {
                    string dossierImages = Path.Combine("wwwroot", "images");
                    Directory.CreateDirectory(dossierImages);  // Crée le dossier s'il n'existe pas

                    string cheminFichier = Path.Combine(dossierImages, file.FileName);
                    using (var stream = new FileStream(cheminFichier, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    urls.Add($"images/{file.FileName}");
                }
            }
            return urls;
        }

        public List<string> GetImagesUrlsByVehiculeId(int vehiculeId)
        {
            var vehicule = _context.Vehicules
                .Include(v => v.Images)
                .FirstOrDefault(v => v.Id == vehiculeId);

            return vehicule.ImageUrls;

        }

       
        public async Task AjouterImage(int vehiculeId, string imageUrl)
        {
            var vehicule = _context.Vehicules.FirstOrDefault(v => v.Id == vehiculeId);          

            if (vehicule != null)            
            {
                var image = new Image
                {
                    Url = imageUrl,
                    VehiculeId = vehiculeId
                };
                
                _context.Images.Add(image);
                _context.SaveChanges();
            }
        }

        public void SupprimerImage(int vehiculeId, string imageUrl)
        {
            var vehicule = _context.Vehicules.FirstOrDefault(v => v.Id == vehiculeId);

            if (vehicule != null)
            {
                var image = _context.Images.FirstOrDefault(img => img.VehiculeId == vehiculeId && img.Url == imageUrl);

                if (image != null)
                {
                    _context.Images.Remove(image);

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", Path.GetFileName(image.Url));

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    _context.SaveChanges();
                }

            }
        }
    }
}
