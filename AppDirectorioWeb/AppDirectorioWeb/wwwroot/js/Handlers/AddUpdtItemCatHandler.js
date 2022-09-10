$(document).ready(function () {
    image();
    checkIsDescuentoMasivo();


    //si estamos en modo edición
    var esUpdate = $('#Id').val();

    if (esUpdate != 0) {
        console.log('es update');

        if ($('input[name=TieneDescuento]').is(':checked') == true) {
            $('#divMtoDctoMasivo').show();
            $("#PorcentajeDescuento").attr("required", true);

        } else {
            $('#divMtoDctoMasivo').hide();
            $("#PorcentajeDescuento").attr("required", false);
        }


    }
    //fin

    $('input[name=TieneDescuento]').on('change', function () {

        var isTypePorcent = $('input[name=TieneDescuento]').is(':checked');
        console.log(isTypePorcent);
        if (isTypePorcent == false) {

            $('#divMtoDctoMasivo').hide();
            $("#PorcentajeDescuento").attr("required", false);
        }
        else {

            $('#divMtoDctoMasivo').show();
            $("#PorcentajeDescuento").attr("required", true);
        }

    });



});


function image() {
    $('.cfiinvisible').attr("disabled", true);

    $('.cfi').on("change", function () {
        $('#picVal').html('');
        var filename = $(this).val().split("\\").pop();
        $(this).next('.cfl').html(filename);



        // recuperamos la extensión del archivo
        var ext = filename.split('.').pop();

        // Convertimos en minúscula porque 
        // la extensión del archivo puede estar en mayúscula
        ext = ext.toLowerCase();

        // console.log(ext);
        switch (ext) {
            case 'jpg':
            case 'jpeg':
            case 'png': break;
            default:
                $('#picVal').html('El archivo no tiene la extensión adecuada, solo se aceptan imagenes');
                // alert('El archivo no tiene la extensión adecuada');
                this.value = ''; // reset del valor
                $(this).next('.cfl').html('Selecciona la foto para el cuón...');
        }
    });

}

function DeleteItemPic(Id) {

    swal({
        title: "Estas seguro que desea eliminar esta imagén?",
        text: "Una vez borrado, no puedes recuperar el registro",
        icon: "warning",
        buttons: true,
        dangerMode: true

    }).then((willDelete) => {
        if (willDelete) {


            $.ajax({
                type: "GET",
                url: '/Ventas/ItemCatalogoProdServ/DeleteItemPic/' + Id,
                success: function (data) {

                    if (data.success) {
                        /*toastr.success(data.message);*/
                        swal({ title: data.message, icon: "success" });
                        $('.cfiinvisible').removeAttr("disabled");
                        $('#adelpiccupon').text('');

                    } else {
                        /*toastr.error(data.message);*/
                        swal({ title: data.message, icon: "info" });
                    }
                }
            });
        }
    });
}




function checkIsDescuentoMasivo() {
    var isTypePorcent = $('input[name=TieneDescuento]').is(':checked');
    console.log(isTypePorcent);
    if (isTypePorcent == false) {

        $('#divMtoDctoMasivo').hide();
        $("#PorcentajeDescuento").attr("required", false);
    }
    else {

        $('#divMtoDctoMasivo').show();
        $("#PorcentajeDescuento").attr("required", true);
    }
}

