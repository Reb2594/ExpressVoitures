using ExpressVoitures.Data;
using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public class ImageService : IImagesService
    {
        private readonly ApplicationDbContext _context;

        public ImageService(ApplicationDbContext context)
        {
            _context = context;
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
