namespace dotnet_museum.Models.MuseumEvents;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string? IconPath { get; set; }

    public ICollection<EventModel> Events { get; set; } = [];
}