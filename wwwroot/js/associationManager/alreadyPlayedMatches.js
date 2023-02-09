$(document).ready(function () {
    $('#MatchesTable').DataTable({
        "ajax": {
            "url": "/api/already-played-matches",
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
            "emptyTable": "No Matches already-played Found"
        },
        "width": "100%"
    });
});
