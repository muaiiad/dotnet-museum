using dotnet_museum.Models.Booking;

namespace dotnet_museum.Models.MuseumEvents;

public class EventModel
{
    public int EventId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Capacity { get; set; }
    public decimal TicketPrice { get; set; }
    public string ImagePath { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public ICollection<BookingModel> Bookings { get; set; } = [];
    
    public int GalleryId { get; set; }
    public Gallery Gallery { get; set; }
}