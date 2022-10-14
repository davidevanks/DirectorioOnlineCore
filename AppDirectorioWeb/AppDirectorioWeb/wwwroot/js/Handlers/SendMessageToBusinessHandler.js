
$('#sendMessageBtn').click(function () {

    const el = document.createElement('div')
    el.innerHTML = "<h3 class='mb - 3 font - weight - normal'>Contactar a propietario</h3>"+
        "<form id='frmSendMessageToBusiness' class='mb-6' >"+
        "<div class='form-group mb-6'>"+
        "<input id='nameInputSender' type='text' class='form-control' placeholder='Tu nombre' required>"+
        " </div>"+
      
            "<div class='form-group mb-6'>"+
            "<input id='celInputSender' type='tel' class='form-control' placeholder='Tu celular' required>"+
        "</div>" +
        "<div class='form-group mb-6'>" +
        "<input id='EmailInputSender' type='email' class='form-control' placeholder='Tu correo' required>" +
        "</div>" +
            " <div class='form-group mb-6'>"+
                        "<textarea id='messageInputSender' class='form-control' rows='3' placeholder='Tu mensaje' required></textarea>"+
                        " </div>"+
                  /*  "<button  id='btnSendMessageToBusiness' class='btn btn-primary'>Enviar</button>"+*/
        "</form>"

    swal({
        buttons: true,
        content: el
    }).then((send) => {
       

        if (send) {

            var model = { BusinessId: $('#IBusinessId').val(), PersonName: $('#nameInputSender').val(), Phone: $('#celInputSender').val(), Email: $('#EmailInputSender').val(), Message: $('#messageInputSender').val() };
            console.log(model);

            $.ajax({
                type: "POST",
                url: "/Home/Home/SendMessageToOwner",
                data: JSON.stringify(model),
                contentType: "application/json",
                success: function (data) {
                    if (data.success) {

                        swal({ title: data.message, icon: "info" });
               


                    } else {

                        swal({ title: data.message, icon: "info" });
                    }
                }
            });
        } 
    });

});

