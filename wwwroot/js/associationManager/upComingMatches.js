$(document).ready(function () {
    $('#MatchesTable').DataTable({
        "ajax": {
            "url": "/api/upcoming-matches",
            "type": 'GET',
            "datatype": "json"
        },
        "columns": [
            { "data": "hostClub", "width": "20%" },
            { "data": "guestClub", "width": "20%" },
            { "data": "startTime", "width": "20%" },
            { "data": "endTime", "width": "20%" }
        ],
        "language": {
            "emptyTable": "No Data Found"
        },
        "width": "100%"
    });
});
