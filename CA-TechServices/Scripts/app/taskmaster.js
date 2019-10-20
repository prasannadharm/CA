var stagescount = 0;
var stageslstobj = [];
var stagesobj = {};
var clientlstobj = [];
var clientobj = {};
var tempclientlstobj = [];
$(document).ready(function () {
    var dobday = new Date(2050, 0, 1);
    $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
    $('#RECURRING_END_DATE').datepicker('setDate', dobday);

    document.getElementById("loader").style.display = "block";
    getMainGridDetails();


    loaddatalist();
});

function loadstagescontrols() {
    $("#StagesGroup").empty();
    for (var i = 0; i < stageslstobj.length; i++) {
        var newTextBoxDiv = $(document.createElement('div'))
            .attr("id", 'StagesDiv' + stageslstobj[i].GENID);

        newTextBoxDiv.after().html("<label id='lblslno" + stageslstobj[i].GENID + "' style='margin-left: 10px; display: inline; text-align: center;font-weight: bold;color:brown'>" + stageslstobj[i].SLNO + "</label>" +
                                        "<input id='txtstages" + stageslstobj[i].GENID + "' list='lststages' class='form-control txtstages' style='margin-left: 10px; width: 40%; display: inline; margin-bottom: 5px' placeholder='Select Stage' value='" + stageslstobj[i].STAGE + "'data-id='" + stageslstobj[i].GENID + "'/>" +
                                        "<input id='txtusers" + stageslstobj[i].GENID + "' list='lstusers' class='form-control txtusers' style='margin-left: 10px; width: 25%; display: inline; margin-bottom: 5px' placeholder='Select User' value='" + stageslstobj[i].USER + "' data-id='" + stageslstobj[i].GENID + "'/>" +
                                        "<img id='btnstageup" + stageslstobj[i].GENID + "' class='btnstageup handcursor' src='../../Images/up.png' data-id='" + stageslstobj[i].GENID + "'/>" +
                                        "<img id='btnstagedown" + stageslstobj[i].GENID + "' class='btnstagedown handcursor' src='../../Images/down.png' data-id='" + stageslstobj[i].GENID + "'/>" +
                                        "<img id='btnstaedel" + stageslstobj[i].GENID + "' class='btnstagedel handcursor' src='../../Images/delete.png' style='margin-left:8px' data-id='" + stageslstobj[i].GENID + "'/>");

        newTextBoxDiv.appendTo("#StagesGroup");
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

    $('.txtusers').on('input', function () {
        var id = $(this).attr("data-id");
        for (var i = 0; i < stageslstobj.length; i++) {
            if (stageslstobj[i].GENID == id) {
                stageslstobj[i].USER = $(this).val();
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
            var options = [];
            for (var i = 0; i < data.d.length; i++) {
                options.push('<option value="',
                  data.d[i].NAME, '"/>');
            }
            $("#lstusers").html(options.join(''));
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
                    "<tr><td style='text-align:center;color:brown'><b>" + data.d[i].T_NAME + "</b></td>" +
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
            "<tr><td style='text-align:center;color:brown'><b>" + tempclientlstobj[i].C_ID + "</b></td><td>" + tempclientlstobj[i].FILE_NO + "</td><td style='color:blue'>" + tempclientlstobj[i].C_NAME + "</td>" +
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
    $('#tableDetClientList').append("<thead><tr><th>C ID</th><th>Name</th><th>File No</th><th></th></tr></thead><tbody></tbody>");
    $('#tableDetClientList tbody').remove();
    $('#tableDetClientList').append("<tbody>");
    for (var i = 0; i < clientlstobj.length; i++) {
        $('#tableDetClientList').append(
            "<tr><td style='text-align:center;color:brown'><b>" + clientlstobj[i].C_ID + "</b></td>" +
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

        stagescount = 1;
        stageslstobj = [];
        stagesobj = {};
        stagesobj.SLNO = 1;
        stagesobj.USER = "";
        stagesobj.STAGE = "";
        stagesobj.GENID = Math.floor((Math.random() * 10000000) + 1);
        stageslstobj.push(stagesobj);
        loadstagescontrols();
        $('#T_NAME').focus();
    });

    $(document).on("click", "#addstage", function () {
        stagescount = stagescount + 1;
        stagesobj = {};
        stagesobj.SLNO = stageslstobj.length + 1;
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
                        "<tr><td style='text-align:center;color:brown'>" + data.d[i].SL_NO + "</td>" +
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

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "TaskMaster.aspx/GetTaskMasterClientListById",
            data: '{id: ' + id + '}',
            dataType: "json",
            success: function (data) {
                $('#griddivClientList').remove();
                $('#maindivclientdetails').append("<div class='table-responsive' id='griddivClientList'></div>");
                $('#griddivClientList').append("<table id='tableClientList' class='table table-striped table-bordered' style='width: 100%'></table>");
                $('#tableClientList').append("<thead><tr><th>C ID</th><th>Name</th><th>File No</th><th>PAN</th></tr></thead><tbody></tbody>");
                $('#tableClientList tbody').remove();
                $('#tableClientList').append("<tbody>");
                for (var i = 0; i < data.d.length; i++) {
                    $('#tableClientList').append(
                        "<tr><td style='text-align:center;color:brown'><b>" + data.d[i].C_ID + "</b></td>" +
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
                        clientobj.FILE_NO = data.d[i].FILE_NO;
                        clientobj.C_NAME = data.d[i].C_NAME;
                        clientobj.PH_NO = data.d[i].PH_NO;
                        clientobj.MOBILE_NO1 = data.d[i].MOBILE_NO1;
                        clientobj.MOBILE_NO2 = data.d[i].MOBILE_NO2;
                        clientobj.PAN = data.d[i].PAN;
                        clientobj.AADHAAR = data.d[i].AADHAAR;
                        clientobj.GSTIN = data.d[i].GSTIN;
                        clientobj.CLI_GRP_NAME = data.d[i].CLI_GRP_NAME;
                        clientobj.CLI_CAT_NAME = data.d[i].CLI_CAT_NAME;
                        tempclientlstobj.push(clientobj);
                    }
                    loadtempsearchclientgrid();
                    $("#SEARCHTEXT").val('');
                }
                else
                {
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

    });

});


