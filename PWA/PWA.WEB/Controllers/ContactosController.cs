using Microsoft.AspNetCore.Mvc;

namespace PWA.WEB.Controllers
{
    public class ContactosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
