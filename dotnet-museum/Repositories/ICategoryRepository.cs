using dotnet_museum.Models.MuseumEvents;

namespace dotnet_museum.Repositories;

public interface ICategoryRepository
{
    List<Category> GetCategories();
    Category? GetById(int id);
    void CreateCategory(Category category);
    List<Category> ListCategories();
    void UpdateCategory(Category category);
    void DeleteCategory(Category category);
}