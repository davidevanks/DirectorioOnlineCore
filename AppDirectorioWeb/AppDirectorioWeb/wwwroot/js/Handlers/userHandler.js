var dataTable;

$(document).ready(function() {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#tblUser').DataTable({
        "bSortClasses": false,
        "stripeClasses": [],
        "ajax": {
            "url": "/Security/User/GetAll"
        },
        "columns": [
            { "data": "fullName", "width": "15%", "className": "t" },
            { "data": "email", "width": "15%", "className": "t" },
            { "data": "phoneNumber", "width": "15%", "className": "t" },
            { "data": "role", "width": "15%", "className": "t" },
            { "data": "subscripcion", "width": "15%", "className": "t" },
            { "data": "planExpirationDate", "width": "15%", "className": "t" },
            {
                "data": {
                    id: "id", lockoutEnd:"lockoutEnd"
                    },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        //user is currently locked
                        return ` 
                             <a onclick=LockUnlock('${data.id}'); class="btn btn-danger text-white" style="cursor:pointer;width:200px;" >
                             <i class="fa fa-lock"></i> Activar</a>`

                            ;
                    } else {

                        return ` 
                             <a onclick=LockUnlock('${data.id}'); class="btn btn-success text-white" style="cursor:pointer;width:200px;">
                             <i class="fa fa-unlock"></i> Desactivar</a>`
                        
                            ;
                    }

                  
                },"width":"25%"
            }
            ]

    });
}

function LockUnlock(id) {
   
            $.ajax({
                type: "POST",
                url: "/Security/User/LockUnlock",
                data: JSON.stringify(id),
                contentType:"application/json",
                success: function(data) {
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