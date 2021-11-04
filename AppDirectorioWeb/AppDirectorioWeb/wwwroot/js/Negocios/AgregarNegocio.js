
$(document).ready(function () {
    let valueProperty = "value";
    let textProperty = "text";
   
    $.ajax({
        type: "GET",
        url: "/Catalogos/getCatalogosByPadre",
        data: { Padre: "Paises".toString() },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var opt = data[i];
                console.log(data[i].text);
                $("#IdPais").append(new Option(opt[textProperty], opt[valueProperty]));
            }


        },
        contentType: 'application/json'
    });



    $.ajax({
        type: "GET",
        url: "/Catalogos/getCatalogosByPadre",
        data: { Padre: "Categorias Negocio".toString() },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var opt = data[i];
                console.log(data[i]);
                $("#IdCategoria").append(new Option(opt[textProperty], opt[valueProperty]));
            }


        },
        contentType: 'application/json'
    });



    $('#IdPais').change(function () {

        $('#IdDepartamento')
            .find('option')
            .remove()
            .end()
            .append(' <option value="">Eliga una opción</option>')
            .val('whatever')
            ;
        let valueProperty = "value";
        let textProperty = "text";

        let textProperty1 = "text";
        $.ajax({
            type: "GET",
            url: "/Catalogos/GetCatalogosxId",
            data: { id: $('#IdPais').val().toString() },
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    var opt = data[i];
                    console.log(data[i].text);
                    $("#IdDepartamento").append(new Option(opt[textProperty], opt[valueProperty]));
                }


            },
            contentType: 'application/json'
        });
    });
 

});



$(function () {

   
    //let fm = new FileManager({ id: '#fileManager', maxFileLen: 1 });
    let marker = new google.maps.Marker(),
        geocoder = new google.maps.Geocoder(),
        mapholder = document.getElementById("mapholder"),
        inputLatitud = $('#hdfLatitud'),
        inputLongitud = $('hdfLongitud');
    var formSelector = "form#form_atencion";
    $(document).on("submit", formSelector, function (event) {
        event.preventDefault(); //Prevengo el submit
        // Obtener archivos uso de método GetFiles()
        //let listArchivos = fm.GetFiles();
        //let jsonFiles = JSON.stringify(listArchivos);
        //console.log(jsonFiles);
        //$(hiddenArchivos).val(jsonFiles);

        $form.onSubmit(event, $save, function (form, result) {
            // Confirmación aceptada
            if (result) {
             
                // Disparando submit del formulario de forma asincrónica.
                $form.xhrSubmit({
                    form: form,

                    success: function (data) {
                       
                        console.log(data);
                        let respuesta = JSON.parse(data.response);
                        console.log(respuesta);
                       
                    },
                    error: function (data) {
                     
                        console.log(data);
                       
                    }
                });
            }
        });


    });

    function showPosition() {

        let mapOptions = {
            zoom: 14,
            mapTypeId: 'roadmap',
            mapTypeControl: false,
            navigationControlOptions: { style: google.maps.NavigationControlStyle.SMALL }
        },
            address = "",
            country = "Costa Rica",
            provincia = "",
            canton = "",
            distrito = "";

        if (provincia !== undefined) {
            address += provincia + country;
            canton = "";
        }

        if (canton !== undefined) {
            address += ", " + canton;
            distrito = "";
        }

        if (distrito !== undefined)
            address += ", " + distrito;

        let map = new google.maps.Map(mapholder, mapOptions),
            lat = "12.12778168245564",
            lon = "-86.26482009887695";

        $('#inputlat').val(lat);
        $('#inputlon').val(lon);

      

            map.setCenter(new google.maps.LatLng(lat, lon));
        

        google.maps.event.addListener(map, 'click', function (event) {
            marker.setMap(null);
            placeMarker(map, event.latLng);
        });
    }

    function placeMarker(map, location) {

        let infowindow = new google.maps.InfoWindow();

        marker = new google.maps.Marker({
            position: location,
            map: map
        });
        console.log(location.lat());
        console.log(location.lng());
        $('#inputlat').val(location.lat());
        $('#inputlon').val(location.lng());

        geocoder.geocode({
            'location': location
        }, function (results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
                if (results[1]) {
                    infowindow = new google.maps.InfoWindow({
                        content: results[1].formatted_address
                    });
                }
            }
        });

        google.maps.event.addListener(marker, 'click', function () {
            infowindow.open(map, marker);
            marker.setMap(null);
        });
    }

    showPosition();



});




