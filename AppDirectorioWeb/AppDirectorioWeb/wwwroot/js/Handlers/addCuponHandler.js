$(document).ready(function () {
  
    checkTypeCupon();
    image();

    var esUpdate = $('#Id').val();

    if (esUpdate != 0)
    {
        //falta setear valores del tipo de descuento y moneda cuando se hace update
    }
});

function image()
{
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

function DeleteCuponPic(Id) {

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
                url: '/Cuponera/Cuponera/DeleteCuponPic/' + Id,
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


function checkTypeCupon() {

    //TypeCupon,DescuentoMonto
    var isTypePorcent = $('input[name=TypeCupon]:checked').val();
    console.log(isTypePorcent);

    if (isTypePorcent === "Per") {
        $('#DescuentoMonto').val(false);
        $('#divTipoMoneda').hide();
    }
    else
    {
        $('#DescuentoMonto').val(true);
        $('#divTipoMoneda').show();
    }



    $('input[name=TypeCupon]').on('change', function () {

        var isTypePorcent = $('input[name=TypeCupon]:checked').val();
        if (isTypePorcent === "Per") {
            $('#DescuentoMonto').val(false);
            $('#divTipoMoneda').hide();
        }
        else {
            $('#DescuentoMonto').val(true);
            $('#divTipoMoneda').show();
        }

    });

    //capturar tipo de moneda
    var tipoMoneda = $('#cboIdMoneda').val();

    $('#MonedaMonto').val(tipoMoneda);

    $('#cboIdMoneda').on('change', function () {

        tipoMoneda = $('#cboIdMoneda').val();
        $('#MonedaMonto').val(tipoMoneda);
    });


}
