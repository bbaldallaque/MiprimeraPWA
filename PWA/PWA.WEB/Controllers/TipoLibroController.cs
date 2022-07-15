using Microsoft.AspNetCore.Mvc;
using PWA.WEB.Clases;
using PWA.WEB.Models;

namespace PWA.WEB.Controllers
{
    public class TipoLibroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<TipoLibroCLS> ListarTipoLibro(string nombreTipoLibro)
        {
            List<TipoLibroCLS> lista = new List<TipoLibroCLS>();
            using (db_a89d9d_dbbibliotecaContext bd = new db_a89d9d_dbbibliotecaContext())
            {
                if (nombreTipoLibro == null)
                {
                    lista = (from tipolibro in bd.TipoLibros
                             where tipolibro.Bhabilitado == 1
                             select new TipoLibroCLS
                             {
                                 iidtipolibro = tipolibro.Iidtipolibro,
                                 nombre = tipolibro.Nombretipolibro,
                                 descripcion = tipolibro.Descripcion
                             }).ToList();
                }
                else
                {
                    lista = (from tipolibro in bd.TipoLibros
                             where tipolibro.Bhabilitado == 1
                             && tipolibro.Nombretipolibro.Contains(nombreTipoLibro)
                             select new TipoLibroCLS
                             {
                                 iidtipolibro = tipolibro.Iidtipolibro,
                                 nombre = tipolibro.Nombretipolibro,
                                 descripcion = tipolibro.Descripcion
                             }).ToList();
                }

                return lista;
            }
        }

        public TipoLibroCLS recuperarTipoLibro(int id)
        {
            TipoLibroCLS  oTipoLibroCLS=new TipoLibroCLS(); ;

            using (db_a89d9d_dbbibliotecaContext bd = new db_a89d9d_dbbibliotecaContext())
            {
                TipoLibro oTipoLibro = bd.TipoLibros.Where(p => p.Iidtipolibro == id).First();

                oTipoLibroCLS.iidtipolibro = oTipoLibro.Iidtipolibro;
                oTipoLibroCLS.nombre = oTipoLibro.Nombretipolibro;
                oTipoLibroCLS.descripcion = oTipoLibro.Descripcion;

                return oTipoLibroCLS;
            }

        }




        public int guardarTipoLibro(TipoLibroCLS oTipoLibroCLS)
        {
            int rpta = 0;
            using (db_a89d9d_dbbibliotecaContext bd = new db_a89d9d_dbbibliotecaContext())
            {
                try
                {
                    if (oTipoLibroCLS.iidtipolibro == 0)
                    {
                        TipoLibro oTipoLibro = new TipoLibro();
                        oTipoLibro.Nombretipolibro = oTipoLibroCLS.nombre;
                        oTipoLibro.Descripcion = oTipoLibroCLS.descripcion;
                        oTipoLibro.Bhabilitado = 1;
                        bd.TipoLibros.Add(oTipoLibro);
                        bd.SaveChanges();
                        rpta = 1;
                    }
                    else
                    {
                        TipoLibro oTipoLibro = bd.TipoLibros.Where(p => p.Iidtipolibro == oTipoLibroCLS.iidtipolibro).First();
                        oTipoLibro.Nombretipolibro = oTipoLibroCLS.nombre;
                        oTipoLibro.Descripcion = oTipoLibroCLS.descripcion;
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
    }
}
