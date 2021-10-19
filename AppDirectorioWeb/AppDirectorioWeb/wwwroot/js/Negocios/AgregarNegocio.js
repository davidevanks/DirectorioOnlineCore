
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
