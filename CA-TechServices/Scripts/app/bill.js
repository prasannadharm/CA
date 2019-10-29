var subItemsList = [];
$(document).ready(function () {
    $("#masterdiv").removeClass("container");
    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    subItemsList = [];
    $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
    $('#dtpFrom').datepicker('setDate', today);
    $('#dtpTo').datepicker('setDate', today);

    $.ajax({
        url: "Bill.aspx/GetLatestTrasnsactionNumber",
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
        url: "Bill.aspx/GetData",
        data: '{fromdate: ' + JSON.stringify($("#dtpFrom").val()) + ', todate: ' + JSON.stringify($("#dtpTo").val()) + '}',
        dataType: "json",
        success: function (data) {
            $('#griddiv').remove();
            $('#maindiv').append("<div class='table-responsive' id='griddiv'></div>");
            $('#griddiv').append("<table id='tablemain' class='table table-striped table-bordered' style='width: 100%'></table>");
            $('#tablemain').append("<thead><tr><th>Bill No</th><th>Date</th><th>C No</th><th>File No</th><th>Client</th><th>Paymode</th><th>Amount</th><th>Void</th><th></th><th></th><th></th><th></th></tr></thead><tbody></tbody>");
            $('#tablemain tbody').remove();
            $('#tablemain').append("<tbody>");
            for (var i = 0; i < data.d.length; i++) {
                $('#tablemain').append(
                    "<tr><td style='text-align:center;color:brown'><b>" + data.d[i].BILL_NO + "</b></td>" +
                    "<td>" + data.d[i].BILL_DATE + "</td>" +
                    "<td style='center;'>" + data.d[i].C_NO + "</td>" +
                    "<td style='center;'>" + data.d[i].FILE_NO + "</td>" +
                    "<td style='color:blue'><b>" + data.d[i].C_NAME + "</b></td>" +
                    "<td>" + data.d[i].PAYMODE_NAME + "</td>" +
                    "<td><b>" + data.d[i].NET_AMT + "</b></td>" +
                    "<td style='text-align:center;'>" + "<input type='checkbox' onclick='return false;' " + (data.d[i].VOID_STATUS == true ? "checked='checked'" : "") + "/></td>" +
                    "<td><img src='../../Images/edit.png' alt='Edit Record' class='editButton handcursor' data-id='" + data.d[i].BILL_ID + "' name='submitButton' id='btnEdit' value='Edit' style='margin-right:5px'/>" + "</td>" +
                    "<td><img src='../../Images/delete.png' alt='Delete Record' class='deleteButton handcursor' data-id='" + data.d[i].BILL_ID + "' name='submitButton' id='btnDelete' value='Delete' style='margin-right:5px;margin-left:5px'/> </td>" +
                    "<td><img src='../../Images/void.png' alt='Void / Cancel Record' class='voidButton handcursor' data-id='" + data.d[i].BILL_ID + "' name='submitButton' id='btnVoid' value='Void' style='margin-right:5px;margin-left:5px'/> </td>" +
                    "<td><img src='../../Images/print.png' alt='Print Record' class='printButton handcursor' data-id='" + data.d[i].BILL_ID + "' id='btnPrint' value='Print' style='margin-right:5px;margin-left:5px'/> </td></tr>");
            }
            $('#tablemain').append("</tbody>");
            $('#tablemain').DataTable({
                "order": [[0, "desc"]]
            });
        },
        error: function (request, status, error) {
            alert(request.responseText);
            alert("Error while Showing update data");
        }
    });
    document.getElementById("loader").style.display = "none";
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