using dotnet_museum.Models;

namespace dotnet_museum.Repositories;

public interface IArtifactRepository
{
    List<Artifact> GetArtifacts();
    Artifact? GetById(int id);
    void CreateArtifact(Artifact artifact);
    List<Artifact> GetArtifactsByGallery(int galleryId);
    void UpdateArtifact(Artifact artifact);
}