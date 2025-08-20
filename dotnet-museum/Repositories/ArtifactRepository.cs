using dotnet_museum.Data;
using dotnet_museum.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Repositories;

public class ArtifactRepository : IArtifactRepository
{
    private readonly AppDbContext _context;

    public ArtifactRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public List<Artifact> GetArtifacts()
    {
        return _context.Artifacts.ToList();
    }

    public Artifact? GetById(int id)
    {
        return _context.Artifacts.FirstOrDefault(a => a.Id == id);
    }

    public void CreateArtifact(Artifact artifact)
    {
        _context.Artifacts.Add(artifact);
        _context.SaveChanges();
    }

    public List<Artifact> GetArtifactsByGallery(int galleryId)
    {
        var artifacts = _context.Artifacts
            .Where(e => e.GalleryId == galleryId)
            .ToList();

        return artifacts;
    }

    public void UpdateArtifact(int id, Artifact artifact)
    {
        var existingArtifact = _context.Artifacts.FirstOrDefault(a => a.Id == id);
        if (existingArtifact == null) return;
        
        existingArtifact.Name = artifact.Name;
        existingArtifact.Description = artifact.Description;
        existingArtifact.Civilization = artifact.Civilization;
        existingArtifact.Origin = artifact.Origin;
        existingArtifact.Period = artifact.Period;
        existingArtifact.GalleryId = artifact.GalleryId;
        existingArtifact.Gallery = artifact.Gallery;
        

        _context.SaveChanges();
    }
    public void UpdateArtifact(Artifact artifact)
    {
        _context.Attach(artifact);
        _context.Entry(artifact).State = EntityState.Modified;
        artifact.Gallery = _context.Galleries.FirstOrDefault(g => g.GalleryId == artifact.GalleryId);

        _context.SaveChanges();
    }
    public void DeleteArtifact(int id)
    {
        var artifact = _context.Artifacts.FirstOrDefault(a => a.Id == id);
        if (artifact == null)
        {
            return;
        }
        _context.Artifacts.Remove(artifact);
        _context.SaveChanges();
    }
}