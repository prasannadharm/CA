
$(document).ready(function () {

    $("#masterdiv").removeClass("container");
    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
    $('#dtpFrom').datepicker('setDate', today);
    $('#dtpTo').datepicker('setDate', today);

    $.ajax({
        url: "CreateTask.aspx/GetLatestTrasnsactionNumber",
        data: '{}',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            for (var i = 0; i < data.d.length; i++) {
                $('#dtpFrom').datepicker({ dateFormat: 'dd-mm-yy' }).datepicker('setDate', data.d[i].split('-')[3] + '-' + data.d[i].split('-')[2] + '-' + data.d[i].split('-')[1]);
                $('#dtpTo').datepicker({ dateFormat: 'dd-mm-yy' }).datepicker('setDate', data.d[i].split('-')[3] + '-' + data.d[i].split('-')[2] + '-' + data.d[i].split('-')[1]);
            }
            getMainGridDetails();
        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });

    $('#dtpFrom').keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            $('#dtpTo').focus();
        }
    });

    $('#dtpTo').keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            getMainGridDetails();
        }
    });

    $("#btnSearch").click(function () {
        getMainGridDetails();
    })

    $("#btnSearchPendingTasks").click(function () {
        getPendingTaskDetails();
    })

    $("#btnskippending").click(function () {
        $('#tasksdetailsmaindiv').show();
        $('#pendingtasksdivmain').hide();
    })

    $(document).on("click", ".cancelButton", function () {
        $('#mainlistingdiv').show();
        $('#maindetaildiv').hide();
        $('#tasksdetailsmaindiv').hide();
        $('#pendingtasksdivmain').hide();
    });
    
    LoadClientFilterCombo();
})

function getMainGridDetails() {

    if (isDate($("#dtpFrom").val()) == false) {
        alert('Please enter valid date');
        $("#dtpFrom").focus();
        return false;
    }

    if (isDate($("#dtpTo").val()) == false) {
        alert('Please enter valid date');
        $("#dtpTo").focus();
        return false;
    }

    var fit_start_time = $("#dtpFrom").val(); //2013-09-5
    fit_start_time = fit_start_time.substring(6, 10) + '-' + fit_start_time.substring(3, 5) + '-' + fit_start_time.substring(0, 2);
    var fit_end_time = $("#dtpTo").val(); //2013-09-10
    fit_end_time = fit_end_time.substring(6, 10) + '-' + fit_end_time.substring(3, 5) + '-' + fit_end_time.substring(0, 2);

    if (Date.parse(fit_start_time) > Date.parse(fit_end_time)) {
        alert("Date from must be lesser than Date to.");
        $("#dtpFrom").focus();
        return false;
    }

    document.getElementById("loader").style.display = "block";

    var obj = {};
    obj.DateFrom = $("#dtpFrom").val();
    obj.DateTo = $("#dtpTo").val();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "CreateTask.aspx/GetData",
        data: '{fromdate: ' + JSON.stringify($("#dtpFrom").val()) + ', todate: ' + JSON.stringify($("#dtpTo").val()) + '}',
        dataType: "json",
        success: function (data) {
            $('#griddiv').remove();
            $('#maindiv').append("<div class='table-responsive' id='griddiv'></div>");
            $('#griddiv').append("<table id='tablemain' class='table table-striped table-bordered' style='width: 100%'></table>");
            $('#tablemain').append("<thead><tr><th>Task No</th><th>Date</th><th>Task Name</th><th>Client</th><th>C No</th><th>File No</th><th>Sch On</th><th>Created By</th><th>Void</th><th></th><th></th><th></th></tr></thead><tbody></tbody>");
            $('#tablemain tbody').remove();
            $('#tablemain').append("<tbody>");
            for (var i = 0; i < data.d.length; i++) {
                $('#tablemain').append(
                    "<tr><td style='text-align:center;color:brown'><b>" + data.d[i].T_NO + "</b></td>" +
                    "<td>" + data.d[i].T_DATE + "</td>" +
                    "<td style='color:brown'><b>" + data.d[i].T_NAME + "</b></td>" +
                    "<td style='color:blue'><b>" + data.d[i].C_NAME + "</b></td>" +
                    "<td style='center;'>" + data.d[i].C_NO + "</td>" +
                    "<td style='center;'>" + data.d[i].FILE_NO + "</td>" +
                    "<td>" + data.d[i].SCH_ON + "</td>" +
                    "<td>" + data.d[i].CREATEDBY_NAME + "</td>" +
                    "<td style='text-align:center;'>" + "<input type='checkbox' onclick='return false;' " + (data.d[i].VOID_STATUS == true ? "checked='checked'" : "") + "/></td>" +
                    "<td><img src='../../Images/edit.png' alt='Edit Record' class='editButton handcursor' data-id='" + data.d[i].T_ID + "' name='submitButton' id='btnEdit' value='Edit' style='margin-right:5px'/>" + "</td>" +
                    "<td><img src='../../Images/delete.png' alt='Delete Record' class='deleteButton handcursor' data-id='" + data.d[i].T_ID + "' name='submitButton' id='btnDelete' value='Delete' style='margin-right:5px;margin-left:5px'/> </td>" +
                    "<td><img src='../../Images/void.png' alt='Void / Cancel Record' class='voidButton handcursor' data-id='" + data.d[i].T_ID + "' name='submitButton' id='btnVoid' value='Void' style='margin-right:5px;margin-left:5px'/> </td></tr>");                    
            }
            $('#tablemain').append("</tbody>");
            $('#tablemain').DataTable({
                "order": [[0, "desc"]]
            });
            document.getElementById("loader").style.display = "none";
        },
        error: function (request, status, error) {
            alert(request.responseText);
            alert("Error while Showing update data");
        }
    });
    //document.getElementById("loader").style.display = "none";
}

function isDate(txtDate) {
    var currVal = txtDate;
    if (currVal == '')
        return false;

    //Declare Regex 
    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var dtArray = currVal.match(rxDatePattern); // is format OK?

    if (dtArray == null)
        return false;

    //Checks for mm/dd/yyyy format.
    dtMonth = dtArray[3];
    dtDay = dtArray[1];
    dtYear = dtArray[5];

    if (dtMonth < 1 || dtMonth > 12)
        return false;
    else if (dtDay < 1 || dtDay > 31)
        return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return false;
    }
    return true;
}

function LoadClientFilterCombo() {
    var options = []; 
    $.ajax({
        url: "CreateTask.aspx/GetActiveClientListforDropdown",
        data: '{}',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            for (var i = 0; i < data.d.length; i++) {
                options.push('<option value="',
                  data.d[i].ID, '">',
                  data.d[i].NAME, '</option>');
            }

            $("#cmb_Client_Filter").html(options.join(''));
            $("#cmb_Client_Filter").addClass("selectpicker");
            $("#cmb_Client_Filter").addClass("form-control");
            LoadTaskFilterCombo();
        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });
    //$('.selectpicker').selectpicker('');    
}

function LoadTaskFilterCombo() {
    var options = [];
    $.ajax({
        url: "CreateTask.aspx/GetActiveTasksList",
        data: '{}',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            for (var i = 0; i < data.d.length; i++) {
                options.push('<option value="',
                  data.d[i].ID, '">',
                  data.d[i].NAME, '</option>');
            }

            $("#cmb_Task_Filter").html(options.join(''));
            $("#cmb_Task_Filter").addClass("selectpicker");
            $("#cmb_Task_Filter").addClass("form-control");
            LoadClientCategoryFilterCombo();
        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });
    //$('.selectpicker').selectpicker('');    
}

function LoadClientCategoryFilterCombo() {
    var options = [];
    $.ajax({
        url: "CreateTask.aspx/GetActiveClientCategories",
        data: '{}',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            for (var i = 0; i < data.d.length; i++) {
                options.push('<option value="',
                  data.d[i].CLI_CAT_ID, '">',
                  data.d[i].CLI_CAT_NAME, '</option>');
            }

            $("#cmb_Cli_Cat_Filter").html(options.join(''));
            $("#cmb_Cli_Cat_Filter").addClass("selectpicker");
            $("#cmb_Cli_Cat_Filter").addClass("form-control");
            $('.selectpicker').selectpicker('');
        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });   
}

function getPendingTaskDetails() {

    var clientCategoryStringList = [];
    $('#cmb_Cli_Cat_Filter > option:selected').each(function () {
        clientCategoryStringList.push($(this).val());
    });

    var clientStringList = [];
    $('#cmb_Client_Filter > option:selected').each(function () {
        clientStringList.push($(this).val());
    });

    var taskStringList = [];
    $('#cmb_Task_Filter > option:selected').each(function () {
        taskStringList.push($(this).val());
    });

    if ((clientCategoryStringList == undefined || clientCategoryStringList == null || clientCategoryStringList.length <= 0)
        && (clientStringList == undefined || clientStringList == null || clientStringList.length <= 0)
        && (taskStringList == undefined || taskStringList == null || taskStringList.length <= 0)) {
        alert('Please select atleast one filter criteria.');
        $("#cmb_Task_Filter").focus();
        return false;
    }


    document.getElementById("loader").style.display = "block";

    var obj = {};
    obj.DateFrom = $("#dtpFrom").val();
    obj.DateTo = $("#dtpTo").val();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "CreateTask.aspx/GetPendingTaskData",
        data: '{TIDSTR: ' + JSON.stringify(taskStringList.join(',')) + ', CIDSTR: ' + JSON.stringify(clientStringList.join(',')) + ', CLICATIDSTR: ' + JSON.stringify(clientCategoryStringList.join(',')) + '}',
        dataType: "json",
        success: function (data) {
            $('#gridpendingtaskdiv').remove();
            $('#pendingtaskdiv').append("<div class='table-responsive' id='gridpendingtaskdiv'></div>");
            $('#gridpendingtaskdiv').append("<table id='tablependingtask' class='table table-striped table-bordered' style='width: 100%'></table>");
            $('#tablependingtask').append("<thead><tr><th>Task Name</th><th>Priority</th><th>Frequency</th><th>Sch On</th><th>Client</th><th>C No</th><th>File No</th><th>PAN</th><th></th><th></th></tr></thead><tbody></tbody>");
            $('#tablependingtask tbody').remove();
            $('#tablependingtask').append("<tbody>");
            for (var i = 0; i < data.d.length; i++) {
                $('#tablependingtask').append(
                    "<tr><td style='color:brown'><b>" + data.d[i].T_NAME + "</b></td>" +
                    "<td style='text-align:center;'><b>" + data.d[i].PRIORITY + "</b></td>" +
                    "<td>" + data.d[i].RECURRING_TYPE + "</td>" +
                    "<td style='color:red'><b>" + data.d[i].TASK_SCH_DATE + "</b></td>" +
                    "<td style='color:blue'><b>" + data.d[i].C_NAME + "</b></td>" +
                    "<td style='center;'><b>" + data.d[i].C_NO + "</b></td>" +
                    "<td style='center;'>" + data.d[i].FILE_NO + "</td>" +
                    "<td>" + data.d[i].PAN + "</td>" +
                    "<td><img src='../../Images/select.png' alt='Select Record' class='selectpendingtaskButton handcursor' data-id='" + data.d[i].T_ID + "_" + data.d[i].C_ID + "_" + data.d[i].TASK_SCH_DATE + "' name='submitButton' id='btnSelectPendingTask' value='Select' style='margin-right:5px;margin-left:5px'/> </td>" +
                    "<td><img src='../../Images/void.png' alt='Select Record' class='voidpendingtaskButton handcursor' data-id='" + data.d[i].T_ID + "_" + data.d[i].C_ID + "_" + data.d[i].TASK_SCH_DATE + "' name='submitButton' id='btnVoidPendingTask' value='Void' style='margin-right:5px;margin-left:5px'/> </td></tr>");
            }
            $('#tablependingtask').append("</tbody>");
            $('#tablependingtask').DataTable({
                "order": [[3, "asc"]]
            });
            document.getElementById("loader").style.display = "none";
        },
        error: function (request, status, error) {
            alert(request.responseText);
            alert("Error while Showing update data");
        }
    });
    
}

function clearpendingtaskscontrols()
{
    $('#cmb_Task_Filter > option:selected').each(function () {
        $("#cmb_Task_Filter").val('default').selectpicker("refresh");
        return false;
    });
    
    $('#cmb_Cli_Cat_Filter > option:selected').each(function () {
        $("#cmb_Cli_Cat_Filter").val('default').selectpicker("refresh");
        return false;
    });

    $('#cmb_Client_Filter > option:selected').each(function () {
        $("#cmb_Client_Filter").val('default').selectpicker("refresh");
        return false;
    });
   

    $('#gridpendingtaskdiv').remove();
    $('#pendingtaskdiv').append("<div class='table-responsive' id='gridpendingtaskdiv'></div>");
    $('#gridpendingtaskdiv').append("<table id='tablependingtask' class='table table-striped table-bordered' style='width: 100%'></table>");
    $('#tablependingtask').append("<thead><tr><th>Task Name</th><th>Priority</th><th>Frequency</th><th>Sch On</th><th>Client</th><th>C No</th><th>File No</th><th>PAN</th><th></th><th></th></tr></thead><tbody></tbody>");
    $('#tablependingtask tbody').remove();
    $('#tablependingtask').append("<tbody>");
    $('#tablependingtask').append("</tbody>");
}

$(function () {
    $(document).on("click", ".addNewButton", function () {
        $('#btnSave').show();
        $('#btnUpdate').hide();

        $('#mainlistingdiv').hide();
        $('#maindetaildiv').show();

        $("#subheaderdiv").html("<h3 style='color:blue'>Tasks -> Create Task</h3>");

        clearpendingtaskscontrols();      

        $('#tasksdetailsmaindiv').hide();
        $('#pendingtasksdivmain').show();
        
    });

    $(document).on("click", ".voidpendingtaskButton", function () {
        if (confirm("Are you sure you want to cancel this pending Task!") == true) {
            var id = $(this).attr("data-id");
            //alert(id);
            var arr = id.split('_');            
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "CreateTask.aspx/InsertTaskAbortData",
                data: '{T_ID: ' + JSON.stringify(arr[0]) + ', C_ID: ' + JSON.stringify(arr[1]) + ', SCH_ON: '+ JSON.stringify(arr[2]) + '}',
                dataType: "json",
                success: function (data) {
                    for (var i = 0; i < data.d.length; i++) {
                        if (data.d[i].RESULT === 1) {
                            getPendingTaskDetails();
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

});