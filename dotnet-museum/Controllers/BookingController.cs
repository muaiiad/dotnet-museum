using dotnet_museum.Data;
using dotnet_museum.Models.Booking;
using dotnet_museum.Models.TourismCompany;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Controllers;

[Authorize]
public class BookingController : Controller
{
    private readonly AppDbContext _context;

    public BookingController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult GetAllData()
    {
        return Ok(_context.Bookings.ToList());
    }
    public IActionResult GetById(int id)
    {
        return Ok(_context.Bookings.FirstOrDefault(a => a.BookingId == id));
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
            associatedEvent.Capacity -= booking.NumberOfTickets;
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
        //ModelState.Remove(nameof(booking.Event));
        if (ModelState.IsValid)
        {
            // Fetch existing entity from DB
            var existingBooking = _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.TourismCompany)
                .FirstOrDefault(b => b.BookingId == booking.BookingId);

            if (existingBooking == null) return NotFound();

            // Update scalar properties
            existingBooking.CustomerName = booking.CustomerName;
            existingBooking.CustomerPhone = booking.CustomerPhone;
            existingBooking.NumberOfTickets = booking.NumberOfTickets;
            existingBooking.ReservationType = booking.ReservationType;
            existingBooking.EventId = booking.EventId;

            // Update navigation properties safely
            existingBooking.Event = _context.Events.FirstOrDefault(e => e.EventId == booking.EventId);
            
            

            if (booking.ReservationType == ReservationType.TourismCompany && booking.TourismCompanyId.HasValue)
            {
                existingBooking.TourismCompany = _context.Companies.FirstOrDefault(c => c.CompanyId == booking.TourismCompanyId);
                existingBooking.TourismCompanyId = booking.TourismCompanyId;
            }
            else
            {
                existingBooking.TourismCompany = null;
                existingBooking.TourismCompanyId = null;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(booking);
    }
}