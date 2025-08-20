using dotnet_museum.Models.MuseumEvents;
using dotnet_museum.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Index = System.Index;

namespace dotnet_museum.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryRestController : ControllerBase
{
    private readonly ICategoryRepository _repo;

    public CategoryRestController(ICategoryRepository repo)
    {
        _repo = repo;
    }
    
    [HttpGet]
    public IActionResult GetAllData()
    {
        return Ok(_repo.GetCategories());
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var category = _repo.GetById(id);
        if(category == null)
            return NotFound();
        return Ok(category);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Create([FromBody]Category category)
    {
        if (ModelState.IsValid)
        {
            _repo.CreateCategory(category);
            return Ok(category);
        }

        return BadRequest(ModelState);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id, [FromBody] Category category)
    {
        if (id != category.CategoryId)
            return BadRequest();
        
        var c =  _repo.GetById(id);
        if (c == null)
            return NotFound();
        
        _repo.UpdateCategory(category);
        return Ok(category);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var category = _repo.GetById(id);
        if (category == null)
        {
            return NotFound();
        }
        _repo.DeleteCategory(category);
        return NoContent();
    }
}