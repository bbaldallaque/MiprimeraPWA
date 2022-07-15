window.onload = function () {
    listarPersonas();
    activarNotificaciones();

}

function activarNotificaciones() {

    if (window.Notification) {

        if (Notification.permission != "granted") {

            Notification.requestPermission(function (rpta) {
                console.log(rpta)
            })

        }

    }

}



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