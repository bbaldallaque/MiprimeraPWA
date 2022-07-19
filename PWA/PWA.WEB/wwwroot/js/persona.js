window.onload = function () {
    listarPersonas();
  /*  activarNotificaciones();*/

}

//function activarNotificaciones() {

//    if (window.Notification) {

//        if (Notification.permission != "granted") {

//            Notification.requestPermission(function (rpta) {
//                if (rpta == "granted") {
//                    new Notification("Mi primera notificacion", {
//                        body: "Esta notificacion la vimos en el curso de PWA",
//                        icon: "/img/icon-192.png"


//                    })
//                }
//            })

//        }

//    }

//}


function listarPersonas() {

    pintar({
        url: "Persona/ListarPersonas",
        propiedades: ["nombreCompleto", "correo"],
        cabeceras: ["Nombre Completo", "Correo"]
    }, {
        url: "Persona/ListarPersonas",
        formulario: [
            [
                {
                    class: "col-md-6",
                    label: "Nombre Completo",
                    name: "nombreCompleto",
                    type: "text"
                }
            ]
        ]
       }
    )
}