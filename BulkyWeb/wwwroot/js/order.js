var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/order/getall',
        },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "30%" },
            { data: 'phoneNumber', "width": "20%" },
            { data: 'applicationUser.email', "width": "10%" },
            { data: 'orderStatus', "width": "15%" },
            { data: 'orderTotal', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/order/details?orderId=${data}"
                     class="btn btn-primary mx-2">Edit</a>               
                    </div>`
                },
                "width": "15%"
            },
        ]
    });
}