$('#btnSendMessage').click(function () {

    if ($('#frmContactUs').valid() == false) {

 
        $('#frmContactUs').submit(function (e) {
            e.preventDefault();
          /*  swal({ title: "Favor llenar todos los campos requeridos", icon: "info" });*/
        });

    } else {
        console.log('entreee');
        $('#frmContactUs').submit(function (e) {
            e.preventDefault();
        });
        
        
        var model = { Subject: $('#txtSubject').val(), CompanyName: $('#txtCompanyName').val(), PersonName: $('#txtPersonName').val(), Email: $('#txtEmail').val(), Phone: $('#txtPhone').val(), Message: $('#txtMessage').val() };

        $.ajax({
            type: "POST",
            url: "/Home/Home/SendMessageContactUs",
            data: JSON.stringify(model),
            contentType: "application/json",
            success: function (data) {
                if (data.success) {
                  
                    swal({ title: data.message, icon: "info" });
                    $('#txtSubject').val('');
                    $('#txtCompanyName').val('');
                    $('#txtPersonName').val('');
                    $('#txtEmail').val('');
                    $('#txtPhone').val('');
                    $('#txtMessage').val('');


                } else {
                  
                     swal({ title: data.message, icon: "info" });
                }
            }
        });

    }

});