var dataTable;

$(document).ready(function () {
    dataTable = $('#MatchesTable').DataTable({
        "ajax": {
            "url": "/api/matches",
            "type": 'GET',
            "datatype": "json"
        },
        "columns": [
            { "data": "hostClub", "width": "20%" },
            { "data": "guestClub", "width": "20%" },
            { "data": "startTime", "width": "20%" },
            { "data": "endTime", "width": "20%" },
            {
                "render": function (data, type, row) {
                    let deleteUrl = `/api/matches?hostClub=${row.hostClub}&guestClub=${row.guestClub}&startTime=${row.startTime}`;
                    return `<div class="text-center">
                        <a href="/Dashboards/AssociationManager/MatchList/Edit?hostClub=${row.hostClub}&guestClub=${row.guestClub}&startTime=${row.startTime}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete('${deleteUrl}')>
                            Delete
                        </a>
                        </div>`;
                },
                "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "No Matches Found"
        },
        "width": "100%"
    });
});


function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will lose also all the tickets related to this match. You will not be able to recover",
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