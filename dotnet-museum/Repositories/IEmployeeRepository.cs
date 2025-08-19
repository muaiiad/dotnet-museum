using dotnet_museum.Models;

namespace dotnet_museum.Repositories;

public interface IEmployeeRepository
{
    List<Employee> GetAll();
    Employee? GetById(int id);
    void CreateEmployee(Employee employee);
    void UpdateEmployee(Employee employee);
}