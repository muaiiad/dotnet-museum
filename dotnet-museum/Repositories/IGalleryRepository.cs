using dotnet_museum.Models;

namespace dotnet_museum.Repositories;

public interface IGalleryRepository
{
    List<Gallery> GetAll();
    Gallery? GetById(int id);
    void CreateGallery(Gallery gallery);
    void UpdateGallery(Gallery gallery);
    void DeleteGallery(Gallery gallery);
}