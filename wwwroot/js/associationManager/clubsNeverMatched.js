$(document).ready(function () {
    $('#ClubsTable').DataTable({
        "ajax": {
            "url": "/api/clubs-never-matched",
            "type": 'GET',
            "datatype": "json"
        },
        "columns": [
            { "data": "club1Name", "width": "20%" },
            { "data": "club2Name", "width": "20%" }
        ],
        "language": {
            "emptyTable": "Every Club is Matched Until Now"
        },
        "width": "100%"
    });
});
