var dataTable;

$(document).ready(function () {
    dataTable = $('#MatchesTable').DataTable({
        "ajax": {
            "url": "/api/available-matches",
            "type": 'GET',
            "datatype": "json"
        },
        "columns": [
            { "data": "hostClub", "width": "15%" },
            { "data": "guestClub", "width": "15%" },
            { "data": "startTime", "width": "20%" },
            { "data": "stadium", "width": "15%" },
            { "data": "location", "width": "15%" },
            {
                "render": function (data, type, row) {
                    let url = `/api/available-matches?hostClub=${row.hostClub}&guestClub=${row.guestClub}&startTime=${row.startTime}`;
                    return `<div class="text-center">
                        <a class='btn btn-success text-white' style='cursor:pointer; width:120px;'
                            onclick="Purchase('${url}')">
                            Purchase
                        </a>
                        </div>`;
                },
                "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "No Upcoming Matches Yet"
        },
        "width": "100%"
    });
});


function Purchase(url) {
    swal({
        title: "Are you sure?",
        text: "A Ticket for this match will be reserved for You",
        icon: "info",
        buttons: true
    }).then((willPurchase) => {
        if (willPurchase) {
            $.ajax({
                type: "POST",
                url: url,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
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