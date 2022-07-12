window.onload = function () {
    listarPersonas();

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