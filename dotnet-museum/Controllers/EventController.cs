using dotnet_museum.Data;
using dotnet_museum.Models.MuseumEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Controllers;

public class EventController : Controller
{
    private readonly AppDbContext  _context;

    public EventController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
        ViewBag.Galleries = new SelectList(_context.Galleries, "GalleryId", "Name");
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EventModel model)
    {
        if (ModelState.IsValid)
        {
            _context.Events.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
    
    public IActionResult Index()
    {
        var events = _context.Events.ToList();
        return View(events);
    }

    public IActionResult Details(int id)
    {
        var ev = _context.Events
            .Include(e => e.Category)
            .Include(e => e.Gallery)
            .FirstOrDefault(e => e.EventId == id);
        
        if (ev != null)
        {
            return View(ev);
        }
        return NotFound();
    }
}