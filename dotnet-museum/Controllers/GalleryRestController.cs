using dotnet_museum.Models;
using dotnet_museum.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_museum.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GalleryRestController : ControllerBase
{
    private readonly IGalleryRepository _repo;

    public GalleryRestController(IGalleryRepository repo)
    {
        _repo = repo;
    }
    
    [HttpGet]
    public IActionResult GetAllData()
    {
        return Ok(_repo.GetAll());
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(_repo.GetById(id));
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Create(Gallery gallery)
    {
        if (ModelState.IsValid)
        {
            _repo.CreateGallery(gallery);
            return Ok(gallery);
        }
        return BadRequest(ModelState);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult Edit(int id, [FromBody]  Gallery gallery)
    {
        if (id != gallery.GalleryId)
        {
            return BadRequest();
        }
        
        var g = _repo.GetById(id);
        if(g == null)
        {
            return NotFound();
        }
        
        _repo.UpdateGallery(gallery);
        return Ok(gallery);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var gallery = _repo.GetById(id);
        if (gallery == null)
        {
            return NotFound();
        }
        _repo.DeleteGallery(gallery);
        return NoContent();
    }
}