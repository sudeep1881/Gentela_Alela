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