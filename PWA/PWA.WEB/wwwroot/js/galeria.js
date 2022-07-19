window.onload = function () {
    listarCard();
}

function listarCard() {
    var contenido = "";
    var objActual;
    fetchGet("TipoLibro/listarTipoLibro", "json", function (data) {
        for (var i = 0; i < data.length; i++) {
            objActual = data[i]
            contenido += `
<div class="col-md-4 col-md-6 col-xl-3 mt-2 mb-2">
                <div class="card m-auto" style="width: 18rem;">
  <img style='height:200px' src="${objActual.base64}" class="card-img-top" alt="...">
  <div class="card-body">
    <h5 class="card-title">${objActual.nombre}</h5>
    <p class="card-text">${objActual.descripcion}.</p>
    <a href="#" class="btn btn-primary">ver</a>
  <button class="btn btn-primary" onclick='Compartir("${objActual.nombre}","${objActual.descripcion}")'>Compartir</button>
  </div>
</div>
</div>
                `
        }
        document.getElementById("divCard").innerHTML = contenido;
    })
}

function Compartir(nombre, descripcion) {
    compartirDatosAplicaciones(nombre, descripcion, "https://stewartpwa.ga/Galeria/Index")
}