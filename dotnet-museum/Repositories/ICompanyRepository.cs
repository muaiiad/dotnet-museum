using dotnet_museum.Models.TourismCompany;

namespace dotnet_museum.Repositories;

public interface ICompanyRepository
{
    List<Company> GetCompanies();
    Company? GetCompanyById(int id);
    void CreateCompany(Company company);
    List<Company> GetDetailedCompanies();
    void UpdateCompany(Company company);
}