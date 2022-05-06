var dataTable;

$(document).ready(function () {
    var idowner = $('#OwnerId').val();
    if (idowner==="") {
        loadDataTableAdmin();
    }
   
});

function loadDataTableAdmin() {
    //tblCategory
    dataTable = $('#tblBusiness').DataTable({
        "bSortClasses": false,
        "stripeClasses": [],
        "ajax": {
            "url": "/Negocios/Negocios/GetBusinessByOwners?idOwner=" + $('#OwnerId').val()
        },
        "columns": [
            { "data": "email", "width": "60%", "className": "t" },
            { "data": "fullname", "width": "60%", "className": "t" },
            { "data": "nombrenegocio", "width": "60%", "className": "t" },
            { "data": "categorybusinessname", "width": "60%", "className": "t" },
            { "data": "departmentname", "width": "60%", "className": "t" },
            { "data": "createdate", "width": "60%", "className": "t" },
            { "data": "statusname", "width": "60%", "className": "t" },
          
            {
                "data": "id",
                "render": function (data) {
                    return ` <a href="/Catalogos/CatCategory/AddCatChild/${data}" class="btn btn-primary text-white" id="btnAgregar"><i class="fa fa-plus"></i></a>
                            <a href="/Catalogos/CatCategory/Upsert/${data}" class="btn btn-warning text-white" id="btnEditar"><i class="fa fa-edit"></i></a>
                             <a onclick=DeleteParent("/Catalogos/CatCategory/DeleteParentCat/${data}"); class="btn btn-danger text-white" id="btnEditar"><i class="fa fa-trash"></i></a>`

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