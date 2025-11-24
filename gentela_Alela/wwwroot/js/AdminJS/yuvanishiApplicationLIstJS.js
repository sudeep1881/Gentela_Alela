let dataTable;

$(function () {
    loadDataTable();
    DeleteHandlerYuvanidhiDelete("/Admin/YuvanidhiApplicationListDeleteMethod?id=", dataTable);

})

function DeleteHandlerYuvanidhiDelete(url, dataTable) {
    const table = document.querySelector("#dataTable");
    table.addEventListener("click", (e) => {
        let id = e.target.dataset.deleteId ?? e.target.parentElement.dataset.deleteId;
        if (id) {
            deleteHandlerHoverYuavanidhi(`${url}${id}`, dataTable);
        }
    })
}

function deleteHandlerHoverYuavanidhi(url, dataTable) {
    Swal.fire({
        title: "Are You Sure About This ",
        text: "You Won't return this data",
        icon: "warning",
        showCancelButton: true,
        cancelButtonColor: "red",
        confirmButtonColor: "green",
        confirmButtonText:"Yes! Delete it"

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

function loadDataTable() {
    dataTable = $("#dataTable").DataTable({
        ajax: {
            url: "/Admin/YuvanidhiApplicationFetchMethod",
            type:"POST"
        },
        columns: [
            { data:"idDTOs"},
            { data:"adharNameDTOs"},
            { data:"dobDTOs"},
            { data:"genderDTOs"},         
            {
                data: "photoDTOs",
                orderable: false,
                width:"25%",
                render: function (data) {
                    return `<a class="image-popup" href="${data}" >
          <img src="${data}"  alt = "FavIcon" class="img-fluid avatar-md img-thumbnail" style="box-shadow: 0 0 6px #AFC5E7;"></img>
          </a>`;
                }
            },
            { data:"pernmentAdressDTOs"},
            { data:"distinctDTOs"},
            { data:"talukDTOs"},
            { data:"pincodeDTOs"},
            { data:"courseDTOs"},
            { data:"levelDTOs"},
            { data:"passoutDTOs"},
            { data:"universityDTOs"},
            { data:"registerNODTOs"},
            { data:"domicineName"},
            { data:"domicineCardNo"},
            { data:"mobileNoDTOs"},
            { data:"emailDTOs"},
            { data:"categoryDTOs"},
            { data:"disabilityDTOs"},
          
            {
                data: "idDTOs",
                width: "25%",
                render: function (data) {
                    return `<div>
   
    <a   class="btn btn-soft-danger   delete-btn" data-delete-id="${data}">
        <i class="bx bxs-trash"></i>
    </a>
    </div>`;
                }
            },
        ]
    })
}