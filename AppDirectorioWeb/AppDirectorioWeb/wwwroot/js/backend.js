
function Backend() {

    this.GetToBackend = async function (uri) {
        let uriFinal =   uri;
        console.info("Peticion a: " + uriFinal);

        

        let respuesta = await fetch(uriFinal);

       

        if (respuesta.ok) {
            return respuesta.json();
        }
        else {
            console.warn("Estado de peticion: " + respuesta.status);
            return [];
        }
    }

    this.PostToBackend = async function (uri, model) {
        let urifinal = uri;
        let _data = model;
        console.info("Peticion a: " + urifinal);
      
        let respuesta = await fetch(urifinal, {
            method: "POST",
            body: JSON.stringify(_data),
            headers: { "Content-type": "application/json; charset=UTF-8" }
        });

        if (respuesta.ok) {
          
            return respuesta.json();
        }
        else {
           
            console.warn("Estado de peticion: " + respuesta.status);
            return [];
        }

    }

    this.PutToBackend = async function (uri, model) {
        let urifinal = uri;
        let _data = model;
        console.info("Peticion a: " + urifinal);
       
        let respuesta = await fetch(urifinal, {
            method: "PUT",
            body: JSON.stringify(_data),
            headers: { "Content-type": "application/json; charset=UTF-8" }
        });

        if (respuesta.ok) {
           
            return respuesta.json();
        }
        else {
           
            console.warn("Estado de peticion: " + respuesta.status);
            return [];
        }

    }

    this.DeleteToBackend = async function (uri, model) {
        let urifinal =  uri;
        let _data = model;
        console.info("Peticion a: " + urifinal);
       
        let respuesta = await fetch(urifinal, {
            method: "DELETE",
            headers: { "Content-type": "application/json; charset=UTF-8" }
        });

        if (respuesta.ok) {
          
            return respuesta.json();
        }
        else {
           
            console.warn("Estado de peticion: " + respuesta.status);
            return [];
        }

    }

    this.ExportBackend = function (uri) {
        let uriFinal = uriBackend + uri;
        console.info("Peticion a: " + uriFinal);

        window.open(uriFinal, '_blank');
    }
}

const $backend = new Backend();