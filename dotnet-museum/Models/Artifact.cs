namespace dotnet_museum.Models;

public class Artifact
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Origin { get; set; }
    public required string Period { get; set; }
    public required string Civilization { get; set; }
    public required string GalleryId {get;set;}
    
    
}