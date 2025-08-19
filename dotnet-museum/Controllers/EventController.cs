using dotnet_museum.Data;
using dotnet_museum.Models.MuseumEvents;
using dotnet_museum.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Controllers;

public class EventController : Controller
{
    private readonly IEventRepository _eventRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IGalleryRepository _galleryRepository;

    public EventController(IEventRepository eventRepository, ICategoryRepository categoryRepository, IGalleryRepository galleryRepository)
    {
        _eventRepository = eventRepository;
        _categoryRepository = categoryRepository;
        _galleryRepository = galleryRepository;
    }
    
    public IActionResult GetAllData()
    {
        return Ok(_eventRepository.GetAllEvents());
    }
    public IActionResult GetById(int id)
    {
        return Ok(_eventRepository.GetEventById(id));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_categoryRepository.GetCategories(), "CategoryId", "Name");
        ViewBag.Galleries = new SelectList(_galleryRepository.GetAll(), "GalleryId", "Name");
        
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EventModel model)
    {
        if (ModelState.IsValid)
        {
            _eventRepository.CreateEvent(model);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
    
    public IActionResult Index()
    {
        var events = _eventRepository.GetAllEvents();
        return View(events);
    }

    public IActionResult Details(int id)
    {
        var ev = _eventRepository.GetDetails(id);
        
        if (ev != null)
        {
            return View(ev);
        }
        return NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var ev = _eventRepository.GetEventById(id);
        ViewBag.Categories = new SelectList(_categoryRepository.GetCategories(), "CategoryId", "Name");
        ViewBag.Galleries = new SelectList(_galleryRepository.GetAll(), "GalleryId", "Name");

        if (ev == null)
        {
            return NotFound();
        }
        return View(ev);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EventModel model)
    {
        if (ModelState.IsValid)
        {
            _eventRepository.UpdateEvent(model);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}