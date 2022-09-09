$(document).ready(function () {

    checkIsDescuentoMasivo();


    $('#cboIdTipoCat').on('change', function () {

        var tipoSer = $('#cboIdTipoCat').val();
        $('#IdTipoCatalogo').val(tipoSer);
    });

    $('#cboIdMoneda').on('change', function () {

        var tipoMoneda = $('#cboIdMoneda').val();
        $('#IdMoneda').val(tipoMoneda);
    });

  
    //si estamos en modo edición
    var esUpdate = $('#Id').val();

    if (esUpdate != 0) {
        console.log('es update');
  
        if ($('input[name=DescuentoMasivo]').is(':checked') == true) {
            $('#divMtoDctoMasivo').show();
            $("#PorcentajeDescuentoMasivo").attr("required", true);

        } else {
            $('#divMtoDctoMasivo').hide();
            $("#PorcentajeDescuentoMasivo").attr("required", false);
        }

        checkTypeCatalogo();
        chechTypeMoneda();
      
    }
    //fin

    $('input[name=DescuentoMasivo]').on('change', function () {

        var isTypePorcent = $('input[name=DescuentoMasivo]').is(':checked') ;
        console.log(isTypePorcent);
        if (isTypePorcent == false) {
           
            $('#divMtoDctoMasivo').hide();
            $("#PorcentajeDescuentoMasivo").attr("required", false);
        }
        else {
          
            $('#divMtoDctoMasivo').show();
            $("#PorcentajeDescuentoMasivo").attr("required", true);
        }

    });



});


function chechTypeMoneda(){
    $('#cboIdMoneda').val($('#IdMoneda').val());

    $('#cboIdMoneda').on('change', function () {

      var  tipoMoneda = $('#cboIdMoneda').val();
        $('#IdMoneda').val(tipoMoneda);
    });
}

function checkIsDescuentoMasivo()
{
    var isTypePorcent = $('input[name=DescuentoMasivo]').is(':checked');
    console.log(isTypePorcent);
    if (isTypePorcent == false) {

        $('#divMtoDctoMasivo').hide();
        $("#PorcentajeDescuentoMasivo").attr("required", false);
    }
    else {

        $('#divMtoDctoMasivo').show();
        $("#PorcentajeDescuentoMasivo").attr("required", true);
    }
}
function checkTypeCatalogo() {


    //TypeCupon,DescuentoMonto
    var isType = $('#IdTipoCatalogo').val();
    console.log(isType);

    $('#cboIdTipoCat').val(isType);
  
    $('#cboIdTipoCat').on('change', function () {

        var tipoSer = $('#cboIdTipoCat').val();
        $('#IdTipoCatalogo').val(tipoSer);
    });



}
