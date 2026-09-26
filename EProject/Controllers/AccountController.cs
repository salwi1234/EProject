
using EProject.Data;
using EProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.ComponentModel;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

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

                // Har new registered account normal User hoga
                user.Role = "User";

                // Password ko hash karna
                var passwordHasher = new PasswordHasher<User>();

                user.password = passwordHasher.HashPassword(
                    user,
                    user.password
                );

                // User database mein save
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
            // Email se user find karo
            var user = await _context.users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }

            // Password hash verify karo
            var passwordHasher = new PasswordHasher<User>();

            var passwordResult = passwordHasher.VerifyHashedPassword(
                user,
                user.password,
                password
            );

            if (passwordResult == PasswordVerificationResult.Failed)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }


            // Authentication Claims

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


            // Admin aur User ke different dashboards

            if (user.Role == "Admin")
            {
                return RedirectToAction(
                    "Index",
                    "Admin"
                );
            }

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


