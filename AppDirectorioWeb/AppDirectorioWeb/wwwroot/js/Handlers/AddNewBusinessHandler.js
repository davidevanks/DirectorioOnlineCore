﻿var maxImageValidPerPlan = 0;
var valorPlanType = $('#inputPlanType').val();
var typeUserMode = $('#inputTypeUserMode').val();

if (typeUserMode === "n") {
    $("#cboPlanSus option[value=" + valorPlanType + "]").attr("selected", true);
    $('#cboPlanSus').change(function () {
        $('#inputPlanType').val($(this).val());


        if ($('#inputPlanType').val() == 1) {
            maxImageValidPerPlan = 5;
        }
        else if ($('#inputPlanType').val() == 2) {
            maxImageValidPerPlan = 30;
        }
    });
} 




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

    $('#btnCopiarLink').click(function () {
        // Crea un campo de texto "oculto"
        var aux = document.createElement("input");

        // Asigna el contenido del elemento especificado al valor del campo
        aux.setAttribute("value", "www.brujulapyme.com/" + $('#Business_PersonalUrl').val());

        // Añade el campo a la página
        document.body.appendChild(aux);

        // Selecciona el contenido del campo
        aux.select();

        // Copia el texto seleccionado
        document.execCommand("copy");

        // Elimina el campo de la página
        document.body.removeChild(aux);

        alert('Link copiado al portapapeles!');

    });

    //validar url personaliada si existe
    $('#Business_PersonalUrl').change(function () {

        $.ajax({
            type: "GET",
            url: '/Negocios/Negocios/VerifyUrlPersonalExist?personalUrl=' + $('#Business_PersonalUrl').val(),
            success: function (data) {
                console.log('gfgfgfgf');
                console.log(data);
                if (data.success) {
                    $('#PersonalUrlMessageError').text('');

                } else {
                    $('#Business_PersonalUrl').val('');
                    $('#Business_PersonalUrl').focus();
                    $('#PersonalUrlMessageError').text(data.message);
                }
            }
        });
        
      

    });
 

    if ($('#inputPlanType').val() == 1) {
        maxImageValidPerPlan = 5;
    }
    else if ($('#inputPlanType').val() == 2)
    {
        maxImageValidPerPlan = 30;
    }
    


    $('input.timepicker').timepicker({});
  
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


        if (files.length > maxImageValidPerPlan) {
            $('#galVal').html('La cantidad máxima de imagenes son ' + maxImageValidPerPlan);
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