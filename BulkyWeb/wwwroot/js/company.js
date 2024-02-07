var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/company/getall',
        },
        "columns": [
            { data: 'name', "width": "25%" },
            { data: 'streetAddress', "width": "15%" },
            { data: 'city', "width": "20%" },
            { data: 'state', "width": "10%" },
            { data: 'phoneNumber', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/company/UpsertCompany?id=${data}"
                     class="btn btn-primary mx-2">Edit</a>               
                     <a onClick=Delete('/admin/company/Delete/${data}') 
                     class="btn btn-danger mx-2">Delete</a>
                    </div>`
                },
                "width": "15%"
            },
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.success);
                }
            })
        }
    });
}