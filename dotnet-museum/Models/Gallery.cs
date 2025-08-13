namespace dotnet_museum.Models;

public class Gallery
{
    public int GalleryId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public IList<Artifact> Artifacts { get; set; }
}