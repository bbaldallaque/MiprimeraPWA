using Microsoft.AspNetCore.Mvc;
using PWA.WEB.Clases;
using PWA.WEB.Models;

namespace PWA.WEB.Controllers
{
    public class LibroController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public LibroController(IWebHostEnvironment env)
        {
           _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public LibroCLS recuperarLibro(int iidLibro)
        {
            LibroCLS olibroCLS = new LibroCLS();
            using (db_a89d9d_dbbibliotecaContext bd = new db_a89d9d_dbbibliotecaContext())
            {
                Libro oLibro = bd.Libros.Where(p => p.Iidlibro == iidLibro).First();
                olibroCLS.iidlibro = oLibro.Iidlibro;
                olibroCLS.titulo = oLibro.Titulo;
                olibroCLS.resumen = oLibro.Resumen;
                olibroCLS.numeropaginas = (int)oLibro.Numpaginas;
                olibroCLS.stock = (int)oLibro.Stock;
                olibroCLS.iidautor = (int)oLibro.Iidautor;
                olibroCLS.base64 = oLibro.Archivo == null ? "" : "data:image/png;base64," + Convert.ToBase64String(oLibro.Archivo);

            }

            return olibroCLS;
        }

        public  int guardarDatos(LibroCLS oLibroCLS)
        {
            int rpta = 0;
            string baseFoto = oLibroCLS.base64.Replace("data:image/png;base64,", "");
            byte[] buffer = null;
            if (oLibroCLS.base64!= "data:,")
            {
                buffer = Convert.FromBase64String(baseFoto);
            }
            using (db_a89d9d_dbbibliotecaContext bd = new db_a89d9d_dbbibliotecaContext())
            {
                try
                {
                    if (oLibroCLS.iidlibro == 0)
                    {
                        Libro oLibro = new Libro();
                        oLibro.Stock = oLibroCLS.stock;
                        oLibro.Titulo = oLibroCLS.titulo;
                        oLibro.Resumen = oLibroCLS.resumen;
                        oLibro.Numpaginas = oLibroCLS.numeropaginas;
                        oLibro.Iidautor = oLibroCLS.iidautor;
                        oLibro.Bhabilitado = 1;
                        if (buffer!=null)
                        oLibro.Archivo = buffer;
                        bd.Libros.Add(oLibro);
                        bd.SaveChanges();
                        rpta = 1;
                    }
                    else
                    {

                        Libro oLibro = bd.Libros.Where(p => p.Iidlibro == oLibroCLS.iidlibro).First();
                        oLibro.Stock = oLibroCLS.stock;
                        oLibro.Titulo = oLibroCLS.titulo;
                        oLibro.Resumen = oLibroCLS.resumen;
                        oLibro.Numpaginas = oLibroCLS.numeropaginas;
                        if (buffer != null)
                        oLibro.Archivo = buffer;
                        bd.SaveChanges();
                        rpta = 1;
                    }
                }
                catch (Exception)
                {

                    rpta = 0;
                }
            }

                return rpta;
        }

        public List<LibroCLS> listarLibros(string nombrelLibro)
        {
            List<LibroCLS> list = new List<LibroCLS>();
            string rutaCompleta = Path.Combine(_env.ContentRootPath, "wwwroot/img/noimage.png");
            byte[] buffer = System.IO.File.ReadAllBytes(rutaCompleta);
            string base64nofoto = Convert.ToBase64String(buffer);
            string base64nofotofinal = "data:image/png;base64," + base64nofoto;
            using (db_a89d9d_dbbibliotecaContext bd = new db_a89d9d_dbbibliotecaContext())
            {
                if (nombrelLibro == null)
                {
                    list = (from libro in bd.Libros
                            join autor in bd.Autors on libro.Iidautor equals autor.Iidautor
                            where libro.Bhabilitado == 1

                            select new LibroCLS
                            {
                                iidlibro = libro.Iidlibro,
                                titulo="<h6>"+libro.Titulo+"</h6><p class='mb-1'>"+ autor.Nombre + " " +
                                autor.Appaterno + "</p>" +"<p class='text-secondary mb-1'> Stock: "+ (int)libro.Stock + "</p>",
                                resumen = libro.Resumen,
                                numeropaginas = (int)libro.Numpaginas,
                                base64 = libro.Archivo == null ? base64nofotofinal
                                 : "data:image/png; base64,"+
                                Convert.ToBase64String(libro.Archivo)

                            }).ToList();
                }
                else
                {
                    list = (from libro in bd.Libros
                            join autor in bd.Autors on libro.Iidautor equals autor.Iidautor
                            where libro.Bhabilitado == 1
                            && libro.Titulo.Contains(nombrelLibro)
                            select new LibroCLS
                            {
                                iidlibro = libro.Iidlibro,
                                titulo = libro.Titulo,
                                resumen = libro.Resumen,
                                numeropaginas = (int)libro.Numpaginas,
                                stock = (int)libro.Stock,
                                nombreautor = autor.Nombre + " " + autor.Appaterno,
                                base64 = libro.Nombrearchivo == null ? base64nofotofinal
                                 : "data:image/" + Path.GetExtension(libro.Nombrearchivo).Replace(".", "") + ";base64," +
                    Convert.ToBase64String(libro.Archivo)

                            }).ToList();
                }

            }
            return list;
        }
    }
}
