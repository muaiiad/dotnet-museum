using dotnet_museum.Data;
using dotnet_museum.Models.Booking;
using dotnet_museum.Models.TourismCompany;
using dotnet_museum.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Controllers;

[Authorize]
public class BookingController : Controller
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IEventRepository _eventRepository;
    private readonly ICompanyRepository _companyRepository;

    public BookingController(IBookingRepository bookingRepository, IEventRepository eventRepository, ICompanyRepository companyRepository)
    {
        _bookingRepository = bookingRepository;
        _eventRepository = eventRepository;
        _companyRepository = companyRepository;
    }
    public IActionResult GetAllData()
    {
        return Ok(_bookingRepository.GetAllBooking());
    }
    public IActionResult GetById(int id)
    {
        return Ok(_bookingRepository.GetById(id));
    }

    [HttpGet]
    public IActionResult Book()
    {
        ViewBag.AllEvents = new SelectList(_eventRepository.GetAllEvents(), "EventId", "Title");
        ViewBag.Companies = new SelectList(_companyRepository.GetCompanies(), "CompanyId", "Name");
        return View(new BookingModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Book(BookingModel booking)
    {
        var associatedEvent = _eventRepository.GetEventById(booking.EventId);
        if (associatedEvent != null)
        {
            booking.Event = associatedEvent;
            booking.TotalPrice = booking.NumberOfTickets * booking.Event.TicketPrice;
            associatedEvent.Capacity -= booking.NumberOfTickets;
        }
        if (booking.ReservationType == ReservationType.TourismCompany)
        {
            booking.TourismCompany = _companyRepository.GetCompanyById(booking.TourismCompanyId.Value);
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
            _bookingRepository.CreateBooking(booking);
            return RedirectToAction("Index", new {name = booking.CustomerName});
        }
        // ViewBag.AllEvents = new SelectList(_context.Events, "EventId", "Title");
        // ViewBag.Companies = new SelectList(_context.Companies, "CompanyId", "Name");
        return View(new BookingModel());
    }

    
    public IActionResult Index(string name)
    {
        var bookings = _bookingRepository.GetByName(name);
        if (bookings.Any())
        {
            return View(bookings.ToList());
        }
        return Content("No bookings found.");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var booking = _bookingRepository.GetById(id);
        ViewBag.AllEvents = new SelectList(_eventRepository.GetAllEvents(), "EventId", "Title");
        ViewBag.Companies = new SelectList(_companyRepository.GetCompanies(), "CompanyId", "Name");

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
            var existingBooking = _bookingRepository.FetchBooking(booking);

            if (existingBooking == null) return NotFound();

            // Update scalar properties
            existingBooking.CustomerName = booking.CustomerName;
            existingBooking.CustomerPhone = booking.CustomerPhone;
            existingBooking.NumberOfTickets = booking.NumberOfTickets;
            existingBooking.ReservationType = booking.ReservationType;
            existingBooking.EventId = booking.EventId;

            // Update navigation properties safely
            existingBooking.Event = _eventRepository.GetEventById(booking.EventId);
            
            

            if (booking.ReservationType == ReservationType.TourismCompany && booking.TourismCompanyId.HasValue)
            {
                existingBooking.TourismCompany = _companyRepository.GetCompanyById(booking.TourismCompanyId.Value);
                existingBooking.TourismCompanyId = booking.TourismCompanyId;
            }
            else
            {
                existingBooking.TourismCompany = null;
                existingBooking.TourismCompanyId = null;
            }
            
            _bookingRepository.Save();
            return RedirectToAction(nameof(Index));
        }
        return View(booking);
    }
}