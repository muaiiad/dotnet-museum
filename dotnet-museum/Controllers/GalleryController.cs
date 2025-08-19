using dotnet_museum.Data;
using dotnet_museum.Models;
using dotnet_museum.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_museum.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGalleryRepository _repo;

        public GalleryController(IGalleryRepository repo)
        {
            _repo = repo;
        }
        
        public IActionResult GetAllData()
        {
            return Ok(_repo.GetAll());
        }
        public IActionResult GetById(int id)
        {
            return Ok(_repo.GetById(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                _repo.CreateGallery(gallery);
                return RedirectToAction(nameof(Index));
            }
            return View(gallery);
        }

        public IActionResult Index()
        {
            var galleries = _repo.GetAll();
            ViewBag.galleryCount = galleries.Count();

            return View(galleries);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var gallery = _repo.GetById(id);
            
            if(gallery == null)
            {
                return NotFound();
            }
            return View(gallery);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateGallery(gallery);
                return RedirectToAction(nameof(Index));
            }
            return View(gallery);
        }
    }
}