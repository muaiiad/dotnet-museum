using dotnet_museum.Data;
using dotnet_museum.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_museum.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _db;

        public EmployeeController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Add(employee);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Index()
        {
            var employees = _db.Employees.ToList();
            ViewBag.EmployeeCount = employees.Count; // Using ViewBag here
            return View(employees);
        }
    }
}