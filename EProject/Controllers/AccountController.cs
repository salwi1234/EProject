
using EProject.Data;
using EProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Data;
using System.Security.Claims;

namespace EProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }



        // REGISTER - GET

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }



        // REGISTER - POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                bool emailExists = await _context.users
                    .AnyAsync(u => u.Email == user.Email);

                if (emailExists)
                {
                    ModelState.AddModelError(
                        "Email",
                        "This email is already registered."
                    );

                    return View(user);
                }

                // Admin and User Role
                if (user.Email == "admin@gmail.com")
                {
                    user.Role = "Admin";
                }
                else
                {
                    user.Role = "User";
                }

                // Password hash
                var passwordHasher = new PasswordHasher<User>();

                user.password = passwordHasher.HashPassword(
                    user,
                    user.password
                );

                _context.users.Add(user);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Login));
            }

            return View(user);
        }



        // LOGIN - GET

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        // LOGIN - POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            string email,
            string password)
        {

            // NORMAL USER LOGIN

            var user = await _context.users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }



            // Check hashed password

            var passwordHasher = new PasswordHasher<User>();

            var passwordResult =
                passwordHasher.VerifyHashedPassword(
                    user,
                    user.password,
                    password
                );


            if (passwordResult ==
                PasswordVerificationResult.Failed)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }



            // USER CLAIMS

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()
                ),

                new Claim(
                    ClaimTypes.Name,
                    user.FullName
                ),

                new Claim(
                    ClaimTypes.Email,
                    user.Email
                ),

                new Claim(
                    ClaimTypes.Role,
                    user.Role
                )
            };


            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);


            // Login Cookie

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );



            // ROLE CHECK

            if (user.Role == "Admin")
            {
                return RedirectToAction(
                    "Index",
                    "Admin"
                );
            }


            // Normal User

            return RedirectToAction(
                "Index",
                "User"
            );
        }



        // LOGOUT

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction(nameof(Login));
        }
    }
}

