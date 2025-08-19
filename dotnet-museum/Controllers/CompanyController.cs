using dotnet_museum.Data;
using dotnet_museum.Models.MuseumEvents;
using dotnet_museum.Models.TourismCompany;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Controllers;

public class CompanyController : Controller
{
    private readonly AppDbContext _context;

    public CompanyController(AppDbContext context)
    {
        _context = context;
    }
    
    public IActionResult GetAllData()
    {
        return Ok(_context.Companies.ToList());
    }
    public IActionResult GetById(int id)
    {
        return Ok(_context.Companies.FirstOrDefault(a => a.CompanyId == id));
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
            _context.Companies.Add(company);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(company);
    }
    public IActionResult Index()
    {
        var companies = _context.Companies
            .Include(c => c.Bookings)
            .ThenInclude(b => b.Event)
            .ToList();
            
        return View(companies);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var company = _context.Companies.FirstOrDefault(c => c.CompanyId == id);

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
            _context.Companies.Update(company);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(company);
    }
}