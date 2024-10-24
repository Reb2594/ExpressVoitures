using ExpressVoitures.Data;
using ExpressVoitures.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Models.Services
{
    public class ImageService : IImagesService
    {
        private readonly ApplicationDbContext _context;

        public ImageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Image>> GetImagesByVehiculeId(int vehiculeId)
        {
            return await _context.Images
                .Where(image => image.VehiculeId == vehiculeId)
                .ToListAsync();
        }

        public void AjouterImage(Image image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
        }
        public void SupprimerImage(int id)
        {
            var image = _context.Images.FirstOrDefault(x => x.Id == id);
            if (image != null)
            {
                _context.Images.Remove(image);
                _context.SaveChanges();
            }

        }
    }
}
