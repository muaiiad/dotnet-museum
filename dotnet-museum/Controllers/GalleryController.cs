using dotnet_museum.Data;
using dotnet_museum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_museum.Controllers
{
    public class GalleryController : Controller
    {
        private readonly AppDbContext _db;

        public GalleryController(AppDbContext db)
        {
            _db = db;
        }
        
        public IActionResult GetAllData()
        {
            return Ok(_db.Galleries.ToList());
        }
        public IActionResult GetById(int id)
        {
            return Ok(_db.Galleries.FirstOrDefault(a => a.GalleryId == id));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                _db.Galleries.Add(gallery);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(gallery);
        }

        public IActionResult Index()
        {
            var galleries = _db.Galleries.ToList();
            ViewBag.galleryCount = galleries.Count();

            return View(galleries);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var gallery = _db.Galleries.FirstOrDefault(g => g.GalleryId == id);
            
            if(gallery == null)
            {
                return NotFound();
            }
            return View(gallery);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                _db.Galleries.Update(gallery);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(gallery);
        }
    }
}