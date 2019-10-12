var City = "";
var City1 = "";
$(document).ready(function () {
    var dobday = new Date(1980, 0, 1);
    $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
    $('#DOB').datepicker('setDate', dobday);

    document.getElementById("loader").style.display = "block";
    LoadCombos();


    $("#btnSearch").click(function () {
        getMainGridDetails();
    });

    $("#btnClearfilter").click(function () {
        clearfilter();
    });

    $('.searchcntrls').keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            getMainGridDetails();
        }
    });
});



function getMainGridDetails() {
    $('#democollapseBtn').collapse('hide');

    var obj = {};
    obj.C_NAME = $.trim($("#txt_C_NAME").val());
    obj.CNT = $("#cmbRows").val();

    obj.C_ID = $.trim($("#txt_C_ID").val());
    obj.FILE_NO = $.trim($("#txt_FILE_NO").val());
    obj.PAN = $.trim($("#txt_PAN").val());
    obj.GSTIN = $.trim($("#txt_GSTIN").val());

    obj.PH_NO = $.trim($('#txt_PHONE').val());
    obj.MOBILE_NO1 = $.trim($('#txt_MOBILE1').val());
    obj.MOBILE_NO2 = $.trim($('#txt_MOBILE2').val());
    obj.AADHAAR = $.trim($("#txt_AADHAAR").val());

    var CLI_GRP_LST = [];
    $('#cmb_CLI_GRP > option:selected').each(function () {
        CLI_GRP_LST.push($(this).val());
    });
    obj.CLI_GRP_LST = CLI_GRP_LST.join(',');

    var CLI_CAT_LST = [];
    $('#cmb_CLI_CAT > option:selected').each(function () {
        CLI_CAT_LST.push($(this).val());
    });
    obj.CLI_CAT_LST = CLI_CAT_LST.join(',');

    obj.WARD = $("#txt_WARD").val();
    obj.RACK_NO = $("#txt_RACK").val();

    obj.GENDER = $("#cmb_GENDER").val();
    obj.STATE = $("#cmb_STATE").val();
    obj.CITY = $("#cmb_CITY").val();
    obj.EMAIL_ID = $("#txt_EMAIL").val();


    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "ClientMaster.aspx/GetData",
        data: '{obj: ' + JSON.stringify(obj) + '}',
        dataType: "json",
        success: function (data) {
            $('#griddiv').remove();
            $('#maindiv').append("<div class='table-responsive' id='griddiv'></div>");
            $('#griddiv').append("<table id='tablemain' class='table table-striped table-bordered' style='width: 100%'></table>");
            $('#tablemain').append("<thead><tr><th>Client ID</th><th>File No</th><th>Name</th><th>Mobile</th><th>PAN</th><th>GSTIN</th><th>Active</th><th></th><th></th><th></th></tr></thead><tbody></tbody>");
            $('#tablemain tbody').remove();
            $('#tablemain').append("<tbody>");
            for (var i = 0; i < data.d.length; i++) {
                $('#tablemain').append(
                    "<tr><td style='text-align:center;color:brown'><b>" + data.d[i].C_ID + "</b></td>" +
                    "<td style='text-align:center;'>" + data.d[i].FILE_NO + "</td>" +
                    "<td style='color:blue'><b>" + data.d[i].C_NAME + "<b></td>" +
                    "<td style='text-align:center;color:green'><b>" + data.d[i].MOBILE_NO1 + "<b></td>" +
                    "<td>" + data.d[i].PAN + "</td>" +
                    "<td>" + data.d[i].GSTIN + "</td>" +
                    "<td  style='text-align:center;'>" + "<input type='checkbox' onclick='return false;' " + (data.d[i].ACTIVE_STATUS == true ? "checked='checked'" : "") + "/></td>" +
                    "<td style='text-align:center;'>" + "<img src='../../Images/edit.png' alt='Edit Record' class='editButton handcursor' data-id='" + data.d[i].C_ID + "' name='submitButton' id='btnEdit' value='Edit' style='margin-right:5px'/>" + "</td>" +
                    "<td style='text-align:center;'><img src='../../Images/delete.png' alt='Delete Record' class='deleteButton handcursor' data-id='" + data.d[i].C_ID + "' name='submitButton' id='btnDelete' value='Delete' style='margin-right:5px;margin-left:5px'/> </td>" +
                    "<td style='text-align:center;'>" + "<img src='../../Images/upload.png' alt='Upload Image' class='uploadButton handcursor' data-id='" + data.d[i].C_ID + "' name='submitButton' id='btnUpload' value='Upload' style='margin-right:5px;margin-left:5px'/>" + "</td></tr>");

            }
            $('#tablemain').append("</tbody>");
            $('#tablemain').DataTable({
                "order": [[0, "desc"]]
            });
            //data-toggle='modal' data-target='#PopupModal'
            document.getElementById("loader").style.display = "none";
        },
        error: function () {
            alert("Error while Showing update data");
        }

        //
    });
}

function clearfilter() {
    $('#democollapseBtn').collapse('hide');
    $("#cmbRows").val(20);
    $("#txt_C_NAME").val('');
    $("#txt_C_ID").val('');
    $("#txt_FILE_NO").val('');
    $("#txt_PAN").val('');
    $("#txt_GSTIN").val('');
    $("#txt_PHONE").val('');
    $("#txt_MOBILE1").val('');
    $("#txt_MOBILE2").val('');
    $("#txt_AADHAAR").val('');
    $("#cmb_CLI_GRP").val('default').selectpicker("refresh");
    $("#cmb_CLI_CAT").val('default').selectpicker("refresh");
    $("#txt_WARD").val('');
    $("#txt_RACK").val('');
    $("#cmb_GENDER").val('');
    $("#cmb_STATE").val('');
    StateComboChangeF();
    $("#cmb_CITY").val('');
    $("#txt_EMAIL").val('');
    getMainGridDetails();
}

function LoadCombos() {
    document.getElementById("loader").style.display = "block";
    $.ajax({
        type: "POST",
        url: "ClientMaster.aspx/GetStates",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadStateCombo
    });
}

function LoadStateCombo(data) {
    var options = [];
    options.push('<option value="',
         '', '">',
         '--Select--', '</option>');
    for (var i = 0; i < data.d.length; i++) {
        options.push('<option value="',
          data.d[i].NAME, '">',
          data.d[i].NAME, '</option>');
    }
    $("#STATE").html(options.join(''));
    $("#STATE1").html(options.join(''));
    $("#cmb_STATE").html(options.join(''));


    $.ajax({
        type: "POST",
        url: "ClientMaster.aspx/GetActiveClientGroups",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadClientGroupCombo
    });
}

//Loading City Combo on State Combo Change (Permanent Address)
function StateComboChange() {
    //if ($('#STATE').val() != '') {
    $.ajax({
        type: "POST",
        url: "ClientMaster.aspx/GetCityByState",
        data: "{str: '" + $('#STATE').val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadCityCombo
    });
    //}
    //else {
    //    $('#CITY')
    //    .find('option')
    //    .remove()
    //    .end()
    //    .append('<option value="whatever">text</option>')
    //    .val('whatever');
    //}
}

function LoadCityCombo(data) {
    var options = [];
    options.push('<option value="',
         '', '">',
         '--Select--', '</option>');
    for (var i = 0; i < data.d.length; i++) {
        options.push('<option value="',
          data.d[i].CITY, '">',
          data.d[i].CITY, '</option>');
    }
    $("#CITY").html(options.join(''));
    if (City.trim() != '') {
        $("#CITY").val(City).change();
        City = '';
    }
}

//Loading City Combo on State Combo Change (Present Address)
function StateComboChange1() {
    //if ($('#STATE1').val() != '') {
    $.ajax({
        type: "POST",
        url: "ClientMaster.aspx/GetCityByState",
        data: "{str: '" + $('#STATE1').val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadCityCombo1
    });
    //}
    //else {
    //    $('#CITY1')
    //    .find('option')
    //    .remove()
    //    .end()
    //    .append('<option value="whatever">text</option>')
    //    .val('whatever');
    //}
}

function LoadCityCombo1(data) {
    var options = [];
    options.push('<option value="',
         '', '">',
         '--Select--', '</option>');
    for (var i = 0; i < data.d.length; i++) {
        options.push('<option value="',
          data.d[i].CITY, '">',
          data.d[i].CITY, '</option>');
    }
    $("#CITY1").html(options.join(''));
    if (City1.trim() != '') {
        $("#CITY1").val(City1).change();
        City1 = '';
    }
}

function StateComboChangeF() {
    //if ($('#STATE1').val() != '') {
    $.ajax({
        type: "POST",
        url: "ClientMaster.aspx/GetCityByState",
        data: "{str: '" + $('#cmb_STATE').val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadCityComboF
    });
    //}
    //else {
    //    $('#CITY1')
    //    .find('option')
    //    .remove()
    //    .end()
    //    .append('<option value="whatever">text</option>')
    //    .val('whatever');
    //}
}

function LoadCityComboF(data) {
    var options = [];
    options.push('<option value="',
         '', '">',
         '--Select--', '</option>');
    for (var i = 0; i < data.d.length; i++) {
        options.push('<option value="',
          data.d[i].CITY, '">',
          data.d[i].CITY, '</option>');
    }
    $("#cmb_CITY").html(options.join(''));
}

function LoadClientGroupCombo(data) {
    var options = [];
    var options1 = [];
    options.push('<option value="',
         0, '">',
         '--Select--', '</option>');
    for (var i = 0; i < data.d.length; i++) {
        options.push('<option value="',
          data.d[i].CLI_GRP_ID, '">',
          data.d[i].CLI_GRP_NAME, '</option>');
        options1.push('<option value="',
          data.d[i].CLI_GRP_ID, '">',
          data.d[i].CLI_GRP_NAME, '</option>');
    }
    $("#CLI_GRP_NAME").html(options.join(''));

    $("#cmb_CLI_GRP").html(options1.join(''));
    $("#cmb_CLI_GRP").addClass("selectpicker");
    $("#cmb_CLI_GRP").addClass("form-control");
    //$('.selectpicker').selectpicker('');

    document.getElementById("loader").style.display = "block";
    $.ajax({
        type: "POST",
        url: "ClientMaster.aspx/GetActiveClientCategories",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadClientCategoryCombo
    });
}

function LoadClientCategoryCombo(data) {
    var options = [];
    //options.push('<option value="',
    //   0, '">',
    //   '--Select--', '</option>');
    for (var i = 0; i < data.d.length; i++) {
        options.push('<option value="',
          data.d[i].CLI_CAT_ID, '">',
          data.d[i].CLI_CAT_NAME, '</option>');
    }
    $("#CLI_CAT").html(options.join(''));
    $("#CLI_CAT").addClass("selectpicker");
    $("#CLI_CAT").addClass("form-control");

    $("#cmb_CLI_CAT").html(options.join(''));
    $("#cmb_CLI_CAT").addClass("selectpicker");
    $("#cmb_CLI_CAT").addClass("form-control");

    $('.selectpicker').selectpicker('');

    getMainGridDetails();
}

function GenederComboChange() {
    if ($('#GENDER').val() == 'Female') {
        $("#HNAME").prop('disabled', false);
    }
    else {
        $("#HNAME").prop('disabled', true);
        $('#HNAME').val('');
    }
}

function ClearDetialViewControls() {
    $("#C_NAME").val('');
    $("#ALIAS").val('');
    $("#FILE_NO").val('');
    $("#FNAME").val('');
    $("#GENDER").val('');
    $("#HNAME").prop('disabled', true);
    $('#HNAME').val('');
    $('#CNT_NAME').val('');
    $('#CLI_GRP_NAME').val(0);
    $("#CLI_CAT").val('default').selectpicker("refresh");
    var dobday = new Date(1980, 0, 1);
    $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
    $('#DOB').datepicker('setDate', dobday);
    $('#PAN').val('');
    $('#AADHAAR').val('');
    $('#GSTIN').val('');
    $('#ADDR').val('');
    $('#STATE').val('');
    $('#CITY').val('');
    $('#PIN').val('');
    $("#SAME_AB").prop('checked', false);
    $("#ADDR1").prop('disabled', false);
    $("#STATE1").prop('disabled', false);
    $("#CITY1").prop('disabled', false);
    $("#PIN1").prop('disabled', false);
    $('#ADDR1').val('');
    $('#STATE1').val('');
    $('#CITY1').val('');
    $('#PIN1').val('');
    $('#PH_NO').val('');
    $('#MOBILE_NO1').val('');
    $('#MOBILE_NO2').val('');
    $('#EMAIL_ID').val('');
    $('#WARD').val('');
    $('#RACK_NO').val('');
    $('#ALERT_MSG').val('');
    $("#ACTIVE_STATUS").prop('checked', true);
    $('#C_ID').text('');
}

function chkaddrchanged() {
    if ($("#SAME_AB").prop("checked") == true) {
        $("#ADDR1").prop('disabled', true);
        $("#STATE1").prop('disabled', true);
        $("#CITY1").prop('disabled', true);
        $("#PIN1").prop('disabled', true);
        $('#ADDR1').val($('#ADDR').val());
        City1 = $('#CITY').val();
        $('#STATE1').val($('#STATE').val());
        StateComboChange1();
        $('#PIN1').val($('#PIN').val());
    }
    else {
        $("#ADDR1").prop('disabled', false);
        $("#STATE1").prop('disabled', false);
        $("#CITY1").prop('disabled', false);
        $("#PIN1").prop('disabled', false);
    }
}

$(function () {
    $("#btnSave").click(function () {

        if ($("#C_NAME").val().trim() == "") {
            alert("Please enter Client name.");
            $("#C_NAME").focus();
            return false;
        }

        var obj = {};
        obj.C_NAME = $("#C_NAME").val();
        obj.ALIAS = $("#ALIAS").val();
        obj.FILE_NO = $("#FILE_NO").val();
        obj.FNAME = $("#FNAME").val();
        obj.GENDER = $("#GENDER").val();
        obj.HNAME = $("#HNAME").val();
        obj.CNT_NAME = $("#CNT_NAME").val();
        obj.CLI_GRP_ID = $("#CLI_GRP_NAME").val();
        var clientCategoryStringList = [];
        $('#CLI_CAT > option:selected').each(function () {
            clientCategoryStringList.push($(this).val());
        });
        obj.ClientCategoryStringList = clientCategoryStringList.join(',');
        obj.DOB = $("#DOB").val();
        obj.PAN = $("#PAN").val();
        obj.AADHAAR = $("#AADHAAR").val();
        obj.GSTIN = $("#GSTIN").val();
        obj.ADDR = $("#ADDR").val();
        obj.STATE = $("#STATE").val();
        obj.CITY = $("#CITY").val();
        obj.PIN = $("#PIN").val();
        if ($('#SAME_AB').is(":checked")) {
            obj.SAME_AB = true;
        }
        else {
            obj.SAME_AB = false;
        }
        obj.ADDR1 = $("#ADDR1").val();
        obj.STATE1 = $("#STATE1").val();
        obj.CITY1 = $("#CITY1").val();
        obj.PIN1 = $("#PIN1").val();
        obj.PH_NO = $('#PH_NO').val();
        obj.MOBILE_NO1 = $('#MOBILE_NO1').val();
        obj.MOBILE_NO2 = $('#MOBILE_NO2').val();
        obj.EMAIL_ID = $("#EMAIL_ID").val();
        obj.WARD = $("#WARD").val();
        obj.RACK_NO = $("#RACK_NO").val();
        obj.ALERT_MSG = $("#ALERT_MSG").val();
        if ($('#ACTIVE_STATUS').is(":checked")) {
            obj.ACTIVE_STATUS = true;
        }
        else {
            obj.ACTIVE_STATUS = false;
        }

        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "ClientMaster.aspx/InsertData",
            data: '{obj: ' + JSON.stringify(obj) + '}',
            dataType: "json",
            success: function (data) {
                for (var i = 0; i < data.d.length; i++) {
                    if (data.d[i].RESULT === 1) {
                        getMainGridDetails();
                        alert(data.d[i].MSG);
                        $('#mainlistingdiv').show();
                        $('#mainldetaildiv').hide();
                    }
                    else {
                        alert(data.d[i].MSG);
                        $("#C_NAME").focus();
                        return false;
                    }
                }
            },
            error: function (data) {
                alert("Error while Adding data of :" + obj.NAME);
                $("#C_NAME").focus();
                return false;
            }
        });

    });

    $(document).on("click", ".deleteButton", function () {
        if (confirm("Are you sure you want to delete !") == true) {
            var id = $(this).attr("data-id");
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "ClientMaster.aspx/DeleteData",
                data: '{id: ' + id + '}',
                dataType: "json",
                success: function (data) {
                    for (var i = 0; i < data.d.length; i++) {
                        if (data.d[i].RESULT === 1) {
                            getMainGridDetails();
                            alert(data.d[i].MSG);
                        }
                        else {
                            alert(data.d[i].MSG);
                            return false;
                        }
                    }
                },
                error: function (data) {
                    alert("Error while Deleting data of :" + id);
                }
            });
        }

    });

    $(document).on("click", ".addNewButton", function () {
        $('#btnSave').show();
        $('#btnUpdate').hide();

        $('#mainlistingdiv').hide();
        $('#mainldetaildiv').show();

        ClearDetialViewControls();
        $("#subheaderdiv").html("<h3 style='color:blue'>Client Master -> Add Client Details</h3>");

        $('#C_NAME').focus();
    });

    $(document).on("click", ".editButton", function () {
        document.getElementById("loader").style.display = "block";
        var id = $(this).attr("data-id");
        console.log(id);
        $("#btnUpdate").attr("edit-id", id);
        //alert(id);  //getting the row id 

        $('#btnSave').hide();
        $('#btnUpdate').show();
        $('#mainlistingdiv').hide();
        $('#mainldetaildiv').show();

        ClearDetialViewControls();
        $("#subheaderdiv").html("<h3 style='color:blue'>Client Master -> Edit Client Details</h3>");
        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "ClientMaster.aspx/EditData",
            data: '{id: ' + id + '}',
            dataType: "json",
            success: function (data) {
                for (var i = 0; i < data.d.length; i++) {
                    $("#subheaderdiv").html("<h3 style='color:blue'>Client Master -> Edit Client Details -> Client ID:" + data.d[i].C_ID + "</h3>");
                    $("#C_NAME").val(data.d[i].C_NAME);
                    $("#ALIAS").val(data.d[i].ALIAS);
                    $("#FILE_NO").val(data.d[i].FILE_NO);

                    $("#FNAME").val(data.d[i].FNAME);
                    $("#GENDER").val(data.d[i].GENDER);
                    $("#HNAME").val(data.d[i].HNAME);
                    $("#CNT_NAME").val(data.d[i].CNT_NAME);
                    $("#CLI_GRP_NAME").val(data.d[i].CLI_GRP_ID);
                    $("#PAN").val(data.d[i].PAN);
                    $("#AADHAAR").val(data.d[i].AADHAAR);
                    $("#GSTIN").val(data.d[i].GSTIN);
                    $("#ADDR").val(data.d[i].ADDR);
                    City = data.d[i].CITY;
                    $("#STATE").val(data.d[i].STATE);
                    StateComboChange();
                    $("#PIN").val(data.d[i].PIN);

                    if (data.d[i].SAME_AB == true) {
                        $("#SAME_AB").prop('checked', true);
                        $("#ADDR1").prop('disabled', true);
                        $("#STATE1").prop('disabled', true);
                        $("#CITY1").prop('disabled', true);
                        $("#PIN1").prop('disabled', true);
                    }
                    else {
                        $("#SAME_AB").prop('checked', false);
                        $("#ADDR1").prop('disabled', false);
                        $("#STATE1").prop('disabled', false);
                        $("#CITY1").prop('disabled', false);
                        $("#PIN1").prop('disabled', false);

                    }
                    $("#ADDR1").val(data.d[i].ADDR1);
                    City1 = data.d[i].CITY1;
                    $("#STATE1").val(data.d[i].STATE1);
                    StateComboChange1();
                    $("#PIN1").val(data.d[i].PIN1);

                    $('#DOB').datepicker({ dateFormat: 'dd-mm-yy' }).datepicker('setDate', data.d[i].DOB.split('-')[2] + '-' + data.d[i].DOB.split('-')[1] + '-' + data.d[i].DOB.split('-')[0]);

                    if (data.d[i].ACTIVE_STATUS == true)
                        $("#ACTIVE_STATUS").prop('checked', true);
                    else
                        $("#ACTIVE_STATUS").prop('checked', false);

                    $("#PH_NO").val(data.d[i].PH_NO);
                    $("#MOBILE_NO1").val(data.d[i].MOBILE_NO1);
                    $("#MOBILE_NO2").val(data.d[i].MOBILE_NO2);
                    $("#EMAIL_ID").val(data.d[i].EMAIL_ID);
                    $("#WARD").val(data.d[i].WARD);
                    $("#RACK_NO").val(data.d[i].RACK_NO);
                    $("#ALERT_MSG").val(data.d[i].ALERT_MSG);
                    $("#C_ID").text(data.d[i].C_ID);

                    var array = data.d[i].ClientCategoryStringList.split(",");
                    $('#CLI_CAT').selectpicker('val', array);
                }
                $('#C_NAME').focus();
                document.getElementById("loader").style.display = "none";
            },
            error: function () {
                alert("Error while retrieving data of :" + id);
            }
        });
    });

    $("#btnUpdate").click(function () {
        if ($("#C_NAME").val().trim() == "") {
            alert("Please enter Client name.");
            $("#C_NAME").focus();
            return false;
        }

        var id = $(this).attr("edit-id");

        var obj = {};
        obj.C_ID = id;
        obj.C_NAME = $("#C_NAME").val();
        obj.ALIAS = $("#ALIAS").val();
        obj.FILE_NO = $("#FILE_NO").val();
        obj.FNAME = $("#FNAME").val();
        obj.GENDER = $("#GENDER").val();
        obj.HNAME = $("#HNAME").val();
        obj.CNT_NAME = $("#CNT_NAME").val();
        obj.CLI_GRP_ID = $("#CLI_GRP_NAME").val();
        var clientCategoryStringList = [];
        $('#CLI_CAT > option:selected').each(function () {
            clientCategoryStringList.push($(this).val());
        });
        obj.ClientCategoryStringList = clientCategoryStringList.join(',');
        obj.DOB = $("#DOB").val();
        obj.PAN = $("#PAN").val();
        obj.AADHAAR = $("#AADHAAR").val();
        obj.GSTIN = $("#GSTIN").val();
        obj.ADDR = $("#ADDR").val();
        obj.STATE = $("#STATE").val();
        obj.CITY = $("#CITY").val();
        obj.PIN = $("#PIN").val();
        if ($('#SAME_AB').is(":checked")) {
            obj.SAME_AB = true;
        }
        else {
            obj.SAME_AB = false;
        }
        obj.ADDR1 = $("#ADDR1").val();
        obj.STATE1 = $("#STATE1").val();
        obj.CITY1 = $("#CITY1").val();
        obj.PIN1 = $("#PIN1").val();
        obj.PH_NO = $('#PH_NO').val();
        obj.MOBILE_NO1 = $('#MOBILE_NO1').val();
        obj.MOBILE_NO2 = $('#MOBILE_NO2').val();
        obj.EMAIL_ID = $("#EMAIL_ID").val();
        obj.WARD = $("#WARD").val();
        obj.RACK_NO = $("#RACK_NO").val();
        obj.ALERT_MSG = $("#ALERT_MSG").val();
        if ($('#ACTIVE_STATUS').is(":checked")) {
            obj.ACTIVE_STATUS = true;
        }
        else {
            obj.ACTIVE_STATUS = false;
        }

        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "ClientMaster.aspx/UpdateData",
            data: '{obj: ' + JSON.stringify(obj) + ', id : ' + id + '}',
            dataType: "json",
            success: function (data) {
                for (var i = 0; i < data.d.length; i++) {
                    if (data.d[i].RESULT === 1) {
                        getMainGridDetails();
                        alert(data.d[i].MSG);
                        $('#mainlistingdiv').show();
                        $('#mainldetaildiv').hide();
                    }
                    else {
                        alert(data.d[i].MSG);
                        $("#C_NAME").focus();
                        return false;
                    }
                }
            },
            error: function (data) {
                alert("Error while Updating data of :" + id);
                $("#C_NAME").focus();
                return false;
            }
        });
    });

    $(document).on("click", ".cancelButton", function () {
        $('#mainlistingdiv').show();
        $('#mainldetaildiv').hide();
    });

    $(document).on("click", ".uploadButton", function () {
        $('#PopupModalUpload').modal('show');
        $('#PopupModalUpload').focus();
        var id = $(this).attr("data-id");
        console.log(id);
        $("div.modal-header h4").html("Client Documents Upload Area -> Client ID : " + id);
        $('#fileToUpload').val("");
        $("#btnUploadDoc").attr("edit-id", id);
        ShowUploadedFiles();
        $('#fileToUpload').focus();
    });

    $("#btnUploadDoc").click(function () {

        if ($('#fileToUpload').val() == null || $('#fileToUpload').val() == "") {
            alert('Please Select an Image file upload');
            $('#fileToUpload').focus();
            return;
        }

        var id = $(this).attr("edit-id");

        var fileToUpload = getNameFromPath($('#fileToUpload').val());
        var orgfilename = fileToUpload;
        var phyfilename = String(id) + '_' + String(getFormattedTimeStamp()) + '.' + orgfilename.substr((orgfilename.lastIndexOf('.') + 1));
        var remarks = $('#txt_docremakrs').val();
        // if (checkFileExtension(fileToUpload)) {
        if (orgfilename != "" && orgfilename != null) {
            $("#loading").show();
            $.ajaxFileUpload({
                url: 'ClientDocsUpload.ashx?action=UPLOAD&clientid=' + id + '&phy_file_name=' + phyfilename + '&org_file_name=' + orgfilename + '&remarks=' + remarks,
                secureuri: false,
                fileElementId: 'fileToUpload',
                dataType: 'json',
                success: function (data, status) {
                    if (typeof (data.error) != 'undefined') {
                        if (data.error != '') {
                            alert(data.error);
                        } else {
                            ShowUploadedFiles();
                            $('#fileToUpload').val("");
                        }
                    }
                    $("#loading").hide();
                    $('#txt_docremakrs').val('');
                },
                error: function (data, status, e) {
                    alert(e);
                    $("#loading").hide();
                }
            });

        }
        //}
        //else {
        //    alert('You can upload only jpg,jpeg,png extension Image files.');
        //}
    });

    $(document).on("click", ".deleteButtonDoc", function () {
        var phyimage = $(this).attr("data-id");
        var clientid = $("#btnUploadDoc").attr("edit-id");
        if (confirm("Are you sure you want to delete the Image!") == true) {
            var orgfilename = '';
            $.ajax({
                url: 'ClientDocsUpload.ashx?action=DELETE&clientid=' + clientid + '&phy_file_name=' + phyimage + '&org_file_name=' + orgfilename,
                type: "GET",
                cache: false,
                async: true,
                success: function (html) {
                    ShowUploadedFiles();
                    alert('File Deleted successfully.')
                }
            });
        }

    });
});

function checkFileExtension(file) {
    var flag = true;
    var extension = file.substr((file.lastIndexOf('.') + 1));

    switch (extension) {
        case 'jpg':
        case 'jpeg':
        case 'png':
        case 'JPG':
        case 'JPEG':
        case 'PNG':
            flag = true;
            break;
        default:
            flag = false;
    }

    return flag;
}

//get file path from client system
function getNameFromPath(strFilepath) {

    var objRE = new RegExp(/([^\/\\]+)$/);
    var strName = objRE.exec(strFilepath);

    if (strName == null) {
        return null;
    }
    else {
        return strName[0];
    }
}

function ShowUploadedFiles() {
    var clientid = $("#btnUploadDoc").attr("edit-id");
    $('#txt_docremakrs').val('');
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "ClientMaster.aspx/GetClientDocsData",
        data: '{id: ' + clientid + '}',
        dataType: "json",
        success: function (data) {
            $('#tableupload tbody').remove();
            $('#tableupload').append("<tbody>");
            for (var i = 0; i < data.d.length; i++) {
                $('#tableupload').append(
                    "<tr><td>" + data.d[i].ORG_FILE_NAME + "</td>" + "<td>" + data.d[i].REMARKS + "</td>" +
                    "<td style='text-align:center'><img src='../../Images/delete.png' alt='Delete Record' class='deleteButtonDoc handcursor' data-id='" + data.d[i].PHY_FILE_NAME + "' name='submitButton' id='btnDeleteDoc' value='Delete' style='margin-right:5px;margin-left:5px'/> </td>" +
                    "<td style='text-align:center'><a class='downloadButton' href='ClientDocsUpload.ashx?action=DOWNLOAD&clientid=" + clientid + "&phy_file_name=" + data.d[i].PHY_FILE_NAME + "&org_file_name=" + data.d[i].ORG_FILE_NAME + "'><img src='../../Images/download.png' alt='Download Record' class='downloadButton handcursor' id='btnDeleteImage' style='margin-right:5px;margin-left:5px'/> </td></a></tr>");
            }
            $('#tableupload').append("</tbody>");
        },
        error: function () {
            alert("Error while Showing update data");
        }
        //
    });

}

function getFormattedTimeStamp() {
    var today = new Date();
    var y = today.getFullYear();
    // JavaScript months are 0-based.
    var m = today.getMonth() + 1;
    var d = today.getDate();
    var h = today.getHours();
    var mi = today.getMinutes();
    var s = today.getSeconds();
    var ms = today.getMilliseconds();
    return y + "-" + m + "-" + d + "-" + h + "-" + mi + "-" + s + "-" + ms;
}
