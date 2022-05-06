////$('.ti').inputmask({
////    alias: "datetime",
////    placeholder: "12:00 AM",
////    inputFormat: "hh:MM TT",
////    insertMode: false,
////    showMaskOnHover: false,
////    hourFormat: 12
////});




$('#btnCrearCuenta').attr("disabled", true);

$('#chkTerm').click(function () {
    if (document.getElementById('chkTerm').checked) {
        $(":submit").removeAttr("disabled");
    } else {
        $('#btnCrearCuenta').attr("disabled", true);
    }
});

$(document).ready(function () {

  
    $('.cfi').on("change", function () {
        $('#logoVal').html('');
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
                $('#logoVal').html('El archivo no tiene la extensión adecuada, solo se aceptan imagenes');
                // alert('El archivo no tiene la extensión adecuada');
                this.value = ''; // reset del valor
                $(this).next('.cfl').html('Selecciona el logo de tu negocio...');
        }
    });


    $('.cfis').on("change", function () {
        $('#galVal').html('');
        var fileLabel = $(this).next('.cfls');
        var files = $(this)[0].files;


        if (files.length > 5) {
            $('#galVal').html('La cantidad máxima de imagenes son 5');
            fileLabel.html('Selecciona fotos de tus productos/servicios...');
            this.value = '';
        } else {






            if (files.length > 1) {
                fileLabel.html(files.length + ' imagenes seleccionadas')
            } else if (files.length == 1) {
                fileLabel.html(files[0].name);
            }

            $.each(files, function (k, v) {

                // recuperamos la extensión del archivo
                var ext = v.name.split('.').pop();

                // Convertimos en minúscula porque 
                // la extensión del archivo puede estar en mayúscula
                ext = ext.toLowerCase();

                // console.log(ext);
                switch (ext) {
                    case 'jpg':
                    case 'jpeg':
                    case 'png': break;
                    default:

                        // alert('El archivo no tiene la extensión adecuada');
                        this.value = ''; // reset del valor
                        $('#galVal').html('Solo se permiten imagenes, favor intentar de nuevo');
                        fileLabel.html('Selecciona fotos de tus productos/servicios...');
                }

            });
        }




    });

});