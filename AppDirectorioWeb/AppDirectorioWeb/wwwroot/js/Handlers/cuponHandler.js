var dataTable;

$(document).ready(function () {
    loadDataTable();
    checkTypeCupon();
});


function checkTypeCupon() {

//TypeCupon
    console.log('TypeCupon');
}

function loadDataTable() {

    dataTable = $('#tblCupon').DataTable({
        "bSortClasses": false,
        "stripeClasses": [],
        "ajax": {
            "url": "/Cuponera/Cuponera/GetCupons"
        },
        "columns": [
            { "data": "id", "width": "15%", "className": "t" },
            { "data": "descripcionPromocion", "width": "15%", "className": "t" },
            { "data": "tipoDescuento", "width": "15%", "className": "t" },
            { "data": "valorCupon", "width": "15%", "className": "t" },
            { "data": "cantidadCuponDisponible", "width": "15%", "className": "t" },
            { "data": "cantidadCuponUsados", "width": "15%", "className": "t" },
            { "data": "FechaExpiracionCupon", "width": "15%", "className": "t" },
            { "data": "statusDescripcion", "width": "15%", "className": "t" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <a href="/Catalogos/CatCategory/Upsert/${data}" class="btn btn-warning text-white" id="btnEditar"><i class="fa fa-edit"></i></a>`

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