$(document).ready(function () {


  
    //si estamos en modo edición
    var esUpdate = $('#Id').val();

    if (esUpdate != 0) {
        console.log('es update');
  
        if ($('#DescuentoMasivo').val() == 'True') {
            $('#divMtoDctoMasivo').show();
           

        } else {
            $('#divMtoDctoMasivo').hide();
        }
    }
    //fin

    $('input[name=TypeCupon]').on('change', function () {

        var isTypePorcent = $('#chkboxDescMasivo').val();
        console.log(isTypePorcent);
        if (isTypePorcent === "False") {
           
            $('#divMtoDctoMasivo').hide();
        }
        else {
          
            $('#divMtoDctoMasivo').show();
        }

    });


    checkTypeCatalogo();
    chechTypeMoneda();

});


function chechTypeMoneda(){
    $('#cboIdMoneda').val($('#IdMoneda').val());

    $('#cboIdMoneda').on('change', function () {

      var  tipoMoneda = $('#cboIdMoneda').val();
        $('#IdMoneda').val(tipoMoneda);
    });
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
