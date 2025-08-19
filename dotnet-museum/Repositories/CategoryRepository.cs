using dotnet_museum.Data;
using dotnet_museum.Models.MuseumEvents;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public List<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }

    public Category? GetById(int id)
    {
        return _context.Categories.FirstOrDefault(a => a.CategoryId == id);
    }

    public void CreateCategory(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
    }

    public List<Category> ListCategories()
    {
        return _context.Categories
            .Include(c => c.Events)
            .ToList();
    }

    public void UpdateCategory(Category category)
    {
        _context.Categories.Update(category);
        _context.SaveChanges();
    }
}