

async function SeleccionarContacto() {
	await seleccionarContacto((contact) => {
		var contenido = "<table class='table'>";
		contenido += `
				<thead>
					<tr>
						<th>Nombre</th>
						<th>Numero</th>
					</tr>
				</thead>

		`
		contenido += "<tbody>";
		for (var i = 0; i < contact.length; i++) {
			contenido += `
					<tr>
						<td>${contact[i].name[0]} </td>
						<td>${contact[i].tel[0]}</td>
					</tr>

				`
		}
		contenido += "</tbody>";
		contenido += "</table>";
		setI("divContactos", contenido)
	})
}