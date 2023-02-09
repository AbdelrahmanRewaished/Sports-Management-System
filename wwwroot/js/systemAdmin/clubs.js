var dataTable;

$(document).ready(function () {
    dataTable = $('#ClubsTable').DataTable({
        "ajax": {
            "url": "/api/clubs",
            "type": 'GET',
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "location", "width": "20%" },
            {
                "data": "clubId",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Dashboards/SystemAdmin/ClubsList/Edit?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick="Delete('/api/clubs?clubId='+${data})">
                            Delete
                        </a>
                        </div>`;
                },
                "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "No Clubs Found"
        },
        "width": "100%"
    });
});


function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will lose also all the reprsentative and matches related to this club. You will not be able to recover",
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
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}