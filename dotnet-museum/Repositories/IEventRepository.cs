using dotnet_museum.Data;
using dotnet_museum.Models.MuseumEvents;

namespace dotnet_museum.Repositories;

public interface IEventRepository
{
    List<EventModel> GetAllEvents();
    EventModel? GetEventById(int id);
    void CreateEvent(EventModel eventModel);
    EventModel? GetDetails(int id);
    void UpdateEvent(EventModel eventModel);
}