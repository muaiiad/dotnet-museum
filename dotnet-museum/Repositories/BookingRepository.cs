using dotnet_museum.Data;
using dotnet_museum.Models.Booking;
using dotnet_museum.Models.MuseumEvents;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public List<BookingModel> GetAllBooking()
    {
        return _context.Bookings.ToList();
    }

    public BookingModel? GetById(int id)
    {
        return _context.Bookings.FirstOrDefault(a => a.BookingId == id);
    }

    public void CreateBooking(BookingModel booking)
    {
        _context.Bookings.Add(booking);
        _context.SaveChanges();
    }

    public IEnumerable<BookingModel>? GetByName(string name)
    {
        var bookings = _context.Bookings
            .Include(b => b.Event)
            .Include(b => b.TourismCompany)
            .Where(b => b.CustomerName == name);

        return bookings;
    }

    public BookingModel? FetchBooking(BookingModel  booking)
    {
        var existingBooking = _context.Bookings
            .Include(b => b.Event)
            .Include(b => b.TourismCompany)
            .FirstOrDefault(b => b.BookingId == booking.BookingId);
        
        return existingBooking;
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}