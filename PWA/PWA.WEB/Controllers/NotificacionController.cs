using Microsoft.AspNetCore.Mvc;
using PWA.WEB.Clases;
using PWA.WEB.Models;
using WebPush;

namespace PWA.WEB.Controllers
{
    public class NotificacionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string generarLlavePublica()
        {
            return Notificaciones.llavePublica;
        }

        public async Task<int> enviarNotificaciones(string parametroPorContenido)
        {
            int rpta = 0;           
            string subject = "mailto:dr_baldallaque@hotmail.com";
            string llavePublica = Notificaciones.llavePublica;
            string llavePrivada = Notificaciones.llavePrivada;
            PushSubscription oPushSubscription;
            var vapidDetail = new VapidDetails(subject, llavePublica, llavePrivada);
            var webpushClient = new WebPushClient();
            try
            {
                using (db_a89d9d_dbbibliotecaContext bd = new db_a89d9d_dbbibliotecaContext())
                {
                    List<Notificacione> lista = bd.Notificaciones.ToList();
                    foreach (Notificacione oNotificacione in lista)
                    {
                        try
                        {

                            oPushSubscription = new PushSubscription(oNotificacione.Endpointnotificacion,
                                oNotificacione.P256dhnotificacion, oNotificacione.Authnotificacion);
                            await webpushClient.SendNotificationAsync(oPushSubscription, parametroPorContenido, vapidDetail);


                            rpta = 1;

                        }

                        catch (WebPushException ex)
                        {
                            if (ex.StatusCode.ToString() == "Gone")
                            {
                                bd.Remove(oNotificacione);
                                bd.SaveChanges();
                                rpta = 1;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                rpta = 0;
            }
            return rpta;
        }

        public int guardarSubscripcion(SubscripcionCLS oSubscripcionCLS)
        {
            int rpta = 0;          
            try
            {
                using (db_a89d9d_dbbibliotecaContext bd = new db_a89d9d_dbbibliotecaContext())
                {
                    Notificacione oNotificacion = new Notificacione();
                    oNotificacion.Endpointnotificacion = oSubscripcionCLS.endpoint;
                    oNotificacion.Authnotificacion = oSubscripcionCLS.auth;
                    oNotificacion.P256dhnotificacion = oSubscripcionCLS.p256dh;
                    oNotificacion.Bhabilitado = 1;
                    bd.Notificaciones.Add(oNotificacion);
                    bd.SaveChanges();
                    rpta = 1;
                }
            }
            catch (Exception ex)
            {

                rpta = 0;
            }

            return rpta;

        }

    }
}
