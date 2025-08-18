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
        
        public IActionResult GetAllData()
        {
            return Ok(_db.Employees.ToList());
        }
        public IActionResult GetById(int id)
        {
            return Ok(_db.Employees.FirstOrDefault(a => a.Id == id));
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _db.Employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Update(employee);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
    }
}