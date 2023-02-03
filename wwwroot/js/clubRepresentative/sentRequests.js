var dataTable;


$(document).ready(function () {
    dataTable = $('#RequestsTable').DataTable({
        ajax: {
            "url": "/api/sent-requests",
            "type": 'GET',
            "datatype": "json"
        },
        columns: [
            { "data": "hostClub", "width": "20%" },
            { "data": "guestClub", "width": "20%" },
            { "data": "startTime", "width": "20%" },
            { "data": "stadium", "width": "20%" },
            {
                "data":"status",
                "render": function (data) {
                    return `<div class="text-center" style='background-color:${getStatusCellColor(data)};'>
                                ${data}
                            </div>`;
                },
                "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "No Data Found"
        },
        "width": "100%"
    });
});


function getStatusCellColor(status) {
    if (status === "unhandled") {
        return "aqua";
    }
    else if (status === "rejected") {
        return "red";
    }
    return "green";
}

