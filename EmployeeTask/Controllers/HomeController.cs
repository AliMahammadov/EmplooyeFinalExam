using EmployeeTask.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeTask.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        AppDbContext _context { get; }

        public IActionResult Index()
        {
            return View(_context.Employees.ToList());
        }
    }
}