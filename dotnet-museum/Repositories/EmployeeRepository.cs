using dotnet_museum.Data;
using dotnet_museum.Models;

namespace dotnet_museum.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public List<Employee> GetAll()
    {
        return _context.Employees.ToList();
    }

    public Employee? GetById(int id)
    {
        return _context.Employees.FirstOrDefault(a => a.Id == id);
    }

    public void CreateEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public void UpdateEmployee(Employee employee)
    {
        _context.Employees.Update(employee);
        _context.SaveChanges();
    }
}