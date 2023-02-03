var dataTable;


$(document).ready(function () {
    dataTable = $('#MatchesTable').DataTable({
        ajax: {
            "url": "/api/club-upcoming-matches",
            "type": 'GET',
            "datatype": "json"
        },
        columns: [
            { "data": "hostClub", "width": "20%" },
            { "data": "guestClub", "width": "20%" },
            { "data": "startTime", "width": "20%" },
            { "data": "endTime", "width": "20%" },
            {
                "render": function (data, type, row) {
                    return `<div class="text-center">
                                ${getActionStatusButton(row)}
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


function getActionStatusButton(row) {
    if (!row.isHostable) {
        return ``;
    }

    let url = `StadiumsList?hostClub=${row.hostClub}&guestClub=${row.guestClub}&startTime=${row.startTime}`;
    return `<a href="${url}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                    Host Match
            </a>`;
}

/*function getAvailableStadiums(url) {
    $.ajax({
        type: "POST",
        url: url,
        success: function (data) {
            window.location.href = "../../../Dashboards/ClubRepresentative/StadiumsList";
        }
    });
}*/