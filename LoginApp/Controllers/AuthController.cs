using Microsoft.AspNetCore.Mvc;

namespace LoginApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
