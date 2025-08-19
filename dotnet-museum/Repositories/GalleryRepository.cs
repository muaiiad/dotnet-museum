using dotnet_museum.Data;
using dotnet_museum.Models;

namespace dotnet_museum.Repositories;

public class GalleryRepository : IGalleryRepository
{
    private readonly AppDbContext _context;

    public GalleryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public List<Gallery> GetAll()
    {
        return _context.Galleries.ToList();
    }

    public Gallery? GetById(int id)
    {
        return _context.Galleries.FirstOrDefault(a => a.GalleryId == id);
    }

    public void CreateGallery(Gallery gallery)
    {
        _context.Galleries.Add(gallery);
        _context.SaveChanges();
    }

    public void UpdateGallery(Gallery gallery)
    {
        _context.Galleries.Update(gallery);
        _context.SaveChanges();
    }
}