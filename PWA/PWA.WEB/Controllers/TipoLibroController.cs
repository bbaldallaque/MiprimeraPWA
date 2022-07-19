using Microsoft.AspNetCore.Mvc;
using PWA.WEB.Clases;
using PWA.WEB.Models;

namespace PWA.WEB.Controllers
{
    public class TipoLibroController : Controller
    {

        public readonly IWebHostEnvironment _env;

        public TipoLibroController(IWebHostEnvironment env)
        {
            _env = env;
        }



        public IActionResult Index()
        {
            return View();
        }

        public List<TipoLibroCLS> listarTipoLibro(string nombretipolibrobusqueda)
        {
            List<TipoLibroCLS> lista = new List<TipoLibroCLS>();
            string rutaCompleta = Path.Combine(_env.ContentRootPath, "wwwroot/img/noimage.png");
            byte[] buffer = System.IO.File.ReadAllBytes(rutaCompleta);
            string base64nofoto = Convert.ToBase64String(buffer);
            string base64nofotofinal = "data:image/png;base64," + base64nofoto;

            using (db_a89d9d_dbbibliotecaContext bd = new db_a89d9d_dbbibliotecaContext())
            {
                if (nombretipolibrobusqueda == null)
                    lista = (from tipolibro in bd.TipoLibros
                             where tipolibro.Bhabilitado == 1
                             select new TipoLibroCLS
                             {
                                 iidtipolibro = tipolibro.Iidtipolibro,
                                 nombre = tipolibro.Nombretipolibro,
                                 descripcion = tipolibro.Descripcion,
                                 base64 = tipolibro.Nombrearchivo == null ? base64nofotofinal
                                 : "data:image/" + Path.GetExtension(tipolibro.Nombrearchivo).Replace(".", "") + ";base64," +
                    Convert.ToBase64String(tipolibro.Archivo)

                             }).ToList();
                else
                    lista = (from tipolibro in bd.TipoLibros
                             where tipolibro.Bhabilitado == 1 &&
                             tipolibro.Nombretipolibro.Contains(nombretipolibrobusqueda)
                             select new TipoLibroCLS
                             {
                                 iidtipolibro = tipolibro.Iidtipolibro,
                                 nombre = tipolibro.Nombretipolibro,
                                 descripcion = tipolibro.Descripcion,
                                 base64 = tipolibro.Nombrearchivo == null ? base64nofotofinal
                                 : "data:image/" + Path.GetExtension(tipolibro.Nombrearchivo).Replace(".", "") + ";base64," +
                                 Convert.ToBase64String(tipolibro.Archivo)
                             }).ToList();
                return lista;

            }


        }

        public TipoLibroCLS recuperarTipoLibro(int id)
        {
            using (db_a89d9d_dbbibliotecaContext bd = new db_a89d9d_dbbibliotecaContext())
            {
                TipoLibroCLS oTipoLibroCLS = new TipoLibroCLS();
                TipoLibro oTipoLibro =
                            bd.TipoLibros.Where(p => p.Iidtipolibro == id).First();
                oTipoLibroCLS.iidtipolibro = oTipoLibro.Iidtipolibro;
                oTipoLibroCLS.nombre = oTipoLibro.Nombretipolibro;
                oTipoLibroCLS.descripcion = oTipoLibro.Descripcion;
                oTipoLibroCLS.base64 = oTipoLibro.Archivo == null ? "" :
                    "data:image/" + Path.GetExtension(oTipoLibro.Nombrearchivo).Replace(".", "") + ";base64," +
                    Convert.ToBase64String(oTipoLibro.Archivo);
                return oTipoLibroCLS;
            }

        }

        public int guardarTipoLibro(TipoLibroCLS oTipoLibroCLS, IFormFile fotoEnviar)
        {
            int rpta = 0;
            byte[] buffer;
            string nombreFoto = "";
            if (fotoEnviar != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fotoEnviar.CopyTo(ms);
                    nombreFoto = fotoEnviar.FileName;
                    buffer = ms.ToArray();
                    oTipoLibroCLS.foto = buffer;
                    oTipoLibroCLS.nombrefoto = nombreFoto;

                }
            }
            using (db_a89d9d_dbbibliotecaContext bd = new db_a89d9d_dbbibliotecaContext())
            {
                try
                {
                    if (oTipoLibroCLS.iidtipolibro == 0)
                    {
                        TipoLibro oTipoLibro = new TipoLibro();
                        oTipoLibro.Nombretipolibro = oTipoLibroCLS.nombre;
                        oTipoLibro.Descripcion = oTipoLibroCLS.descripcion;
                        oTipoLibro.Archivo = oTipoLibroCLS.foto;
                        oTipoLibro.Nombrearchivo = oTipoLibroCLS.nombrefoto;

                        oTipoLibro.Bhabilitado = 1;
                        bd.TipoLibros.Add(oTipoLibro);
                        bd.SaveChanges();
                        rpta = 1;
                    }
                    else
                    {
                        TipoLibro oTipoLibro =
                            bd.TipoLibros.Where(p => p.Iidtipolibro == oTipoLibroCLS.iidtipolibro).First();
                        oTipoLibro.Nombretipolibro = oTipoLibroCLS.nombre;
                        oTipoLibro.Descripcion = oTipoLibroCLS.descripcion;

                        if (nombreFoto != "")
                        {
                            oTipoLibro.Archivo = oTipoLibroCLS.foto;
                            oTipoLibro.Nombrearchivo = oTipoLibroCLS.nombrefoto;
                        }
                        bd.SaveChanges();
                        rpta = 1;
                    }
                }
                catch (Exception ex)
                {
                    rpta = 0;
                }

            }
            return rpta;
        }

    }
}
