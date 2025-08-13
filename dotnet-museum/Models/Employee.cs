namespace dotnet_museum.Models;

public class Employee
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Title { get; set; }
    public required string Department { get; set; }
    public required float Salary { get; set; }
    
}