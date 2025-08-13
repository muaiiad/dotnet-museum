using System.Collections.Generic;

namespace dotnet_museum.Models;

public class Gallery
{
    public int GalleryId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public List<Artifact> Artifacts { get; set; } = new();
}