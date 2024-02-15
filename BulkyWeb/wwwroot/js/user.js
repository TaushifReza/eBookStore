var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/user/getall',
        },
        "columns": [
            { data: 'name', "width": "15%" },
            { data: 'email', "width": "15%" },
            { data: 'phoneNumber', "width": "15%" },
            { data: 'company.name', "width": "15%" },
            { data: 'role', "width": "15%" },
            {
                data: { id: 'id', lockoutEnd: 'lockoutEnd' },
                "render": function(data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    if (lockout > today) {
                        return `
                        <div class="text-center btn-group">
                            <a onclick=LockUnlock('${data.id}') class="btn btn-danger text-white" 
                                style="cursor: pointed;">
                                Lock
                            </a>
                            <a href="/admin/user/RoleManagment?userId=${data.id}" class="btn btn-danger text-white" style="cursor: pointed;">
                                Permission
                            </a>
                        </div>
                        `;
                    }
                    else {
                        return `
                        <div class="text-center btn-group">
                            <a onclick=LockUnlock('${data.id}') class="btn btn-success text-white" 
                                style="cursor: pointed;">
                                UnLock
                            </a>
                            <a href="/admin/user/RoleManagment?userId=${data.id}" class="btn btn-danger text-white" style="cursor: pointed;">
                                Permission
                            </a>
                        </div>
                        `;
                    }
                },
                "width": "25%"
            }
        ]
    });
}

function LockUnlock(id) {
    $.ajax({
        type: 'POST',
        url: '/Admin/User/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function(data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
        }
    });

}