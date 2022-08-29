var dataTable;

$(document).ready(function () {
    loadDataTable();
    
});

function loadDataTable() {

    dataTable = $('#tblCupon').DataTable({
        "bSortClasses": false,
        order: [[7, 'asc']],
        "stripeClasses": [],
        "ajax": {
            "url": "/Cuponera/Cuponera/GetCupons"
        },
        "columns": [
            { "data": "nombreCatalogo", "width": "15%", "className": "t" },
            { "data": "nombreMoneda", "width": "15%", "className": "t" },
            { "data": "nombreTipoCatalogo", "width": "15%", "className": "t" },
            { "data": "tipoPagos", "width": "15%", "className": "t" },
            { "data": "nombreDescuentoMasivo", "width": "15%", "className": "t" },
            { "data": "porcentajeDescuentoMasivo", "width": "15%", "className": "t" },
            { "data": "activo", "width": "15%", "className": "t" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <a href="/Cuponera/Cuponera/Add/${data}" class="btn btn-warning text-white" id="btnEditar"><i class="fa fa-edit"></i></a>`

                        ;
                }, "width": "40%"
            }
        ]

    });
}
