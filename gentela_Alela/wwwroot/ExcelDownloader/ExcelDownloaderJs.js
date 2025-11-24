$("#downloadExcel").click( function () {    
    $.ajax({
        url: "/Citizine/RegistrationExcelDownload", // Added leading slash for proper routing
        type: "POST",
        dataType: "json",
        success: function (data) {
            if (data.downloadAllows === 1) {

                //Get the list of registration data
                var jdata = JSON.parse(JSON.stringify(data));

                //Convert to JSON string for excel helper
                var jsonData = JSON.stringify(jdata.data.result);

                //Use your excel helper class
                var myTextXML = new myExcelXML(jsonData);
                                                        
                const dynamicExcelName = "RegisterName";
                myTextXML.downLoad(dynamicExcelName);

                toastr.success("Excel downloaded successfully!");
            } else {
                toastr.error("Download failed! Please try again.");
            }
        },
        error: function ( ) {
            console.error("Error while downloading Excel:");
            toastr.error("An error occurred while downloading Excel.");
        }
    });
});
