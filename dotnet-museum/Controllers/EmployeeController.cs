using dotnet_museum.Data;
using dotnet_museum.Models;
using dotnet_museum.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_museum.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeController(IEmployeeRepository repo)
        {
            _repo =  repo;
        }
        
        public IActionResult GetAllData()
        {
            return Ok(_repo.GetAll());
        }
        public IActionResult GetById(int id)
        {
            return Ok(_repo.GetById(id));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _repo.CreateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Index()
        {
            var employees = _repo.GetAll();
            ViewBag.EmployeeCount = employees.Count; // Using ViewBag here
            return View(employees);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _repo.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
    }
}