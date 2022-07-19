using Microsoft.AspNetCore.Mvc;

namespace PWA.WEB.Controllers
{
    public class GaleriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
