window.onload = function () {
	listarTipoLibro();
}

function listarTipoLibro() {

	pintar({
		url: "TipoLibro/listarTipoLibro",
		propiedades: ["nombre", "descripcion"],
		cabeceras: ["Tipo Libro", "Descripcion"],
		titlePopup: "Tipo Libro",
		rowClickRecuperar: true,
		propiedadId: "iidtipolibro"
	},
		{
			url: "TipoLibro/listarTipoLibro",
			formulario: [
				[
					{
						class: "col-md-6",
						label: "Nombre Tipo Libro",
						name: "nombretipolibrobusqueda",
						type: "text"
					}
				]
			]
		}, {
		type: "popup",
			urlguardar: "TipoLibro/guardarTipoLibro",
			urlrecuperar: "TipoLibro/recuperarTipoLibro",
			parametrorecuperar: "id",
		formulario: [
			[
				{
					class: "d-none",
					label: "Id tipo Libro",
					name: "iidtipolibro",
					type: "text"
				},
				{
					class: "col-md-6",
					label: "Nombre Tipo Libro",
					name: "nombre",
					type: "text"
				},
				{
					class: "col-md-6",
					label: "Descripcion Tipo Libro",
					name: "descripcion",
					type: "textarea"
				}
			]
		]

	}
	)

}