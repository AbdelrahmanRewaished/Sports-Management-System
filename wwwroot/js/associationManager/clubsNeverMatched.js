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
            "emptyTable": "No Data Found"
        },
        "width": "100%"
    });
});
