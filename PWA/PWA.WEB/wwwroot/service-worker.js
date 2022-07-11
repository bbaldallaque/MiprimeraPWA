var nombreCacheEstatico="cacheEstatico1"
var archivosEstaticos = [
    "/css/menu.css",
    "/PWA.WEB.styles.css",
    "/lib/jquery/dist/jquery.min.js",
    "/lib/bootstrap/dist/js/bootstrap.bundle.min.js",
    "/js/menu.js",
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

	const respuesta = caches.match(event.request).then(res => {

		if (res) return res;
		else {
			return fetch(event.request).then(response => {
				return response;
			})
		}

	}).catch(err => {
		return null;
	})
	//if (event.request.url == "https://localhost:7008/css/menu.css") {
	//	event.respondWith(null);
	//}else
	//console.log(event.request.url)
	event.respondWith(respuesta)
})
