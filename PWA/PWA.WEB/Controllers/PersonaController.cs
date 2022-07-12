using Microsoft.AspNetCore.Mvc;
using PWA.WEB.Clases;
using PWA.WEB.Models;

namespace PWA.WEB.Controllers
{
    public class PersonaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<PersonaCLS> ListarPersonas(string nombreCompleto)
        {
            List<PersonaCLS> list = new List<PersonaCLS>();
            using (db_a89d9d_dbbibliotecaContext bd = new db_a89d9d_dbbibliotecaContext())
            {
                if (nombreCompleto == null)
                {
                    list = (from persona in bd.Personas
                            where persona.Bhabilitado == 1
                            select new PersonaCLS
                            {
                                iidepersona = persona.Iidpersona,
                                nombreCompleto = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
                                correo = persona.Correo

                            }).ToList();
                }
                else
                {
                    list = (from persona in bd.Personas
                            where persona.Bhabilitado == 1
                            && (persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno).Contains(nombreCompleto)
                            select new PersonaCLS
                            {
                                iidepersona = persona.Iidpersona,
                                nombreCompleto = persona.Nombre+" "+ persona.Appaterno + " " + persona.Apmaterno,
                                correo = persona.Correo

                            }).ToList();
                }

            }
            return list;
        }
    }
}
