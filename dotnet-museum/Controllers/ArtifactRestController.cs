using Asp.Versioning;
using dotnet_museum.Models;
using dotnet_museum.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_museum.Controllers;


[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class ArtifactRestController : ControllerBase
{
    
    private readonly IArtifactRepository _artifactRepository;
    
    public ArtifactRestController(IArtifactRepository artifactRepository)
    {
        _artifactRepository  = artifactRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        
        return Ok(_artifactRepository.GetArtifacts());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        if (_artifactRepository.GetArtifacts().Any(a => a.Id == id))
        {
            return Ok(_artifactRepository.GetById(id));
        }
        return NotFound();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Post([FromBody] Artifact artifact)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        _artifactRepository.CreateArtifact(artifact);
        return Ok(artifact);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public IActionResult Put(int id, [FromBody] Artifact artifact)
    {
        _artifactRepository.UpdateArtifact(id, artifact);
        return Ok(artifact);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        _artifactRepository.DeleteArtifact(id);
        return Ok();
    }
    
    
    
    
}