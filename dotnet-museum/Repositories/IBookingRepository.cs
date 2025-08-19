using dotnet_museum.Models.Booking;
using dotnet_museum.Models.MuseumEvents;

namespace dotnet_museum.Repositories;

public interface IBookingRepository
{
    List<BookingModel> GetAllBooking();
    BookingModel? GetById(int id);
    void CreateBooking(BookingModel booking);
    IEnumerable<BookingModel>? GetByName(string name);
    BookingModel? FetchBooking(BookingModel booking);
    void Save();
}