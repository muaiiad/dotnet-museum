using dotnet_museum.Data;
using dotnet_museum.Models.MuseumEvents;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Repositories;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;

    public EventRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public List<EventModel> GetAllEvents()
    {
        return _context.Events.ToList();
    }

    public EventModel? GetEventById(int id)
    {
       return _context.Events.FirstOrDefault(a => a.EventId == id);
    }

    public void CreateEvent(EventModel eventModel)
    {
       _context.Events.Add(eventModel);
       _context.SaveChanges();
    }

    public EventModel? GetDetails(int id)
    {
        var eventDetails = _context.Events
            .Include(e => e.Category)
            .Include(e => e.Gallery)
            .FirstOrDefault(e => e.EventId == id);
        return eventDetails;
    }

    public void UpdateEvent(EventModel eventModel)
    {
        _context.Attach(eventModel);
        _context.Entry(eventModel).State = EntityState.Modified;
        
        eventModel.Category =  _context.Categories.FirstOrDefault(c => c.CategoryId == eventModel.CategoryId);
        eventModel.Gallery =  _context.Galleries.FirstOrDefault(g => g.GalleryId == eventModel.GalleryId);
        
        _context.SaveChanges();
    }
}