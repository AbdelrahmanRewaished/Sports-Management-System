var dataTable;


$(document).ready(function () {
    dataTable = $('#FansTable').DataTable({
        ajax: {
            "url": "/api/fans",
            "type": 'GET',
            "datatype": "json"
        },
        columns: [
            { data: "name", "width": "20%" },
            { data: "username", "width": "20%" },
            { data: "phoneNo", "width": "20%" },
            {
                "data": "nationalId",
                "render": function (data, type, row) {
                    return `<div class="text-center">
                                ${getActionStatusButton(data, row.status)}
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


function getActionStatusButton(id, status) {
    let url = `/api/fans?nationalId=${id}&status=${status}`;
    let statusButtonAction = `<a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick="updateStatus('${url}', ${status})">
                            Block
                        </a>`;
    if (!status) {
        statusButtonAction = `<a class='btn btn-success text-white' style='cursor:pointer; width:90px;'
                            onclick="updateStatus('${url}', ${status})">
                            UnBlock
                        </a>`;
    }
    return statusButtonAction;
}

function getStatusMessage(status) {
    let message = "Once blocked, user will not be able to access the system";
    if (!status) {
        message = "Once unblocked, user will be able to access the system again";
    }
    return message
}

function updateStatus(url, status) {
    swal({
        title: "Are you sure?",
        text: getStatusMessage(status),
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willUpdate) => {
        if (willUpdate) {
            $.ajax({
                type: "PATCH",
                url: url,
                data: JSON.stringify({ status: ! status }),
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
