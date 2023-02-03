$(document).ready(function () {
    $('#TicketsTable').DataTable({
        "ajax": {
            "url": "/api/purchased-tickets",
            "type": 'GET',
            "datatype": "json"
        },
        "columns": [
            { "data": "hostClub", "width": "15%" },
            { "data": "guestClub", "width": "15%" },
            { "data": "startTime", "width": "20%" },
            { "data": "stadium", "width": "15%" },
            { "data": "location", "width": "15%" },
            { "data": "tickets", "width": "20%" }
        ],
        "language": {
            "emptyTable": "No Data Found"
        },
        "width": "100%"
    });
});
