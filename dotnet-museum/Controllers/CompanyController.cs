using dotnet_museum.Data;
using dotnet_museum.Models.MuseumEvents;
using dotnet_museum.Models.TourismCompany;
using dotnet_museum.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Controllers;

public class CompanyController : Controller
{
    private readonly ICompanyRepository _repo;
    
    public CompanyController(ICompanyRepository repo)
    {
        _repo = repo;
    }
    
    public IActionResult GetAllData()
    {
        return Ok(_repo.GetCompanies());
    }
    public IActionResult GetById(int id)
    {
        return Ok(_repo.GetCompanyById(id));
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
    public IActionResult Create(Company company)
    {
        if (ModelState.IsValid)
        {
            _repo.CreateCompany(company);
            return RedirectToAction(nameof(Index));
        }
        return View(company);
    }
    public IActionResult Index()
    {
        var companies = _repo.GetCompanies();
            
        return View(companies);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var company = _repo.GetCompanyById(id);

        if (company == null)
        {
            return NotFound();
        }
        return View(company);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Company company)
    {
        if (ModelState.IsValid)
        {
            _repo.UpdateCompany(company);
            return RedirectToAction(nameof(Index));
        }
        return View(company);
    }
}