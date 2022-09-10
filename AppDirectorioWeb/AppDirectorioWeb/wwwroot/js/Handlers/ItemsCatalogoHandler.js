var dataTable;

$(document).ready(function () {
    loadDataTable();
 
});




function loadDataTable() {

    dataTable = $('#tblItems').DataTable({
        "bSortClasses": false,
        order: [[7, 'asc']],
        "stripeClasses": [],
        "ajax": {
            "url": "/Ventas/ItemCatalogoProdServ/GetItemsCat?idConfigCat=" + $('#idConfigCat').val()
        },
        "columns": [
            { "data": "codigoRef", "width": "15%", "className": "t" },
            { "data": "nombreItem", "width": "15%", "className": "t" },
            { "data": "descripcionTieneDescuento", "width": "15%", "className": "t" },
            { "data": "porcentajeDescuento", "width": "15%", "className": "t" },
            { "data": "nombreMoneda", "width": "5%", "className": "t" },
            { "data": "precioUnitario", "width": "15%", "className": "t" },
            { "data": "descripcionActivo", "width": "5%", "className": "t" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <a href="/Ventas/ItemCatalogoProdServ/AddUpdItemCat/${data}" class="btn btn-warning text-white" id="btnEditar"><i class="fa fa-edit"></i></a>
 <a target="_blank" href="/Ventas/ItemCatalogoProdServ/GetDetailsRead/${data}" class="btn btn-primary text-white" id="btnVerDetalle"><i class="fa fa-eye"></i></a>`

                        ;
                }, "width": "40%"
            }
        ]

    });
}

function LockUnlock(id) {

    $.ajax({
        type: "POST",
        url: "/Security/User/LockUnlock",
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                /*toastr.success(data.message);*/
                swal({ title: data.message, icon: "success" });
                dataTable.ajax.reload();
            } else {
                /*toastr.error(data.message);*/
                swal({ title: data.message, icon: "info" });
            }
        }
    });

}