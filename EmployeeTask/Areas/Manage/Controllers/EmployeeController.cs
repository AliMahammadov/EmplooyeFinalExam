using EmployeeTask.DAL;
using EmployeeTask.Models;
using EmployeeTask.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTask.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class EmployeeController : Controller
    {
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        AppDbContext _context { get; }

        public IActionResult Index()
        {
            return View(_context.Employees.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateVM create)
        {
            if (create is null)
            {
                return BadRequest();
            }
            Employee employee = new Employee()
            {
                ImgUrl = create.ImgUrl,
                Name = create.Name,
                Surname = create.Surname,
                Position = create.Position,
                About = create.About,
                Tvitter = create.Tvitter,
                Facebook = create.Facebook,
                Instagram = create.Instagram,
                Linkedin = create.Linkedin,
            };
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            if (id == null) return NotFound();
            Employee employee = _context.Employees.Find(id);
            if (employee == null) return BadRequest();
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            Employee employee = _context.Employees.Find(id);
            if (employee == null) return NotFound();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Update(int? id, Employee employee)
        {
            if (id==null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid) { return BadRequest(); }
            if (employee == null) { return NotFound(); }
            Employee exist = _context.Employees.Find(id);
            if (employee is null)
            {
                return BadRequest();
            }
            exist.Name = employee.Name;
            exist.Surname = employee.Surname;
            exist.ImgUrl = employee.ImgUrl;
            exist.Position = employee.Position;
            exist.About = employee.About;
            exist.Tvitter = employee.Tvitter;
            exist.Facebook = employee.Facebook;
            exist.Instagram = employee.Instagram;
            exist.Linkedin = employee.Linkedin;
            _context.Employees.Update(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));



        }
    }
}
