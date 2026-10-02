using Microsoft.AspNetCore.Mvc;

namespace EProject.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
