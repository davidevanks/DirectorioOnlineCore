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
            { "data": "user", "width": "15%", "className": "t" },
            { "data": "planSuscripcion", "width": "15%", "className": "t" },
            { "data": "noAutorizacion", "width": "15%", "className": "t" },
            { "data": "fechaPago", "width": "15%", "className": "t" },
            { "data": "facturaPagada", "width": "15%", "className": "t" },
            { "data": "facturaEnviada", "width": "15%", "className": "t" },
            //{
            //    "data": {
            //        id: "id", lockoutEnd: "lockoutEnd"
            //    },
            //    "render": function (data) {
            //        var today = new Date().getTime();
            //        var lockout = new Date(data.lockoutEnd).getTime();
            //        if (lockout > today) {
            //            //user is currently locked
            //            return ` 
            //                 <a onclick=LockUnlock('${data.id}'); class="btn btn-danger text-white" style="cursor:pointer;width:200px;" >
            //                 <i class="fa fa-lock"></i> Activar</a>`

            //                ;
            //        } else {

            //            return ` 
            //                 <a onclick=LockUnlock('${data.id}'); class="btn btn-success text-white" style="cursor:pointer;width:200px;">
            //                 <i class="fa fa-unlock"></i> Desactivar</a>`

            //                ;
            //        }


            //    }, "width": "25%"
            //}
        ]

    });
}
