var dataTable;

$(document).ready(function () {
    dataTable = $('#StadiumsTable').DataTable({
        "ajax": {
            "url": "/api/available-stadiums",
            "type": 'GET',
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "location", "width": "20%" },
            { "data": "capacity", "width": "20%" },
            {
                "data": "name",
                "render": function (data) {
                    return `<div class="text-center">
                       
                        <a class='btn btn-info text-white' style='cursor:pointer; width:70px;'
                            onclick=SendRequest()>
                            Send Request
                        </a>
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


function SendRequest() {
    swal({
        title: "Are you sure?",
        text: "Once sent, the request will directly be available to the manager of the stadium.",
        icon: "info",
        buttons: true
    });
}