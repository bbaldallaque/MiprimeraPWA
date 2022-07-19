window.onload = function () {
    listarLibros();
}

function listarLibros() {

    fetchGet("Autor/listaAutores", "json", function (rpta) {
        llenarCombo(rpta, "cboAutor","iidautor", "nombreautor")
    })

    pintar({
        url: "Libro/listarLibros",
        propiedades: ["base64", "titulo"],
        cabeceras: ["Foto", "Libro"],
        //titlePopup: "Libro",
        rowClickRecuperar: true,
        type: "popup",
        propiedadId: "iidlibro",
        columnaimg: ["base64"],
        callbackrecuperar: function (id) {
            setI("lblTitulo", "Editar Libro")
            LimpiarDatos("frmlibro")
            document.getElementById("videoFoto").srcObject = null;
            document.getElementById("videoFoto").poster = "";
            recuperarGenerico("Libro/recuperarLibro/?iidlibro=" + id, "frmlibro", function (data) {
                document.getElementById("videoFoto").poster = data.base64;
            })
        }
    })
}

function Nuevo() {
    LimpiarDatos("frmlibro")
    document.getElementById("videoFoto").srcObject = null;
    document.getElementById("videoFoto").poster = "";
    setI("lblTitulo", "Agregar Libro")
}



function GuardarDatos() {
    var frmlibro = document.getElementById("frmlibro")
    var frm = new FormData(frmlibro)
    var fotoTomada = obtenerImagenVideo("videoFoto")
    frm.append("base64", fotoTomada)

   var errores = ValidarDatos("frmlibro")

    if (errores != "") {
        Error(errores)
        return;
    }

    Confirmacion("Confirmación", "Desea guardar cambios?", function () {
        fetchPost("Libro/guardarDatos", "text", frm, function (rpta) {
            if (rpta == 1) {
                Exito("Se guardo correctamente");
                listarLibros()
                document.getElementById("btnCerrar").click();
            }
            else {
                Error();
            }
        })
    })
}