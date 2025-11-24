let dataTable;

$(function () {
    loadDataTable();
    DeleteHandlerPersonalDetails("/Admin/DeleteHandlerPersonalList?id=");
});

function DeleteHandlerPersonalDetails(url) {

    const table = document.querySelector("#dataTable");

    table.addEventListener("click", (e) => {
        let id = e.target.dataset.deleteId ?? e.target.parentElement.dataset.deleteId;

        if (id) {
            DeleteHandlerPersonDetailsHover(`${url}${id}`);
        }
    });
}

         

function DeleteHandlerPersonDetailsHover(url) {

    Swal.fire({
        title: "Are you Sure!!",
        text: "You won't be able to revert this!",
        icon: "question",
        showCancelButton: true,
        cancelButtonColor: "red",
        confirmButtonColor: "blue",
        confirmButtonText: "Yes! Delete it"
    }).then(async (res) => {

        if (res.isConfirmed) {

            try {
                const result = await fetch(url, { method: "DELETE" });
                const { success, message } = await result.json();

                if (success) {
                    dataTable.ajax.reload();
                    toastr.success(message);
                } else {
                    toastr.error(message);
                }
            }

            catch (e) {
                console.log("Network issue", e);
            }
        }
    });
}

function loadDataTable() {
    dataTable = $("#dataTable").DataTable({
        ajax: {
            url: "/Admin/PersonalMethodFetchMethod",
            type: "POST",
        },

        columns: [
            { data: "idDTOs" },
            { data: "roleNameDTOs" },
            { data: "fullNameDTOs" },
            { data: "genderDTOs" },
            { data: "dobDTOs" },
            { data: "emailDTOs" },
            { data: "phoneNumber" },
            { data: "countryDTOs" },
            { data: "stateDTOs" },
            { data: "districtDTOs" },
            {
                data: "profileImageDTOs",
                render: function (data) {
                    return `<a class="image-popup" href="${data}">
                            <img src="${data}" class="img-fluid img-thumbnail avatar-md" style="box-shadow:0 0 6px #AFC5E7;">
                        </a>`;
                } 
            }, 


            {
                data: "idDTOs", 
                render: function (data) {
                    return ` <div>
                        <a href="/Admin/PersonalDetails?id=${data}" class="btn btn-soft-info">
                            <i class="bx bx-edit"></i>
                        </a>
                         
                        <a class="btn btn-soft-danger delete-btn" data-delete-id="${data}">
                            <i class="bx bxs-trash"></i>
                        </a>                
                    </div>`;
                }

            }
        ]
    });
}
