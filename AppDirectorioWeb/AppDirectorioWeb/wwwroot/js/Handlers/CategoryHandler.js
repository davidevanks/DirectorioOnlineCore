var dataTable;

$(document).ready(function() {
    loadDataTable();
});

function loadDataTable() {
//tblCategory
    dataTable = $('#tblCategory').DataTable({
        "ajax": {
            "url": "/Catalogos/CatCategory/GetAllParents"
        },
        "columns": [
            { "data": "nombre", "width": "60%" },
            {
                "data": "id",
                "render": function(data) {
                    return ` <a href="/Catalogos/CatCategory/AddCat/${data}" class="btn btn-primary text-white" id="btnAgregar"><i class="fa fa-plus"></i></a>
                            <a href="/Catalogos/CatCategory/Update/${data}" class="btn btn-warning text-white" id="btnEditar"><i class="fa fa-edit"></i></a> `
                 
                    ;
                },"width":"40%"
            }
            ]

    });
}