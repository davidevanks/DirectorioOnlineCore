var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    //tblCategory
    dataTable = $('#tblCategory').DataTable({
        "bSortClasses": false,
        "stripeClasses": [],
        "ajax": {
            "url": "/Catalogos/CatCategory/GetAllChildCategory?idPadre=" + $('#hdfIdPadre').val()
          
        },
        "columns": [
            { "data": "nombre", "width": "60%", "className": "t" },
            {
                "data": "activo", "render": function (data) {
                    if (data)
                        return `Activo`;
                    else
                        return `Inactivo`;

                }, "width": "20%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return ` 
                            <a href="/Catalogos/CatCategory/UpsertChild/${data}" class="btn btn-warning text-white" id="btnEditar"><i class="fa fa-edit"></i></a>
                             `

                        ;
                }, "width": "40%"
            }
        ]

    });
}

function DeleteParent(url) {
    swal({
        title: "Estas seguro que desea borrar esta categoría?",
        text: "Una vez borrado, no puedes recuperar el registro",
        icon: "warning",
        buttons: true,
        dangerMode: true

    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
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
    });
}