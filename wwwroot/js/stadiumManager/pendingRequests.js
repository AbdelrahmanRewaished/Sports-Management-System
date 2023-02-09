var dataTable;

$(document).ready(function () {
    dataTable = $('#RequestsTable').DataTable({
        "ajax": {
            "url": "/api/pending-requests",
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
                    let acceptUrl = `/api/pending-requests?hostClub=${row.hostClub}&guestClub=${row.guestClub}&startTime=${row.startTime}&accepting=true`;
                    let rejectUrl = `/api/pending-requests?hostClub=${row.hostClub}&guestClub=${row.guestClub}&startTime=${row.startTime}&accepting=false`;
                    return `<div class="text-center">
                                <a class='btn btn-success text-white' style='cursor:pointer; width:80px;'
                                    onclick="handleRequest('${acceptUrl}' , true )" >
                                    Accept
                                </a>
                                &nbsp;
                                <a class='btn btn-danger text-white' style='cursor:pointer; width:80px;'
                                    onclick="handleRequest('${rejectUrl}', false )" >
                                    Reject
                                </a>
                            </div>`;
                },
                "width": "70%"
            }
        ],
        "language": {
            "emptyTable": "No Pending Requests Found"
        },
        "width": "100%"
    });
});

function getAlertMessage(accepting) {
    return accepting ? "Once Accepting, the match will be officially created and its Tickets will be available." :
        "Once Rejecting, this match will not be conducted on your stadium.";
}

function handleRequest(url, status) {
    swal({
        title: "Are you sure?",
        text: getAlertMessage(status),
        icon: "info",
        buttons: true
    }).then((willRespond) => {
        if (willRespond) {
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