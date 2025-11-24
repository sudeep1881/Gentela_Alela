let dataTable;

$(function () {
    loadDataTable();
    DeleteHandlerRoleList("/Admin/RoleListDeleteMethod?id=", dataTable);
})

function DeleteHandlerRoleList(url, dataTable) {
    const Table = document.querySelector("#dataTable");
    Table.addEventListener("click", (e) => {
        let id = e.target.dataset.deleteId ?? e.target.parentElement.dataset.deleteId;
        if (id) {
            DeleteHandlerHoverRoleList(`${url}${id}`, dataTable);
        }
    })
}

function DeleteHandlerHoverRoleList(url, dataTable) {
    Swal.fire({
        title: "Are You Sure!!!",
        text: "You Won't Return this Data",
        icon: "warning",
        showCancelButton: true,
        cancelButtonColor: "red",
         
        confirmButtonColor: "blue",
        confirmButtonText:"yes! Delete it"
    }).then(async (res) => {
        if (res.isConfirmed) {
            try {
                const { success, message } = await fetch(url, { method: "DELETE" }).then(s => s.json());
                if (success) {
                    dataTable.ajax.reload();
                    toastr.success(message);
                }
                else {
                    toastr.error(message);
                }
            }
            catch (e) {
                console.log("Network issue", e);
            }

        }
    })
}

function loadDataTable()
{
    dataTable = $("#dataTable").DataTable({
        ajax: {
            url: "/Admin/RoleListFetchMethod",
            type: "POST",

        },
        columns: [
            { data: "id" },
            { data: "roleName" },
            {
                data: "id",
                width: "25%",
                render: function (data) {
                    return `<div>
    <a href="/Admin/Role?id=${data}" class="btn btn-soft-info  ">
        <i class="bx bx-edit"></i>
    </a>
    <a   class="btn btn-soft-danger   delete-btn" data-delete-id="${data}">
        <i class="bx bxs-trash"></i>
    </a>
    </div>`;
                }
            }
        ]
    })
}