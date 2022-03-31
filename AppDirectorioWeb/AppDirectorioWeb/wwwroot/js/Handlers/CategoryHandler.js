var dataTable;

$(document).ready(function() {
    loadDataTable();
});

function loadDataTable() {
//tblCategory
    dataTable = $('#tblCategory').DataTable({
        "bSortClasses": false,
        "stripeClasses": [],
        "ajax": {
            "url": "/Catalogos/CatCategory/GetAllParents"
        },
        "columns": [
            { "data": "nombre", "width": "60%", "className": "t" },
            { "data": "activo", "render": function(data) {
                if (data)
                    return `Activo`;
                else
                    return `Inactivo`;

            }, "width": "20%"},
            {
                "data": "id",
                "render": function(data) {
                    return ` <a href="/Catalogos/CatCategory/AddCat/${data}" class="btn btn-primary text-white" id="btnAgregar"><i class="fa fa-plus"></i></a>
                            <a href="/Catalogos/CatCategory/Upsert/${data}" class="btn btn-warning text-white" id="btnEditar"><i class="fa fa-edit"></i></a> `
                 
                    ;
                },"width":"40%"
            }
            ]

    });
}