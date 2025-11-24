let dataTable;

$(function () {
    loadDataTable();
    DeleteHandler("/Citizine/HomePageDeleteMethod?id=", dataTable);
})

function DeleteHandler(url, dataTable) {
    const table = document.querySelector("#dataTable");
    table.addEventListener("click", (e) => {
        let id = e.target.dataset.deleteId ?? e.target.parentElement.dataset.deleteId;
        if (id) {
            DeleteHandlerHover(`${url}${id}`, dataTable);
        }
    })
}

function DeleteHandlerHover(url, dataTable) {
    Swal.fire({
        title: "Are You Sure?!",
        text: "You Wont return this Data",
        icon: "warning",
        showCancelButton: true,
        cancelButtonColor: "red",
        confirmButtonColor: "Green",
        confirmButtonText: "Yes! Delete it"
    }).then(async (result) => {
        if (result.isConfirmed) {
            try {
                const { success, message } = await fetch(url, { method:"DELETE" }).then(s => s.json());
                if (success) {
                    dataTable.ajax.reload();
                    toastr.success(message);
                }
                else {
                    toastr.error(message);
                }
            }
            catch (e) {
                console.log("Network Issue", e);
            }
        }
    })
}

function loadDataTable() {
    dataTable = $("#dataTable").DataTable({
        ajax: {
            url: "/Citizine/HomePageFetchMethod",
            type: "POST"
        },
        columns: [
            { data: "id" },
            { data: "name" },
            { data: "dob" },
            { data: "gender" },

            { data: "email" },
            { data: "password" },
            {
                data: "profileImage",
                width:"150px",
                orderable: false,
                render: function (data) {
                    return `<a class="image-popup" href="${data}" >
<img src="${data}"  alt = "profile Img" class="img-fluid avatar-md img-thumbnail" style="box-shadow: 0 0 6px #AFC5E7;"></img>
</a>`;
                }
            },
            {
                data: "id",
                width: "25%",
                render: function (data) {
                    return `<div>
    <a href="/Citizine/Registration?id=${data}" class="btn btn-soft-info  ">
        <i class="bx bx-edit"></i>
    </a>
    <a   class="btn btn-soft-danger   delete-btn" data-delete-id="${data}">
        <i class="bx bxs-trash"></i>
    </a>
    </div>`;
                }
            },
        ]
    })
}