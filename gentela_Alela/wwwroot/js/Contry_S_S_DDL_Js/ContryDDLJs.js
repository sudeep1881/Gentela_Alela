
$(document).ready(function () {

    // When Country changes
    $('#CountryId').change(function () {
        var countryId = $(this).val();
        $('#StateId').empty().append('<option value="">--Select State--</option>');
        $('#DistrictId').empty().append('<option value="">--Select District--</option>');

        if (countryId) {
            $.getJSON('/Admin/GetState?countryId=' + countryId, function (data) {
                $.each(data, function (i, item) {
                    $('#StateId').append($('<option>', {
                        value: item.id,
                        text: item.stateName
                    }));
                });
            });
        }
    });

    // When State changes
    $('#StateId').change(function () {
        var stateId = $(this).val();
        $('#DistrictId').empty().append('<option value="">--Select District--</option>');

        if (stateId) {
            $.getJSON('/Admin/GetDistrict?stateId=' + stateId, function (data) {
                $.each(data, function (i, item) {
                    $('#DistrictId').append($('<option>', {
                        value: item.id,
                        text: item.districtName
                    }));
                });
            });
        }
    });

});

