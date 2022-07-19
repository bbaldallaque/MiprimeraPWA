importScripts("/js/pouchDB.js")

var nombreCacheDinamico = "cacheDinamico1"
var nombreCacheEstatico = "cacheEstatico1"
var archivosEstaticos = [
	"/css/menu.css",
	"/PWA.WEB.styles.css",
	"/lib/jquery/dist/jquery.min.js",
	"/lib/bootstrap/dist/js/bootstrap.bundle.min.js",
	"/js/menu.js",
	"/js/generic.js",
	"/img/loading.gif",
	"/Persona/ListarPersonas",
	"/PaginaError/Index",
	"/js/sweetalert.js",
	"/TipoLibro/ListarTipoLibro",
	"/js/pouchDB.js",
	"/js/libro.js",
	"/js/galeria.js",
	"/"
]
self.addEventListener("install", event => {

	console.log("Evento Install")
	event.waitUntil(
		caches.open(nombreCacheEstatico).then(cache => {
			return cache.addAll(archivosEstaticos)
		})
	)


})

self.addEventListener("activate", event => {

	console.log("Evento Activate")
	event.waitUntil(self.clients.claim())
})

self.addEventListener("fetch", event => {
	if (event.request.method != "POST") {
		const respuesta =
			fetch(event.request).then(response => {
				caches.open(nombreCacheDinamico).then(cache => {
					cache.put(event.request, response)
				})
				return response.clone();
			}).catch(err => {

				return caches.match(event.request).then(res => {

					if (res) return res;
					else {
						if (event.request.headers.get("accept").includes("text/html")) {
							return caches.match("/PaginaError/Index")
						} else {
							var response = new Response(`
			<h1 class="text-danger">Para realizar esta accion necesita internet</h1>
		`, {
								headers: {
									"Content-Type": "text/html"
								}
							})
							return response;
						}
					}

				})
			})

		event.respondWith(respuesta)
	} else {
		if (self.registration.sync) {
			var respuesta = fetch(event.request.clone()).then(response => {
				if (response) return response

			}).catch(err => {
				return event.request.clone().formData().then(formdata => {

					//console.log(formdata)
					var db = new PouchDB("BDBiblioteca")
					var objeto = Object.fromEntries(formdata)
					//_id
					objeto._id = new Date().toISOString()
					objeto.url = event.request.url
					return db.put(objeto).then(res => {
						self.registration.sync.register("insertData")
						return new Response("2", {
							headers: {
								"Content-Type": "text/plain"
							}
						})

					})
				})
			})
			event.respondWith(respuesta)
		} else {
			event.respondWith(fetch(event.request))
		}

	}

})

self.addEventListener("sync", event => {

	console.log("Entro")
	var db = new PouchDB("BDBiblioteca")
	var respuesta = db.allDocs({ include_docs: true }).then(data => {
		data.rows.forEach(fila => {

			var doc = fila.doc;
			var frm = new FormData();
			for (var key in doc) {
				frm.append(key, doc[key])
			}

			console.log(doc)
			return fetch(doc.url, {
				method: "POST",
				body: frm
			}).then(res => {
				db.remove(doc)
			})

		})
	})

	event.waitUntil(respuesta)
})

self.addEventListener("push", event => {

	var data = event.data.text()
	var valores = data.split("_")
	var titulo = valores[0]
	var opciones = {
		body: valores[1],
		icon: valores[2],
		vibrate: [200, 100, 200, 100, 200, 100, 200]
	}
	event.waitUntil(self.registration.showNotification(titulo, opciones))
})