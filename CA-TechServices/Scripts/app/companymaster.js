var City = "";
$(function () {
    $.ajax({
        type: "POST",
        url: "Company.aspx/GetStates",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadStateCombo
    });
});

function LoadStateCombo(data) {
    var options = [];
    for (var i = 0; i < data.d.length; i++) {
        options.push('<option value="',
          data.d[i].NAME, '">',
          data.d[i].NAME, '</option>');
    }
    $("#STATE1").html(options.join(''));
    getDetails();
}


function getDetails() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Company.aspx/GetData",
        data: {},
        dataType: "json",
        success: function (data) {
            for (var i = 0; i < data.d.length; i++) {
                $('#COMPANY_NAME1').val(data.d[i].COMPANY_NAME);
                $('#ADDRESS1').val(data.d[i].ADDRESS);
                $('#STATE1').val(data.d[i].STATE).change();
                $('#PIN_NO1').val(data.d[i].PIN_NO);
                $('#GSTIN1').val(data.d[i].GSTIN);
                $('#TELEPHONE1').val(data.d[i].TELEPHONE);
                $('#FAX1').val(data.d[i].FAX);
                $('#EMAIL1').val(data.d[i].EMAIL);
                $('#WEB1').val(data.d[i].WEB);
                City = data.d[i].CITY;
            }
        },
        error: function () {
            alert("Error while Showing update data");
        }
    });
}

//Loading City Combo on State Combo Change
function StateComboChange() {
    if ($('#STATE1').val() != '') {
        $.ajax({
            type: "POST",
            url: "Company.aspx/GetCityByState",
            data: "{str: '" + $('#STATE1').val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: LoadCityCombo
        });
    }
    else {
        $('#CITY1')
        .find('option')
        .remove()
        .end()
        .append('<option value="whatever">text</option>')
        .val('whatever');
    }
}

function LoadCityCombo(data) {
    var options = [];
    for (var i = 0; i < data.d.length; i++) {
        options.push('<option value="',
          data.d[i].CITY, '">',
          data.d[i].CITY, '</option>');
    }
    $("#CITY1").html(options.join(''));
    if (City.trim() != '') {
        $("#CITY1").val(City).change();
        City = '';
    }
}


$(function () {
    $("#btnSave").click(function () {
        if ($("#COMPANY_NAME1").val().trim() == "") {
            alert("Please enter company Name.");
            $("#COMPANY_NAME1").focus();
            return false;
        }

        var obj = {};
        obj.COMPANY_NAME = $('#COMPANY_NAME1').val();
        obj.ADDRESS = $('#ADDRESS1').val();
        obj.STATE = $('#STATE1').val();
        obj.PIN_NO = $('#PIN_NO1').val();
        obj.GSTIN = $('#GSTIN1').val();
        obj.TELEPHONE = $('#TELEPHONE1').val();
        obj.FAX = $('#FAX1').val();
        obj.EMAIL = $('#EMAIL1').val();
        obj.WEB = $('#WEB1').val();
        obj.CITY = $('#CITY1').val();

        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "Company.aspx/UpdateData",
            data: '{obj: ' + JSON.stringify(obj) + '}',
            dataType: "json",
            success: function (data) {
                for (var i = 0; i < data.d.length; i++) {
                    if (data.d[i].RESULT === 1) {
                        alert(data.d[i].MSG);
                    }
                    else {
                        alert(data.d[i].MSG);
                        $("#COMPANY_NAME1").focus();
                        return false;
                    }
                }
            },
            error: function (data) {
                alert("Error while Adding data of :" + obj.NAME);
                $("#COMPANY_NAME1").focus();
                return false;
            }
        });

    });

});
