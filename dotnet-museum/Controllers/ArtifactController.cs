using dotnet_museum.Data;
using dotnet_museum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dotnet_museum.Controllers;

public class ArtifactController : Controller
{
    private readonly AppDbContext _db;

    public ArtifactController(AppDbContext db)
    {
        _db = db;
    }
    [HttpGet]
    public IActionResult Create()
    {
        var galleries = _db.Galleries.ToList();

        ViewBag.Galleries = new SelectList(galleries, "GalleryId", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Artifact artifact)
    {
        
        if (ModelState.IsValid)
        {
            _db.Artifacts.Add(artifact);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(artifact);
    }
    public IActionResult Index()
    {
        var artifacts = _db.Artifacts.ToList();
        ViewData["artifactCount"] = artifacts.Count();
        
        return View(artifacts);
    }

    public IActionResult ByGallery(int galleryId)
    {
        var artifacts = _db.Artifacts
            .Where(e => e.GalleryId == galleryId)
            .ToList();
            
        return View("Index", artifacts);
    }
}