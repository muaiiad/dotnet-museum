using dotnet_museum.Data;
using dotnet_museum.Models.TourismCompany;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly AppDbContext _context;

    public CompanyRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public List<Company> GetCompanies()
    {
        return _context.Companies.ToList();
    }

    public Company? GetCompanyById(int id)
    {
       return _context.Companies.FirstOrDefault(a => a.CompanyId == id);
    }

    public void CreateCompany(Company company)
    {
        _context.Companies.Add(company);
        _context.SaveChanges();
    }

    public List<Company> GetDetailedCompanies()
    {
        var companies = _context.Companies
            .Include(c => c.Bookings)
            .ThenInclude(b => b.Event)
            .ToList();
        return companies;
    }

    public void UpdateCompany(Company company)
    {
        _context.Companies.Update(company);
        _context.SaveChanges();
    }
}