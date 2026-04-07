$("#downloadExcelFormatHomePage").click(function () {
    $.ajax({
        url: "/Citizine/DownloadExcelHomePage",
        type: "POST",
        datatype: "json",
        success: function (data){
            if (data.dataAllows === 1){

                var jdata = JSON.parse(JSON.stringify(data));
                var json = JSON.stringify(jdata.data.result);
                var jArraysList = json;

                var myXMLDownload = new myExcelXML(jArraysList);
                
                const dynamicFileName = "USer Register Details";
                myXMLDownload.downLoad(dynamicFileName);
                myExce
                toastr.success("Excel Format Download Succesffulyy");

            }
            else {
                toastr.error("Excel Format Not Downloaded");
            }
        },
        

        error: function () {
            console.log("error while  downloading Excel Format");
        }
    })
})

let dataTable;

$(function () {
    loadDataTable();
    DeleteHandler("/Admin/DeleteHandler?id=", dataTable);
})

function DeleteHandler(url, dataTable) {
    const table = document.querySelector("#dataTable");
    table.addEventListener("click", (e) => {
        let id = e.target.dataset.deleteId ?? e.target.parentElement.dataset.deleteId;
        if (id) {
            DelethandlerHover(`${url}${id}`, dataTable);
        }
    })
}

function DelethandlerHover(url, dataTable) {
    Swal.fire({
        title: "Are you sure!",
        text: "You won't return this data",
        icon: "warning",
        showCancelButton: true,
        cancelbuttonColor: "red",
        confirmButtonColor: "green",
        confimButtonText:"Yes! delete it"

    }).then(async (res) => {
        if (res.isConfimed) {
            try {
                const { success, message } = await fetch(url, { method="Delete" }).then(res => res.json());
                if (success) {
                    dataTable.ajax.reload();
                    toastr.success(message);

                } else {
                    toastr.error(message);

                }
            }
            catch (e) {
                toastr.error(message);

            }
        }
    })
}

function loadDataTable() {

    dataTable = $("#dataTable").DataTable({
        ajax: {

        }
    })
}
