
var stageslstobj = [];
var stagesobj = {};
var clientlstobj = [];
var clientobj = {};
var tempclientlstobj = [];
var lstuers = [];
$(document).ready(function () {
    var dobday = new Date(2050, 0, 1);
    $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
    $('#RECURRING_END_DATE').datepicker('setDate', dobday);

    document.getElementById("loader").style.display = "block";
    getMainGridDetails();
    
    loaddatalist();

    $.ajax({
        type: "POST",
        url: "TaskMaster.aspx/GetActiveClientCategories",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadClientCategoryCombo
    });

    $('#SEARCHTEXT').keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            searchclients();
        }
    });
    
});

function loadstagescontrols() {
    $("#StagesGroup").empty();
    for (var i = 0; i < stageslstobj.length; i++) {
        var newTextBoxDiv = $(document.createElement('div'))
            .attr("id", 'StagesDiv' + stageslstobj[i].GENID);

        newTextBoxDiv.after().html("<label id='lblslno" + stageslstobj[i].GENID + "' style='margin-left: 10px; display: inline; text-align: center;font-weight: bold;color:brown'>" + stageslstobj[i].SLNO + "</label>" +
                                        "<input id='txtstages" + stageslstobj[i].GENID + "' list='lststages' class='form-control txtstages' style='margin-left: 10px; width: 40%; display: inline; margin-bottom: 5px' placeholder='Select Stage' value='" + stageslstobj[i].STAGE + "'data-id='" + stageslstobj[i].GENID + "'/>" +
                                        "<select id='cmbusers" + stageslstobj[i].GENID + "' class='form-control cmbusers' style='margin-left: 10px; width: 25%; display: inline; margin-bottom: 5px' data-id='" + stageslstobj[i].GENID + "'/>" +
                                        "<img id='btnstageup" + stageslstobj[i].GENID + "' class='btnstageup handcursor' src='../../Images/up.png' data-id='" + stageslstobj[i].GENID + "'/>" +
                                        "<img id='btnstagedown" + stageslstobj[i].GENID + "' class='btnstagedown handcursor' src='../../Images/down.png' data-id='" + stageslstobj[i].GENID + "'/>" +
                                        "<img id='btnstaedel" + stageslstobj[i].GENID + "' class='btnstagedel handcursor' src='../../Images/delete.png' style='margin-left:8px' data-id='" + stageslstobj[i].GENID + "'/>");

        newTextBoxDiv.appendTo("#StagesGroup");

        $("#cmbusers" + stageslstobj[i].GENID).html(lstuers.join(''));
        if (stageslstobj[i].USER_ID == undefined || stageslstobj[i].USER_ID == null)
            $("#cmbusers" + stageslstobj[i].GENID).val(0);
        else
            $("#cmbusers" + stageslstobj[i].GENID).val(stageslstobj[i].USER_ID);

    }

    $('.txtstages').on('input', function () {
        var id = $(this).attr("data-id");
        for (var i = 0; i < stageslstobj.length; i++) {
            if (stageslstobj[i].GENID == id) {
                stageslstobj[i].STAGE = $(this).val();
                return false;
            }
        }
    });

    $('.cmbusers').on('input', function () {
        var id = $(this).attr("data-id");
        for (var i = 0; i < stageslstobj.length; i++) {
            if (stageslstobj[i].GENID == id) {
                stageslstobj[i].USER = $("#cmbusers" + id + " option:selected").html();
                stageslstobj[i].USER_ID = $(this).val();
                return false;
            }
        }
    });

    $(".btnstagedel").click(function () {
        if (confirm("Are you sure you want to delete !") == true) {
            var id = $(this).attr("data-id");
            var newsubItemsList = [];
            for (var i = 0; i < stageslstobj.length; i++) {
                if (stageslstobj[i].GENID != id) {
                    newsubItemsList.push(stageslstobj[i]);
                }
            }
            stageslstobj = [];
            for (var i = 0; i < newsubItemsList.length; i++) {
                stageslstobj.push(newsubItemsList[i]);
                stageslstobj[i].SLNO = (i + 1);
            }
            loadstagescontrols();
        }
    });

    $(".btnstageup").click(function () {
        var id = $(this).attr("data-id");
        var curslno = 0;
        var pregenid = '';
        var prevslno = 0;
        for (var i = 0; i < stageslstobj.length; i++) {
            if (stageslstobj[i].GENID == id) {
                curslno = stageslstobj[i].SLNO;
            }
        }
        if (curslno != 1) {
            prevslno = curslno - 1;
            for (var i = 0; i < stageslstobj.length; i++) {
                if (stageslstobj[i].SLNO == prevslno) {
                    stageslstobj[i].SLNO = curslno;
                }
            }
            for (var i = 0; i < stageslstobj.length; i++) {
                if (stageslstobj[i].GENID == id) {
                    stageslstobj[i].SLNO = prevslno;
                }
            }
        }
        resortstageslist();
        loadstagescontrols();
    });

    $(".btnstagedown").click(function () {
        var id = $(this).attr("data-id");
        var curslno = 0;
        var nxtgenid = '';
        var nxtslno = 0;
        for (var i = 0; i < stageslstobj.length; i++) {
            if (stageslstobj[i].GENID == id) {
                curslno = stageslstobj[i].SLNO;
            }
        }
        if (curslno < stageslstobj.length) {
            nxtslno = curslno + 1;
            for (var i = 0; i < stageslstobj.length; i++) {
                if (stageslstobj[i].SLNO == nxtslno) {
                    stageslstobj[i].SLNO = curslno;
                }
            }
            for (var i = 0; i < stageslstobj.length; i++) {
                if (stageslstobj[i].GENID == id) {
                    stageslstobj[i].SLNO = nxtslno;
                }
            }
        }
        resortstageslist();
        loadstagescontrols();
    });
}

function resortstageslist() {
    var newList = [];
    var cnt = 0;

    while (cnt < stageslstobj.length) {
        for (var i = 0; i < stageslstobj.length; i++) {
            if (stageslstobj[i].SLNO == (cnt + 1)) {
                newList.push(stageslstobj[i]);
            }
        }
        cnt = cnt + 1;
    }
    stageslstobj = newList;
}

function loaddatalist() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "TaskMaster.aspx/GetActiveUsers",
        data: '{}',
        dataType: "json",
        success: function (data) {
            lstuers = [];
            lstuers.push('<option value="',
             '0', '">',
             'Select User', '</option>');
            for (var i = 0; i < data.d.length; i++) {
                lstuers.push('<option value="',
                          data.d[i].ID, '">',
                          data.d[i].NAME, '</option>');
            }
        },
        error: function () {
            alert("Error while Showing update data");
        }
    });
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "TaskMaster.aspx/GetActiveTaskStages",
        data: '{}',
        dataType: "json",
        success: function (data) {
            var options = [];
            for (var i = 0; i < data.d.length; i++) {
                options.push('<option value="',
                  data.d[i].NAME, '"/>');
            }
            $("#lststages").html(options.join(''));
        },
        error: function () {
            alert("Error while Showing update data");
        }
    });
}

function getMainGridDetails() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "TaskMaster.aspx/GetData",
        data: '{}',
        dataType: "json",
        success: function (data) {
            $('#griddiv').remove();
            $('#maindiv').append("<div class='table-responsive' id='griddiv'></div>");
            $('#griddiv').append("<table id='tablemain' class='table table-striped table-bordered' style='width: 100%'></table>");
            $('#tablemain').append("<thead><tr><th>Task Name</th><th>Description</th><th>Priority</th><th>Frequency</th><th>Active</th><th></th><th></th><th></th><th></th></tr></thead><tbody></tbody>");
            $('#tablemain tbody').remove();
            $('#tablemain').append("<tbody>");
            for (var i = 0; i < data.d.length; i++) {
                $('#tablemain').append(
                    "<tr><td style='color:brown'><b>" + data.d[i].T_NAME + "</b></td>" +
                    "<td>" + data.d[i].T_DESC + "</td>" +
                    "<td style='text-align:center;color:blue'><b>" + data.d[i].PRIORITY + "<b></td>" +
                    "<td style='text-align:center;color:green'>" + data.d[i].RECURRING_TYPE + "</td>" +
                    "<td style='text-align:center;'>" + "<input type='checkbox' onclick='return false;' " + (data.d[i].ACTIVE_STATUS == true ? "checked='checked'" : "") + "/></td>" +
                    "<td style='text-align:center;'>" + "<img src='../../Images/edit.png' alt='Edit Record' class='editButton handcursor' data-id='" + data.d[i].T_ID + "' name='submitButton' id='btnEdit' value='Edit' style='margin-right:5px'/>" + "</td>" +
                    "<td style='text-align:center;'><img src='../../Images/delete.png' alt='Delete Record' class='deleteButton handcursor' data-id='" + data.d[i].T_ID + "' name='submitButton' id='btnDelete' value='Delete' style='margin-right:5px;margin-left:5px'/> </td>" +
                    "<td style='text-align:center;'>" + "<img src='../../Images/flow.png' alt='Task Stage' class='stagesButton handcursor' data-id='" + data.d[i].T_ID + "' data-name='" + data.d[i].T_NAME + "' name='submitButton' id='btnStages' value='Task Stages' style='margin-right:5px;margin-left:5px'/>" + "</td>" +
                    "<td style='text-align:center;'>" + "<img src='../../Images/clients.png' alt='Cleints' class='clientsButton handcursor' data-id='" + data.d[i].T_ID + "' data-name='" + data.d[i].T_NAME + "' name='submitButton' id='btnClients' value='Clients' style='margin-right:5px;margin-left:5px'/>" + "</td></tr>");

            }
            $('#tablemain').append("</tbody>");
            $('#tablemain').DataTable({
                "order": [[0, "asc"]]
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

function loadtempsearchclientgrid() {
    $('#tableclientsearch tbody').remove();
    $('#tableclientsearch').append("<tbody>");
    for (var i = 0; i < tempclientlstobj.length; i++) {
        $('#tableclientsearch').append(
            "<tr><td style='text-align:center;color:brown'><b>" + tempclientlstobj[i].C_NO + "</b></td><td>" + tempclientlstobj[i].FILE_NO + "</td><td style='color:blue'>" + tempclientlstobj[i].C_NAME + "</td>" +
            "<td>" + tempclientlstobj[i].PAN + "</td><td>" + tempclientlstobj[i].AADHAAR + "</td><td>" + tempclientlstobj[i].GSTIN + "</td>" +
            "<td style='text-align: center'><img src='../../Images/select.png' alt='Select Record' class='selectButtonSubis handcursor' data-id='" + tempclientlstobj[i].C_ID + '_' + tempclientlstobj[i].GENID + "' id='btnselectSubIS_" + tempclientlstobj[i].GENID + "' value='Select' style='margin-right:5px;margin-left:5px'/> </td></tr>");

    }
    $('#tableclientsearch').append("</tbody>");

    $("div.mhs h4").html("Search results for " + $('#SEARCHBY').val() + " : " + $('#SEARCHTEXT').val());

    $('#PopupModalClientSearch').modal('show');
    $('#PopupModalClientSearch').focus();

    $(".selectButtonSubis").click(function () {
        var id = this.id.split("_");
        for (var i = 0; i < tempclientlstobj.length; i++) {
            if (tempclientlstobj[i].GENID == id[1]) {
                clientobj = {};
                clientobj.GENID = tempclientlstobj[i].GENID;
                clientobj.C_ID = tempclientlstobj[i].C_ID;
                clientobj.C_NO = tempclientlstobj[i].C_NO;
                clientobj.FILE_NO = tempclientlstobj[i].FILE_NO;
                clientobj.C_NAME = tempclientlstobj[i].C_NAME;
                clientobj.PAN = tempclientlstobj[i].PAN;
                clientobj.AADHAAR = tempclientlstobj[i].AADHAAR;
                clientobj.GSTIN = tempclientlstobj[i].GSTIN;
                clientobj.PH_NO = tempclientlstobj[i].PH_NO;
                clientobj.MOBILE_NO1 = tempclientlstobj[i].MOBILE_NO1;
                clientobj.MOBILE_NO2 = tempclientlstobj[i].MOBILE_NO2;
                clientobj.CLI_GRP_NAME = tempclientlstobj[i].CLI_GRP_NAME;
                clientlstobj.push(clientobj);
                break;
            }
        }
        rebuildclientsubtable();
        $('#PopupModalClientSearch').modal('hide');
        $("#SEARCHTEXT").focus();
    });

}

function rebuildclientsubtable() {

    $('#griddivDetClientList').remove();
    $('#detdivclientdetails').append("<div class='table-responsive' id='griddivDetClientList'></div>");
    $('#griddivDetClientList').append("<table id='tableDetClientList' class='table table-striped table-bordered' style='width: 100%'></table>");
    $('#tableDetClientList').append("<thead><tr><th>C No</th><th>Name</th><th>File No</th><th></th></tr></thead><tbody></tbody>");
    $('#tableDetClientList tbody').remove();
    $('#tableDetClientList').append("<tbody>");
    for (var i = 0; i < clientlstobj.length; i++) {
        $('#tableDetClientList').append(
            "<tr><td style='text-align:center;color:brown'><b>" + clientlstobj[i].C_NO + "</b></td>" +
            "<td style='color:blue'><b>" + clientlstobj[i].C_NAME + "<b></td>" +
            "<td style='text-align:center;color:green'>" + clientlstobj[i].FILE_NO + "</td>" +
            "<td style='text-align:center;'><img src='../../Images/delete.png' alt='Delete Record' class='deleteclientButton handcursor' data-id='" + clientlstobj[i].GENID + "' name='submitButton' id='btncliDelete' value='Delete' style='margin-right:5px;margin-left:5px'/> </td></tr>");
    }
    $('#tableDetClientList').append("</tbody>");
    $('#tableDetClientList').DataTable({
        "order": [[0, "asc"]]
    });

    $(".deleteclientButton").click(function () {
        if (confirm("Are you sure you want to delete !") == true) {
            var id = $(this).attr("data-id");
            var newsubItemsList = [];
            for (var i = 0; i < clientlstobj.length; i++) {
                if (clientlstobj[i].GENID != id) {
                    newsubItemsList.push(clientlstobj[i]);
                }
            }
            clientlstobj = newsubItemsList;
            rebuildclientsubtable();
        }
    });
}

function recurringcombochange() {
    $("#RECURRING_DAYS").prop('disabled', true);
    $("#RECURRING_START_DAY").prop('disabled', true);
    $("#RECURRING_END_DATE").prop('disabled', true);
    if ($('#RECURRING_TYPE').val() == 'Once') {
        $("#RECURRING_DAYS").val(1);
        $("#RECURRING_START_DAY").val(1);
        var dobday = new Date(2050, 0, 1);
        $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
        $('#RECURRING_END_DATE').datepicker('setDate', dobday);
        $("#RECURRING_DAYS").prop('disabled', true);
        $("#RECURRING_START_DAY").prop('disabled', true);
        $("#RECURRING_END_DATE").prop('disabled', true);
    }
    else if ($('#RECURRING_TYPE').val() == 'Weekly') {
        $("#RECURRING_DAYS").val(7);
        $("#RECURRING_START_DAY").val(1);
        $("#RECURRING_DAYS").prop('disabled', true);
        $("#RECURRING_START_DAY").prop('disabled', false);
        $("#RECURRING_END_DATE").prop('disabled', false);
    }
    else if ($('#RECURRING_TYPE').val() == 'Bi-Monthly') {
        $("#RECURRING_DAYS").val(15);
        $("#RECURRING_START_DAY").val(1);
        $("#RECURRING_DAYS").prop('disabled', true);
        $("#RECURRING_START_DAY").prop('disabled', false);
        $("#RECURRING_END_DATE").prop('disabled', false);
    }
    else if ($('#RECURRING_TYPE').val() == 'Monthly') {
        $("#RECURRING_DAYS").val(30);
        $("#RECURRING_START_DAY").val(1);
        $("#RECURRING_DAYS").prop('disabled', true);
        $("#RECURRING_START_DAY").prop('disabled', false);
        $("#RECURRING_END_DATE").prop('disabled', false);
    }
    else if ($('#RECURRING_TYPE').val() == 'Quarterly') {
        $("#RECURRING_DAYS").val(90);
        $("#RECURRING_START_DAY").val(1);
        $("#RECURRING_DAYS").prop('disabled', true);
        $("#RECURRING_START_DAY").prop('disabled', false);
        $("#RECURRING_END_DATE").prop('disabled', false);
    }
    else if ($('#RECURRING_TYPE').val() == 'Bi-Yearly') {
        $("#RECURRING_DAYS").val(180);
        $("#RECURRING_START_DAY").val(1);
        $("#RECURRING_DAYS").prop('disabled', true);
        $("#RECURRING_START_DAY").prop('disabled', false);
        $("#RECURRING_END_DATE").prop('disabled', false);
    }
    else if ($('#RECURRING_TYPE').val() == 'Yearly') {
        $("#RECURRING_DAYS").val(365);
        $("#RECURRING_START_DAY").val(1);
        $("#RECURRING_DAYS").prop('disabled', true);
        $("#RECURRING_START_DAY").prop('disabled', false);
        $("#RECURRING_END_DATE").prop('disabled', false);
    }
    else if ($('#RECURRING_TYPE').val() == 'Custom') {
        $("#RECURRING_DAYS").val(1);
        $("#RECURRING_START_DAY").val(1);
        $("#RECURRING_DAYS").prop('disabled', false);
        $("#RECURRING_START_DAY").prop('disabled', false);
        $("#RECURRING_END_DATE").prop('disabled', false);
    }
}

function clearcontrols() {
    
    stageslstobj = [];
    stagesobj = {};
    stagesobj.SLNO = 1;
    stagesobj.USER_ID = 0;
    stagesobj.USER = "";
    stagesobj.STAGE = "";
    stagesobj.GENID = Math.floor((Math.random() * 10000000) + 1);
    stageslstobj.push(stagesobj);
    loadstagescontrols();

    clientlstobj = [];
    rebuildclientsubtable();

    $('#T_NAME').val('');
    $('#PRIORITY').val(1);
    $('#T_DESC').val('');
    $('#RECURRING_TYPE').val('');
    $('#RECURRING_DAYS').val(1);
    $('#RECURRING_START_DAY').val(1);
    var dobday = new Date(2050, 0, 1);
    $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
    $('#RECURRING_END_DATE').datepicker('setDate', dobday);
    $("#ACTIVE_STATUS").prop('checked', true);
    $('#SEARCHBY').val('NAME');
    $('#SEARCHTEXT').val('');
    $("#cmb_CLI_CAT").val('default').selectpicker("refresh");
    recurringcombochange();
}

function LoadClientCategoryCombo(data) {
    var options = [];
   for (var i = 0; i < data.d.length; i++) {
        options.push('<option value="',
          data.d[i].CLI_CAT_ID, '">',
          data.d[i].CLI_CAT_NAME, '</option>');
    }
    $("#cmb_CLI_CAT").html(options.join(''));
    $("#cmb_CLI_CAT").addClass("selectpicker");
    $("#cmb_CLI_CAT").addClass("form-control");
    $('.selectpicker').selectpicker('');   
}

function searchclients() {
    if ($("#SEARCHBY").val().trim() == "") {
        alert("Please select searchby.");
        $("#SEARCHBY").focus();
        return false;
    }

    if ($("#SEARCHTEXT").val().trim() == "") {
        alert("Please enter search text.");
        $("#SEARCHTEXT").focus();
        return false;
    }
    document.getElementById("loader").style.display = "block";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "TaskMaster.aspx/GetClientSearchList",
        data: '{filterby: ' + JSON.stringify($("#SEARCHBY").val()) + ',filtertext: ' + JSON.stringify($("#SEARCHTEXT").val()) + '}',
        dataType: "json",
        success: function (data) {
            if (data.d.length > 0) {

                tempclientlstobj = [];
                for (var i = 0; i < data.d.length; i++) {
                    clientobj = {};
                    clientobj.GENID = Math.floor((Math.random() * 10000000) + 1);
                    clientobj.C_ID = data.d[i].C_ID;
                    clientobj.C_NO = data.d[i].C_NO;
                    clientobj.FILE_NO = data.d[i].FILE_NO;
                    clientobj.C_NAME = data.d[i].C_NAME;
                    clientobj.PH_NO = data.d[i].PH_NO;
                    clientobj.MOBILE_NO1 = data.d[i].MOBILE_NO1;
                    clientobj.MOBILE_NO2 = data.d[i].MOBILE_NO2;
                    clientobj.PAN = data.d[i].PAN;
                    clientobj.AADHAAR = data.d[i].AADHAAR;
                    clientobj.GSTIN = data.d[i].GSTIN;
                    clientobj.CLI_GRP_NAME = data.d[i].CLI_GRP_NAME;
                    tempclientlstobj.push(clientobj);
                }
                loadtempsearchclientgrid();
                $("#SEARCHTEXT").val('');
            }
            else {
                alert('No data found for the search criteria');
                $("#SEARCHTEXT").focus();
            }

            document.getElementById("loader").style.display = "none";
        },
        error: function () {
            alert("Error while Showing update data");
        }

        //
    });

}

$(function () {

    $(document).on("click", ".deleteButton", function () {
        if (confirm("Are you sure you want to delete !") == true) {
            var id = $(this).attr("data-id");
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "TaskMaster.aspx/DeleteData",
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

        $("#subheaderdiv").html("<h3 style='color:blue'>Task Master -> Add Task Details</h3>");

        clearcontrols();

        $('#T_NAME').focus();
    });

    $(document).on("click", "#addstage", function () {
        
        stagesobj = {};
        stagesobj.SLNO = stageslstobj.length + 1;
        stagesobj.USER_ID = 0;
        stagesobj.USER = "";
        stagesobj.STAGE = "";
        stagesobj.GENID = Math.floor((Math.random() * 10000000) + 1);
        stageslstobj.push(stagesobj);
        loadstagescontrols();
    });

    $(document).on("click", ".cancelButton", function () {
        $('#mainlistingdiv').show();
        $('#mainldetaildiv').hide();
    });

    $(document).on("click", ".stagesButton", function () {
        document.getElementById("loader").style.display = "block";
        $('#PopupModalStageDetails').modal('show');
        $('#PopupModalStageDetails').focus();
        var id = $(this).attr("data-id");
        var tskname = $(this).attr("data-name");
        console.log(id);
        $("#divstageheader h4").html("Task Stages -> Task : " + tskname);

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "TaskMaster.aspx/GetTaskMasterSatgesListById",
            data: '{id: ' + id + '}',
            dataType: "json",
            success: function (data) {
                $('#griddivStageDetails').remove();
                $('#maindivstagedetails').append("<div class='table-responsive' id='griddivStageDetails'></div>");
                $('#griddivStageDetails').append("<table id='tableStageDetails' class='table table-striped table-bordered' style='width: 100%'></table>");
                $('#tableStageDetails').append("<thead><tr><th>Sl No</th><th>Stage Name</th><th>User Assigned</th></tr></thead><tbody></tbody>");
                $('#tableStageDetails tbody').remove();
                $('#tableStageDetails').append("<tbody>");
                for (var i = 0; i < data.d.length; i++) {
                    $('#tableStageDetails').append(
                        "<tr><td style='text-align:center;color:brown'><b>" + data.d[i].SL_NO + "</b></td>" +
                        "<td style='color:red'><b>" + data.d[i].TS_NAME + "</b></td>" +
                        "<td style='text-align:center;color:blue'>" + data.d[i].NAME + "</td></tr>");
                }
                $('#tableStageDetails').append("</tbody>");
                $('#tableStageDetails').DataTable({
                    "order": [[0, "asc"]]
                });
                document.getElementById("loader").style.display = "none";
            },
            error: function () {
                alert("Error while Showing update data");
            }
        });
    });

    $(document).on("click", ".clientsButton", function () {
        document.getElementById("loader").style.display = "block";
        $('#PopupModalClientList').modal('show');
        $('#PopupModalClientList').focus();
        var id = $(this).attr("data-id");
        var tskname = $(this).attr("data-name");
        console.log(id);
        $("#divclientheader h4").html("Clients Mapped to Task -> " + tskname);
        $('#griddivClientList').remove();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "TaskMaster.aspx/GetTaskMasterClientListById",
            data: '{id: ' + id + '}',
            dataType: "json",
            success: function (data) {                
                $('#maindivclientdetails').append("<div class='table-responsive' id='griddivClientList'></div>");
                $('#griddivClientList').append("<table id='tableClientList' class='table table-striped table-bordered' style='width: 100%'></table>");
                $('#tableClientList').append("<thead><tr><th>C No</th><th>Name</th><th>File No</th><th>PAN</th></tr></thead><tbody></tbody>");
                $('#tableClientList tbody').remove();
                $('#tableClientList').append("<tbody>");
                for (var i = 0; i < data.d.length; i++) {
                    $('#tableClientList').append(
                        "<tr><td style='text-align:center;color:brown'><b>" + data.d[i].C_NO + "</b></td>" +
                        "<td style='color:bluse'>" + data.d[i].C_NAME + "</td>" +
                        "<td style='text-align:center;color:red'>" + data.d[i].FILE_NO + "</td>" +
                        "<td style='color:black'>" + data.d[i].PAN + "</td></tr>");
                }
                $('#tableClientList').append("</tbody>");
                $('#tableClientList').DataTable({
                    "order": [[0, "asc"]]
                });
                document.getElementById("loader").style.display = "none";
            },
            error: function () {
                alert("Error while Showing update data");
            }
        });
    });

    $(document).on("click", "#btnsearchClient", function () {
        searchclients();
    });

    $("#btnSave").click(function () {

        if ($("#T_NAME").val().trim() == "") {
            alert("Please enter Task name.");
            $("#T_NAME").focus();
            return false;
        }

        if ($("#PRIORITY").val().trim() == "" && $("#PRIORITY").val() <=0) {
            alert("Please enter Priority greater than Zero.");
            $("#PRIORITY").focus();
            return false;
        }

        if ($("#T_DESC").val().trim() == "") {
            alert("Please enter Task Description.");
            $("#T_DESC").focus();
            return false;
        }

        if ($("#RECURRING_TYPE").val() == null || $("#RECURRING_TYPE").val() == undefined || $.trim($("#RECURRING_TYPE").val()) == '') {
            alert('Please select Task Occurance.');
            $("#RECURRING_TYPE").focus();
            return false;
        }

        if ($("#RECURRING_TYPE").val() != null && $("#RECURRING_TYPE").val() != undefined || $.trim($("#RECURRING_TYPE").val()) != '' && $.trim($("#RECURRING_TYPE").val()) != 'Once') {
                        
            if ($("#RECURRING_TYPE").val() == 'Custom' && $("#RECURRING_DAYS").val().trim() == "" && $("#RECURRING_DAYS").val() <= 0)
            {
                alert('Please enter Task occurrance frequency days.');
                $("#RECURRING_DAYS").focus();
                return false;
            }

            if ($("#RECURRING_START_DAY").val().trim() == "" && $("#RECURRING_START_DAY").val() <= 0) {
                alert('Please enter Task occuring day.');
                $("#RECURRING_START_DAY").focus();
                return false;
            }

            if ($("#RECURRING_END_DATE").val() == null || $("#RECURRING_END_DATE").val() == undefined || $.trim($("#RECURRING_END_DATE").val()) == '') {
                alert('Please select Task end date.');
                $("#RECURRING_END_DATE").focus();
                return false;
            }

        }

        var clientCategoryStringList = [];
        $('#cmb_CLI_CAT > option:selected').each(function () {
            clientCategoryStringList.push($(this).val());
        });

        //if (clientCategoryStringList == undefined || clientCategoryStringList == null || clientCategoryStringList.length <= 0) {
        //    alert('Please select atleast one Category.');
        //    $("#cmb_CLI_CAT").focus();
        //    return false;
        //}

        if (stageslstobj == undefined || stageslstobj == null || stageslstobj.length <= 0) {
            alert("Please add atleast one Stage.");
            $("#addstage").focus();
            return false;
        }

        for (var i = 0; i < stageslstobj.length; i++) {
            if ($("#txtstages" + stageslstobj[i].GENID).val().trim() == "") {
                alert("Please select Stage name.");
                $("#txtstages" + stageslstobj[i].GENID).focus();
                return false;
            }
            if ($("#cmbusers" + stageslstobj[i].GENID).val() == undefined || $("#cmbusers" + stageslstobj[i].GENID).val() == null || $("#cmbusers" + stageslstobj[i].GENID).val().trim() == "" || $("#cmbusers" + stageslstobj[i].GENID).val() <= 0) {
                alert("Please select User for the stage : " + $("#txtstages" + stageslstobj[i].GENID).val() + ".");
                $("#cmbusers" + stageslstobj[i].GENID).focus();
                return false;
            }
        }

        var obj = {};
        obj.T_NAME = $.trim($("#T_NAME").val());
        obj.T_DESC = $.trim($("#T_DESC").val());
        obj.PRIORITY = $("#PRIORITY").val();
        obj.RECURRING_TYPE = $("#RECURRING_TYPE").val();
        obj.RECURRING_DAYS = $("#RECURRING_DAYS").val();
        obj.RECURRING_START_DAY = $("#RECURRING_START_DAY").val();
        obj.RECURRING_END_DATE = $("#RECURRING_END_DATE").val();
        if ($('#ACTIVE_STATUS').is(":checked")) {
            obj.ACTIVE_STATUS = true;
        }
        else {
            obj.ACTIVE_STATUS = false;
        }

        obj.MAPPED_CLI_CAT = clientCategoryStringList.join(',');

        var mappedclientList = [];
        for (var i = 0; i < clientlstobj.length; i++) {
            mappedclientList.push(clientlstobj[i].C_ID);            
        }
        obj.MAPPED_CLIENTS = mappedclientList.join(',');
        
        obj.SUBARR = stageslstobj;

        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "TaskMaster.aspx/InsertData",
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
                        $("#T_NAME").focus();
                        return false;
                    }
                }
            },
            error: function (data) {
                alert("Error while Adding data of :" + obj.NAME);
                $("#T_NAME").focus();
                return false;
            }
        });

    });

    $("#btnUpdate").click(function () {
        if ($("#T_NAME").val().trim() == "") {
            alert("Please enter Task name.");
            $("#T_NAME").focus();
            return false;
        }

        if ($("#PRIORITY").val().trim() == "" && $("#PRIORITY").val() <= 0) {
            alert("Please enter Priority greater than Zero.");
            $("#PRIORITY").focus();
            return false;
        }

        if ($("#T_DESC").val().trim() == "") {
            alert("Please enter Task Description.");
            $("#T_DESC").focus();
            return false;
        }

        if ($("#RECURRING_TYPE").val() == null || $("#RECURRING_TYPE").val() == undefined || $.trim($("#RECURRING_TYPE").val()) == '') {
            alert('Please select Task Occurance.');
            $("#RECURRING_TYPE").focus();
            return false;
        }

        if ($("#RECURRING_TYPE").val() != null && $("#RECURRING_TYPE").val() != undefined || $.trim($("#RECURRING_TYPE").val()) != '' && $.trim($("#RECURRING_TYPE").val()) != 'Once') {

            if ($("#RECURRING_TYPE").val() == 'Custom' && $("#RECURRING_DAYS").val().trim() == "" && $("#RECURRING_DAYS").val() <= 0) {
                alert('Please enter Task occurrance frequency days.');
                $("#RECURRING_DAYS").focus();
                return false;
            }

            if ($("#RECURRING_START_DAY").val().trim() == "" && $("#RECURRING_START_DAY").val() <= 0) {
                alert('Please enter Task occuring day.');
                $("#RECURRING_START_DAY").focus();
                return false;
            }

            if ($("#RECURRING_END_DATE").val() == null || $("#RECURRING_END_DATE").val() == undefined || $.trim($("#RECURRING_END_DATE").val()) == '') {
                alert('Please select Task end date.');
                $("#RECURRING_END_DATE").focus();
                return false;
            }

        }

        var clientCategoryStringList = [];
        $('#cmb_CLI_CAT > option:selected').each(function () {
            clientCategoryStringList.push($(this).val());
        });

        //if (clientCategoryStringList == undefined || clientCategoryStringList == null || clientCategoryStringList.length <= 0) {
        //    alert('Please select atleast one Category.');
        //    $("#cmb_CLI_CAT").focus();
        //    return false;
        //}

        if (stageslstobj == undefined || stageslstobj == null || stageslstobj.length <= 0) {
            alert("Please add atleast one Stage.");
            $("#addstage").focus();
            return false;
        }

        for (var i = 0; i < stageslstobj.length; i++) {
            if ($("#txtstages" + stageslstobj[i].GENID).val().trim() == "") {
                alert("Please select Stage name.");
                $("#txtstages" + stageslstobj[i].GENID).focus();
                return false;
            }
            if ($("#cmbusers" + stageslstobj[i].GENID).val() == undefined || $("#cmbusers" + stageslstobj[i].GENID).val() == null || $("#cmbusers" + stageslstobj[i].GENID).val().trim() == "" || $("#cmbusers" + stageslstobj[i].GENID).val() <= 0) {
                alert("Please select User for the stage : " + $("#txtstages" + stageslstobj[i].GENID).val() + ".");
                $("#cmbusers" + stageslstobj[i].GENID).focus();
                return false;
            }
        }

        var id = $(this).attr("edit-id");

        var obj = {};
        obj.T_ID = id;
        obj.T_NAME = $.trim($("#T_NAME").val());
        obj.T_DESC = $.trim($("#T_DESC").val());
        obj.PRIORITY = $("#PRIORITY").val();
        obj.RECURRING_TYPE = $("#RECURRING_TYPE").val();
        obj.RECURRING_DAYS = $("#RECURRING_DAYS").val();
        obj.RECURRING_START_DAY = $("#RECURRING_START_DAY").val();
        obj.RECURRING_END_DATE = $("#RECURRING_END_DATE").val();
        if ($('#ACTIVE_STATUS').is(":checked")) {
            obj.ACTIVE_STATUS = true;
        }
        else {
            obj.ACTIVE_STATUS = false;
        }
        obj.MAPPED_CLI_CAT = clientCategoryStringList.join(',');

        var mappedclientList = [];
        for (var i = 0; i < clientlstobj.length; i++) {
            mappedclientList.push(clientlstobj[i].C_ID);
        }
        obj.MAPPED_CLIENTS = mappedclientList.join(',');

        obj.SUBARR = stageslstobj;

        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "TaskMaster.aspx/UpdateData",
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
                        $("#T_NAME").focus();
                        return false;
                    }
                }
            },
            error: function (data) {
                alert("Error while Updating data of :" + id);
                $("#T_NAME").focus();
                return false;
            }
        });
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

        clearcontrols();
        $("#subheaderdiv").html("<h3 style='color:blue'>Task Master -> Edit Task Details</h3>");
        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "TaskMaster.aspx/EditData",
            data: '{id: ' + id + '}',
            dataType: "json",
            success: function (data) {
                if (data.d.length > 0) {
                    if (data.d[0].MainArray != null && data.d[0].MainArray != undefined && data.d[0].MainArray.length > 0)
                    {
                        $("#subheaderdiv").html("<h3 style='color:blue'>Task Master -> Edit Task Details -> Task : " + data.d[0].MainArray[0].T_NAME + "</h3>");
                        $("#T_NAME").val(data.d[0].MainArray[0].T_NAME);

                        $("#PRIORITY").val(data.d[0].MainArray[0].PRIORITY);
                        $("#T_DESC").val(data.d[0].MainArray[0].T_DESC);
                        $("#RECURRING_TYPE").val(data.d[0].MainArray[0].RECURRING_TYPE);
                        recurringcombochange();
                        $("#RECURRING_DAYS").val(data.d[0].MainArray[0].RECURRING_DAYS);
                        $("#RECURRING_START_DAY").val(data.d[0].MainArray[0].RECURRING_START_DAY);                        
                        $('#RECURRING_END_DATE').datepicker({ dateFormat: 'dd-mm-yy' }).datepicker('setDate', data.d[0].MainArray[0].RECURRING_END_DATE.split('-')[2] + '-' + data.d[0].MainArray[0].RECURRING_END_DATE.split('-')[1] + '-' + data.d[0].MainArray[0].RECURRING_END_DATE.split('-')[0]);

                        if (data.d[0].MainArray[0].ACTIVE_STATUS == true)
                            $("#ACTIVE_STATUS").prop('checked', true);
                        else
                            $("#ACTIVE_STATUS").prop('checked', false);

                    }

                    if (data.d[0].MainArray != null && data.d[0].MainArray != undefined && data.d[0].ClientCategoryMapArray.length > 0) {
                        var array = [];
                        for (var i = 0; i < data.d[0].ClientCategoryMapArray.length; i++)
                        {
                            array.push(data.d[0].ClientCategoryMapArray[i].CLI_CAT_ID);
                        }                        
                        $('#cmb_CLI_CAT').selectpicker('val', array);
                    }

                    if (data.d[0].MainArray != null && data.d[0].MainArray != undefined && data.d[0].SubArray.length > 0) {                        
                        stageslstobj = [];                        
                        for (var i = 0; i < data.d[0].SubArray.length; i++) {                            
                            stagesobj = {};
                            stagesobj.SLNO = data.d[0].SubArray[i].SL_NO;
                            stagesobj.USER_ID = data.d[0].SubArray[i].USER_ID;
                            stagesobj.USER = data.d[0].SubArray[i].NAME;
                            stagesobj.STAGE = data.d[0].SubArray[i].TS_NAME;
                            stagesobj.GENID = Math.floor((Math.random() * 10000000) + 1);
                            stageslstobj.push(stagesobj);
                        }

                        loadstagescontrols();
                    }

                    if (data.d[0].MainArray != null && data.d[0].MainArray != undefined && data.d[0].ClientMapArray.length > 0) {
                        clientlstobj = [];
                        for (var i = 0; i < data.d[0].ClientMapArray.length; i++) {                            
                            clientobj = {};
                            clientobj.GENID = Math.floor((Math.random() * 10000000) + 1);
                            clientobj.C_ID = data.d[0].ClientMapArray[i].C_ID;
                            clientobj.C_NO = data.d[0].ClientMapArray[i].C_NO;
                            clientobj.FILE_NO = data.d[0].ClientMapArray[i].FILE_NO;
                            clientobj.C_NAME = data.d[0].ClientMapArray[i].C_NAME;
                            clientobj.PAN = data.d[0].ClientMapArray[i].PAN;
                            clientobj.AADHAAR = data.d[0].ClientMapArray[i].AADHAAR;
                            clientobj.GSTIN = data.d[0].ClientMapArray[i].GSTIN;
                            clientobj.PH_NO = data.d[0].ClientMapArray[i].PH_NO;
                            clientobj.MOBILE_NO1 = data.d[0].ClientMapArray[i].MOBILE_NO1;
                            clientobj.MOBILE_NO2 = data.d[0].ClientMapArray[i].MOBILE_NO2;
                            clientobj.CLI_GRP_NAME = data.d[0].ClientMapArray[i].CLI_GRP_NAME;
                            clientlstobj.push(clientobj);
                        }
                        rebuildclientsubtable();
                    }                        
                }                    
                
                $('#T_NAME').focus();
                document.getElementById("loader").style.display = "none";
            },
            error: function () {
                alert("Error while retrieving data of :" + id);
            }
        });
    });

});


