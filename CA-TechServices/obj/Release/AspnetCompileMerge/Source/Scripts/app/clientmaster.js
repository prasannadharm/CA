var City = "";
var City1 = "";
$(document).ready(function () {
    var dobday = new Date(1980, 0, 1);  
    $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
    $('#DOB').datepicker('setDate', dobday);

    document.getElementById("loader").style.display = "block";
    LoadCombos();
    getMainGridDetails();
});



function getMainGridDetails() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "ClientMaster.aspx/GetData",
        data: {},
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
            $('#tablemain').DataTable();
            //data-toggle='modal' data-target='#PopupModal'
            document.getElementById("loader").style.display = "none";
        },
        error: function () {
            alert("Error while Showing update data");
        }

        //
    });
}

function LoadCombos()
{
    document.getElementById("loader").style.display = "block";
    $.ajax({
        type: "POST",
        url: "ClientMaster.aspx/GetStates",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadStateCombo
    });
    document.getElementById("loader").style.display = "block";
    $.ajax({
        type: "POST",
        url: "ClientMaster.aspx/GetActiveClientGroups",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadClientGroupCombo
    });
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

function LoadClientGroupCombo(data) {
    var options = [];
    options.push('<option value="',
         0, '">',
         '--Select--', '</option>');
    for (var i = 0; i < data.d.length; i++) {
        options.push('<option value="',
          data.d[i].CLI_GRP_ID, '">',
          data.d[i].CLI_GRP_NAME, '</option>');
    }
    $("#CLI_GRP_NAME").html(options.join(''));
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

    $('.selectpicker').selectpicker('');

    document.getElementById("loader").style.display = "none";
}

function GenederComboChange()
{
    if ($('#GENDER').val() == 'Female')
    {     
        $("#HNAME").prop('disabled', false);
    }
    else
    {
        $("#HNAME").prop('disabled', true);
        $('#HNAME').val('');
    }
}

function ClearDetialViewControls()
{
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
    else
    {
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
      
        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "ClientMaster.aspx/EditData",
            data: '{id: ' + id + '}',
            dataType: "json",
            success: function (data) {
                for (var i = 0; i < data.d.length; i++) {
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

});