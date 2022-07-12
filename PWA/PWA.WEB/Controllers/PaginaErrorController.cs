using Microsoft.AspNetCore.Mvc;

namespace PWA.WEB.Controllers
{
    public class PaginaErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
