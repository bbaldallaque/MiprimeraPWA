using Microsoft.AspNetCore.Mvc;
using PWA.WEB.Clases;
using PWA.WEB.Models;

namespace PWA.WEB.Controllers
{
    public class AutorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<AutorCLS> listaAutores()
        {
            var lista = new List<AutorCLS>();
            using (db_a89d9d_dbbibliotecaContext bd = new db_a89d9d_dbbibliotecaContext())
            {

                lista = (from autor in bd.Autors
                         where autor.Bhabilitado == 1
                         select new AutorCLS
                         {
                             iidautor = autor.Iidautor,
                             nombreautor = autor.Nombre + " "+ autor.Appaterno+" "+autor.Apmaterno

                         }).ToList();
            }

            return lista;
        }
    }
}
