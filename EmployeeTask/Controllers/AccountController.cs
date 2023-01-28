using EmployeeTask.DAL;
using EmployeeTask.Models;
using EmployeeTask.View_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace EmployeeTask.Controllers
{

    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        AppDbContext _context;

        public AccountController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM logVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(logVM.UsernameOrEmail);
            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(logVM.UsernameOrEmail);
                if (user is null)
                {
                    ModelState.AddModelError("", "Login Or Password is wrong");
                }
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, logVM.Password, logVM.RememberMe, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Login or Password is wrong");
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM regVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = new AppUser
            {
                Name = regVM.Name,
                Surname = regVM.Surname,
            };

            var result = await _userManager.CreateAsync(user, regVM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }


           
            _context.SaveChanges();
            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

       
    }



}