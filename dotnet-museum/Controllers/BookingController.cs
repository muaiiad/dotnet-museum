using dotnet_museum.Data;
using dotnet_museum.Models.Booking;
using dotnet_museum.Models.TourismCompany;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Controllers;

public class BookingController : Controller
{
    private readonly AppDbContext _context;

    public BookingController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Book()
    {
        ViewBag.AllEvents = new SelectList(_context.Events, "EventId", "Title");
        ViewBag.Companies = new SelectList(_context.Companies, "CompanyId", "Name");
        return View(new BookingModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Book(BookingModel booking)
    {
        var associatedEvent = _context.Events.FirstOrDefault(e => e.EventId == booking.EventId);
        if (associatedEvent != null)
        {
            booking.Event = associatedEvent;
            booking.TotalPrice = booking.NumberOfTickets * booking.Event.TicketPrice;
        }
        if (booking.ReservationType == ReservationType.TourismCompany)
        {
            booking.TourismCompany = _context.Companies.FirstOrDefault(c => c.CompanyId == booking.TourismCompanyId);
            if (booking.TourismCompany != null)
            {
                booking.TotalPrice *= (1 - (booking.TourismCompany.DiscountPercentage / 100));
            }
        }
        else
        {
            booking.TourismCompany = null;
            booking.TourismCompanyId = null;
        }
        ModelState.Remove(nameof(booking.Event));
        if (ModelState.IsValid)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return RedirectToAction("Index", new {name = booking.CustomerName});
        }
        // ViewBag.AllEvents = new SelectList(_context.Events, "EventId", "Title");
        // ViewBag.Companies = new SelectList(_context.Companies, "CompanyId", "Name");
        return View(new BookingModel());
    }

    
    public IActionResult Index(string name)
    {
        var bookings = _context.Bookings
            .Include(b => b.Event)
            .Include(b => b.TourismCompany)
            .Where(b => b.CustomerName == name);
        if (bookings.Any())
        {
            return View(bookings.ToList());
        }
        return Content("No bookings found.");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == id);
        ViewBag.AllEvents = new SelectList(_context.Events, "EventId", "Title");
        ViewBag.Companies = new SelectList(_context.Companies, "CompanyId", "Name");

        if (booking == null)
        {
            return NotFound();
        }
        return View(booking);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(BookingModel booking)
    {
        if (ModelState.IsValid)
        {
            _context.Attach(booking);
            _context.Entry(booking).State = EntityState.Modified;
            booking.Event = _context.Events.FirstOrDefault(e => e.EventId == booking.EventId);

            if (booking.ReservationType == ReservationType.TourismCompany)
            {
                booking.TourismCompany =  _context.Companies.FirstOrDefault(c => c.CompanyId == booking.TourismCompanyId);
            }
            else
            {
                booking.TourismCompany = null;
                booking.TourismCompanyId = null;
            }
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(booking);
    }
}