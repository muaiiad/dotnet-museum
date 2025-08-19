using dotnet_museum.Data;
using dotnet_museum.Models.MuseumEvents;
using Microsoft.AspNetCore.Authorization;
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
    
    public IActionResult GetAllData()
    {
        return Ok(_context.Events.ToList());
    }
    public IActionResult GetById(int id)
    {
        return Ok(_context.Events.FirstOrDefault(a => a.EventId == id));
    }

    [Authorize]
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
        ViewBag.Galleries = new SelectList(_context.Galleries, "GalleryId", "Name");
        
        return View();
    }

    [Authorize]
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

    [Authorize]
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var ev = _context.Events.FirstOrDefault(e => e.EventId == id);
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
        ViewBag.Galleries = new SelectList(_context.Galleries, "GalleryId", "Name");

        if (ev == null)
        {
            return NotFound();
        }
        return View(ev);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EventModel model)
    {
        if (ModelState.IsValid)
        {
            _context.Attach(model);
            _context.Entry(model).State = EntityState.Modified;
            model.Category = _context.Categories.FirstOrDefault(c => c.CategoryId == model.CategoryId);
            model.Gallery = _context.Galleries.FirstOrDefault(g => g.GalleryId == model.GalleryId);
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}