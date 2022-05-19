
$('#btnCrearCuenta').attr("disabled", true);

$('.cfiinvisible').attr("disabled", true);
$('.cfisinvisible').attr("disabled", true);


$('#chkTerm').click(function () {
    if (document.getElementById('chkTerm').checked) {
        $('#btnCrearCuenta').removeAttr("disabled");
    } else {
        $('#btnCrearCuenta').attr("disabled", true);
    }
});


function DeleteLogo(Id) {
   
    swal({
        title: "Estas seguro que desea eliminar este logo?",
        text: "Una vez borrado, no puedes recuperar el registro",
        icon: "warning",
        buttons: true,
        dangerMode: true

    }).then((willDelete) => {
        if (willDelete) {

            
            $.ajax({
                type: "GET",
                url: '/Negocios/Negocios/DeleteLogo/'+Id,
                success: function (data) {
                   
                    if (data.success) {
                        /*toastr.success(data.message);*/
                        swal({ title: data.message, icon: "success" });
                        $('.cfiinvisible').removeAttr("disabled");
                        $('#adellogo').text('');
                     
                    } else {
                        /*toastr.error(data.message);*/
                        swal({ title: data.message, icon: "info" });
                    }
                }
            });
        }
    });
}


function DeletePictures(Id) {
   
    swal({
        title: "Estas seguro que desea todas las fotos?",
        text: "Una vez borrado, no puedes recuperar los registros",
        icon: "warning",
        buttons: true,
        dangerMode: true

    }).then((willDelete) => {
        if (willDelete) {

            $.ajax({
                type: "GET",
                url: '/Negocios/Negocios/DeletePictures/' + Id,
                success: function (data) {

                    if (data.success) {
                        /*toastr.success(data.message);*/
                        swal({ title: data.message, icon: "success" });
                        $('.cfisinvisible').removeAttr("disabled");
                        $('#adelpictures').text('');
                      
                    } else {
                        /*toastr.error(data.message);*/
                        swal({ title: data.message, icon: "info" });
                    }
                }
            });
        }
    });
}

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


        if (files.length > 6) {
            $('#galVal').html('La cantidad máxima de imagenes son 6');
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

              
                switch (ext) {
                    case 'jpg':
                    case 'jpeg':
                    case 'png': break;
                    default:

                        this.value = ''; // reset del valor
                        $('#galVal').html('Solo se permiten imagenes, favor intentar de nuevo');
                        fileLabel.html('Selecciona fotos de tus productos/servicios...');
                }

            });
        }




    });

});