var dataTable;

$(document).ready(function () {
 
    var idowner = $('#OwnerId').val();
    if (idowner === "-1") {
        loadDataTableAdmin();
    } else {
        loadDataTableBusinessAdmin();
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
            { "data": "email", "width": "10%", "className": "t" },
            { "data": "nombreNegocio", "width": "30%", "className": "t" },
            { "data": "categoryBusinessName", "width": "5%", "className": "t" },
            { "data": "departmentName", "width": "5%", "className": "t" },
            { "data": "createDateString", "width": "5%", "className": "t" },
            {
                "data": {
                    statusName: "statusName", status: "status"
                },
                "render": function (data) {

                    if (data.status == 19) {
                        //user is currently locked
                        return ` 
                             <span style="font-size: medium;" class="badge badge-success">${data.statusName}</span>
                             `

                            ;
                    } else if (data.status == 17) {

                        return ` 
                             <span style="font-size: medium;" class="badge badge-primary">${data.statusName}</span>
                             `

                            ;
                    } else if (data.status == 18) {
                        return ` 
                            <span style="font-size: medium;" class="badge badge-danger">${data.statusName}</span>
                           `

                            ;
                    }

                }, "width": "5%"
            },
          
            {
                "data": {
                    id: "id", status: "status"
                },
                "render": function (data) {
                
                    if (data.status == 19) {
                        //user is currently locked
                        return ` 
                             <a onclick=ManageBusinessActivation('${data.id}'); class="btn btn-danger text-white" style="cursor:pointer;width:180px;" >
                             <i class="fa fa-check"></i> Aprobar</a>`

                            ;
                    } else if (data.status == 17) {

                        return ` 
                             <a onclick=ManageBusinessActivation('${data.id}'); class="btn btn-success text-white" style="cursor:pointer;width:180px;">
                             <i class="fa fa-times"></i> Inactivar</a>`

                            ;
                    } else if (data.status == 18) {
                        return ` 
                            <a onclick=ManageBusinessActivation('${data.id}'); class="btn btn-danger text-white" style="cursor:pointer;width:180px;" >
                             <i class="fa fa-check"></i> Activar</a>`

                            ;
                    }



                }, "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return ` <a href="/Negocios/Negocios/GetDetailByBussinesId/${data}" class="btn btn-primary text-white" id="btnVerDetalle"><i class="fa fa-eye"></i></a>`
                           
                        ;
                }, "width": "10%"
            }
        ]

    });
}


function loadDataTableBusinessAdmin() {
    //tblCategory
    dataTable = $('#tblBusiness').DataTable({
        "bSortClasses": false,
        "stripeClasses": [],
        "ajax": {
            "url": "/Negocios/Negocios/GetBusinessByOwners?idOwner=" + $('#OwnerId').val()
        },
        "columns": [
           
            { "data": "nombreNegocio", "width": "25%", "className": "t" },
            { "data": "categoryBusinessName", "width": "10%", "className": "t" },
            { "data": "departmentName", "width": "15%", "className": "t" },
            { "data": "createDateString", "width": "5%", "className": "t" },
            {
                "data": {
                    statusName: "statusName", status: "status"
                },
                "render": function (data) {

                    if (data.status == 19) {
                        //user is currently locked
                        return ` 
                             <span style="font-size: medium;" class="badge badge-success">${data.statusName}</span>
                             `

                            ;
                    } else if (data.status == 17) {

                        return ` 
                             <span style="font-size: medium;" class="badge badge-primary">${data.statusName}</span>
                             `

                            ;
                    } else if (data.status == 18) {
                        return ` 
                            <span style="font-size: medium;" class="badge badge-danger">${data.statusName}</span>
                           `

                            ;
                    }

                }, "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return ` <a href="/Negocios/Negocios/AgregarNegocio/${data}" class="btn btn-primary text-white" id="btnAgregar"><i class="fa fa-edit"></i></a>
                        <a onclick = DeleteParent("/Catalogos/CatCategory/DeleteParentCat/${data}"); class="btn btn-danger text-white" id = "btnEditar" > <i class="fa fa-trash"></i></a >`
                        ;
                }, "width": "40%"
            }
        ]

    });
}

function ManageBusinessActivation(id) {

    $.ajax({
        type: "POST",
        url: "/Negocios/Negocios/ManageBusinesActivation",
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