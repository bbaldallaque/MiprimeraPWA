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


		event.request.clone().formData().then(formdata => {
			console.log(formdata)
			console.log(Object.fromEntries(formdata))
        })

		event.respondWith(fetch(event.request))
	}

})
