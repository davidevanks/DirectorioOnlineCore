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
            "url": "/Ventas/CatalogoProductService/GetLstConfigCat"
        },
        "columns": [
            { "data": "nombreCatalogo", "width": "15%", "className": "t" },
            { "data": "nombreMoneda", "width": "15%", "className": "t" },
            { "data": "nombreTipoCatalogo", "width": "15%", "className": "t" },
            { "data": "nombreTipoPagos", "width": "15%", "className": "t" },
            { "data": "nombreDescuentoMasivo", "width": "15%", "className": "t" },
            { "data": "porcentajeDescuentoMasivo", "width": "5%", "className": "t" },
            { "data": "descripcionActivo", "width": "5%", "className": "t" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <a href="/Ventas/CatalogoProductService/AddUpdCatConfig/${data}" class="btn btn-warning text-white" id="btnEditar"><i class="fa fa-edit"></i></a>
                             <a href="/Ventas/ItemCatalogoProdServ/Index?idConfigCat=${data}" class="btn btn-primary text-white" id="btnEditar"><i class="fa fa-plus"></i></a>`

                        ;
                }, "width": "60%"
            }
        ]

    });
}
