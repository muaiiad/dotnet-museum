using dotnet_museum.Data;
using dotnet_museum.Models.MuseumEvents;
using dotnet_museum.Models.TourismCompany;
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

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

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