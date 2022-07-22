var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#tblFactura').DataTable({
        "bSortClasses": false,
        "stripeClasses": [],
        "ajax": {
            "url": "/Ventas/Facturacion/GetInvoices"
        },
        "columns": [
            { "data": "idFactura", "width": "15%", "className": "t" },
            { "data": "user", "width": "15%", "className": "t" },
            { "data": "planSuscripcion", "width": "15%", "className": "t" },
            { "data": "noAutorizacion", "width": "15%", "className": "t" },
            { "data": "fechaPago", "width": "15%", "className": "t" },
            {
                "data": {
                    facturaPagada: "facturaPagada"
                },
                "render": function (data) {
                    var fp = data.facturaPagada;
                    if (fp == true) {
                        //user is currently locked
                        return ` 
                             <a  class="btn btn-primary text-white" style="cursor:pointer;width:80px;" >
                              Si</a>`

                            ;
                    } else {

                        return ` 
                              <a  class="btn btn-danger text-white" style="cursor:pointer;width:80px;" >
                              No</a>`

                            ;
                    }


                }, "width": "15%"
            },
            {
                "data": {
                    facturaEnviada: "facturaEnviada"
                },
                "render": function (data) {
                    var fp = data.facturaEnviada;
                    if (fp == true) {
                       
                        return ` 
                             <a  class="btn btn-primary text-white" style="cursor:pointer;width:80px;" >
                              Si</a>`

                            ;
                    } else {

                        return ` 
                              <a  class="btn btn-danger text-white" style="cursor:pointer;width:80px;" >
                              No</a>`

                            ;
                    }


                }, "width": "15%"
            },
            {
             
                "data": {
                    idFactura: "idFactura", facturaPagada: "facturaPagada", facturaEnviada: "facturaEnviada"
                },
                "render": function (data) {
                    var fp = data.facturaPagada;
                    var ef = data.facturaEnviada;
                    if (fp == false || ef == false) {
                        return ` 
                            <a href="/Ventas/Facturacion/UpInvoice/${data.idFactura}" class="btn btn-warning text-white" id="btnEditar"><i class="fa fa-edit"></i></a>`
                    } else
                    {
                        return ` 
                            `
                    }
                    

                        ;
                }, "width": "40%"
            }
          
        ]

    });
}
