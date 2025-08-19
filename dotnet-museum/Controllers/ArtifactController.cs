using dotnet_museum.Data;
using dotnet_museum.Models;
using dotnet_museum.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Controllers;

public class ArtifactController : Controller
{
    private readonly IArtifactRepository _artifactRepository;
    private readonly IGalleryRepository _galleryRepository;

    public ArtifactController(IArtifactRepository artifactRepository, IGalleryRepository galleryRepository)
    {
        _artifactRepository  = artifactRepository;
        _galleryRepository  = galleryRepository;
    }

    public IActionResult GetAllData()
    {
        return Ok(_artifactRepository.GetArtifacts());
    }
    public IActionResult GetById(int id)
    {
        return Ok(_artifactRepository.GetById(id));
    }
    [Authorize]
    [HttpGet]
    public IActionResult Create()
    {
        var galleries = _galleryRepository.GetAll();

        ViewBag.Galleries = new SelectList(galleries, "GalleryId", "Name");
        return View();
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Artifact artifact)
    {
        
        if (ModelState.IsValid)
        {
            _artifactRepository.CreateArtifact(artifact);
            return RedirectToAction(nameof(Index));
        }
        return View(artifact);
    }
    public IActionResult Index()
    {
        var artifacts = _artifactRepository.GetArtifacts();
        ViewData["artifactCount"] = artifacts.Count();
        
        return View(artifacts);
    }

    public IActionResult ByGallery(int galleryId)
    {
        var artifacts = _artifactRepository.GetArtifactsByGallery(galleryId);
            
        return View("Index", artifacts);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var artifact = _artifactRepository.GetById(id);
        ViewBag.Galleries = new SelectList(_galleryRepository.GetAll(), "GalleryId", "Name");

        if (artifact == null)
        {
            return NotFound();
        }
        return View(artifact);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Artifact artifact)
    {
        if (ModelState.IsValid)
        {
            _artifactRepository.UpdateArtifact(artifact);
            return RedirectToAction(nameof(Index));
        }
        return View(artifact);
    }
}