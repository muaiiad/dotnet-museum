using System.Collections.Generic;
using dotnet_museum.Models.MuseumEvents;

namespace dotnet_museum.Models;

public class Gallery
{
    public int GalleryId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public List<Artifact> Artifacts { get; set; } = new();
    
    public List<EventModel> Events { get; set; } = new();
}