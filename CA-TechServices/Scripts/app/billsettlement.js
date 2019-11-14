var tempclientlstobj = [];
var clientobj = {};
var cname = '';
var cid = 0;
var cno = 0;
var fileno = '';
var sublstobj = [];
var subobj = {};

var bsamt = 0;

$(document).ready(function () {
    $("#masterdiv").removeClass("container");
    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
    $('#dtpFrom').datepicker('setDate', today);
    $('#dtpTo').datepicker('setDate', today);

    $.ajax({
        url: "BillSettlement.aspx/GetLatestTrasnsactionNumber",
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

    $.ajax({
        type: "POST",
        url: "BillSettlement.aspx/GetActivePaymodeList",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadPayModeCombo
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

function LoadPayModeCombo(data) {
    var options = [];
    options.push('<option value="',
         '0', '">',
         '--Select--', '</option>');
    for (var i = 0; i < data.d.length; i++) {
        options.push('<option value="',
          data.d[i].ID, '">',
          data.d[i].NAME, '</option>');
    }
    $("#PAYMODE_NAME").html(options.join(''));
}

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
        url: "BillSettlement.aspx/GetData",
        data: '{fromdate: ' + JSON.stringify($("#dtpFrom").val()) + ', todate: ' + JSON.stringify($("#dtpTo").val()) + '}',
        dataType: "json",
        success: function (data) {
            $('#griddiv').remove();
            $('#maindiv').append("<div class='table-responsive' id='griddiv'></div>");
            $('#griddiv').append("<table id='tablemain' class='table table-striped table-bordered' style='width: 100%'></table>");
            $('#tablemain').append("<thead><tr><th>No</th><th>Date</th><th>C No</th><th>File No</th><th>Client</th><th>Paymode</th><th>Amount</th><th>Void</th><th></th><th></th><th></th></tr></thead><tbody></tbody>");
            $('#tablemain tbody').remove();
            $('#tablemain').append("<tbody>");
            for (var i = 0; i < data.d.length; i++) {
                $('#tablemain').append(
                    "<tr><td style='text-align:center;color:brown'><b>" + data.d[i].BS_NO + "</b></td>" +
                    "<td>" + data.d[i].BS_DATE + "</td>" +
                    "<td style='center;'>" + data.d[i].C_NO + "</td>" +
                    "<td style='center;'>" + data.d[i].FILE_NO + "</td>" +
                    "<td style='color:blue'><b>" + data.d[i].C_NAME + "</b></td>" +
                    "<td>" + data.d[i].PAYMODE_NAME + "</td>" +
                    "<td style='text-align:center;color:red'><b>" + data.d[i].BS_AMT + "</b></td>" +
                    "<td style='text-align:center;'>" + "<input type='checkbox' onclick='return false;' " + (data.d[i].VOID_STATUS == true ? "checked='checked'" : "") + "/></td>" +                    
                    "<td><img src='../../Images/delete.png' alt='Delete Record' class='deleteButton handcursor' data-id='" + data.d[i].BS_ID + "' name='submitButton' id='btnDelete' value='Delete' style='margin-right:5px;margin-left:5px'/> </td>" +
                    "<td><img src='../../Images/void.png' alt='Void / Cancel Record' class='voidButton handcursor' data-id='" + data.d[i].BS_ID + "' name='submitButton' id='btnVoid' value='Void' style='margin-right:5px;margin-left:5px'/> </td>" +
                    "<td><img src='../../Images/print.png' alt='Print Record' class='printButton handcursor' data-id='" + data.d[i].BS_ID + "' id='btnPrint' value='Print' style='margin-right:5px;margin-left:5px'/> </td></tr>");
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

function fetchclients() {
    
    document.getElementById("loader").style.display = "block";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "BillSettlement.aspx/GetPendingBillsCustomers",
        data: '{}',
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
                    clientobj.C_DETAILS = data.d[i].C_DETAILS;
                    clientobj.NET_AMT = data.d[i].NET_AMT;
                    clientobj.BAL_AMT = data.d[i].BAL_AMT;
                    clientobj.BILL_COUNT = data.d[i].BILL_COUNT;
                    tempclientlstobj.push(clientobj);
                }
                loadclientfetchgrid();                
            }
            else {
                alert('No Pending Bills data found.');              
            }

            document.getElementById("loader").style.display = "none";
        },
        error: function () {
            alert("Error while Showing update data");
        }

        //
    });

}

function loadclientfetchgrid() {
    $('#tableclientsearch tbody').remove();
    $('#tableclientsearch').append("<tbody>");
    for (var i = 0; i < tempclientlstobj.length; i++) {
        $('#tableclientsearch').append(            
            "<tr><td style='color:blue'>" + tempclientlstobj[i].C_NAME + "</td>" +
            "<td>" + tempclientlstobj[i].C_DETAILS + "</td>" +
            "<td style='text-align:center;color:brown' ><b>" + tempclientlstobj[i].BILL_COUNT + "</b></td>" +
            "<td style='text-align:center;color:blue' ><b>" + tempclientlstobj[i].NET_AMT + "</b></td>" +
            "<td style='text-align:center;color:red' ><b>" + tempclientlstobj[i].BAL_AMT + "</b></td>" +
            "<td style='text-align:center'><img src='../../Images/select.png' alt='Select Record' class='selectButtonSubis handcursor' data-id='" + tempclientlstobj[i].C_ID + '_' + tempclientlstobj[i].GENID + "' id='btnselectSubIS_" + tempclientlstobj[i].GENID + "' value='Select' style='margin-right:5px;margin-left:5px'/> </td></tr>");
    }
    $('#tableclientsearch').append("</tbody>");

    $("div.mhs h4").html("Pending Bills details Clientwise:");

    $('#PopupModalClientSearch').modal('show');
    $('#PopupModalClientSearch').focus();

    $(".selectButtonSubis").click(function () {
        var id = this.id.split("_");
        for (var i = 0; i < tempclientlstobj.length; i++) {
            if (tempclientlstobj[i].GENID == id[1]) {

                cname = tempclientlstobj[i].C_NAME;
                cid = tempclientlstobj[i].C_ID;
                cno = tempclientlstobj[i].C_NO;
                fileno = tempclientlstobj[i].FILE_NO;
                $('#lblclientdetails').text(tempclientlstobj[i].C_DETAILS);
                $('#C_NAME').val(tempclientlstobj[i].C_NAME);

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "BillSettlement.aspx/GetPendingBillsbyClientID",
                    data: '{id: ' + JSON.stringify(cid) + '}',
                    dataType: "json",
                    success: function (data) {
                        if (data.d.length > 0) {
                            sublstobj = [];
                            for (var i = 0; i < data.d.length; i++) {
                                subobj = {};
                                subobj.SL_NO = i + 1;
                                subobj.GENID = Math.floor((Math.random() * 10000000) + 1);
                                subobj.BILL_ID = data.d[i].BILL_ID;
                                subobj.BILL_NO = data.d[i].BILL_NO;
                                subobj.BILL_DATE = data.d[i].BILL_DATE;
                                subobj.BILL_AMT = data.d[i].BILL_AMT;
                                subobj.PAID_AMT = data.d[i].PAID_AMT;
                                subobj.BAL_AMT = data.d[i].BAL_AMT;
                                subobj.BS_AMT = data.d[i].BS_AMT;
                                sublstobj.push(subobj);
                            }
                            loadsubcontrols();
                            calcamt();
                            $('#PopupModalClientSearch').modal('hide');
                            $("#PAYMODE_NAME").focus();
                        }
                        else {
                            alert('No Pending Bills data found for selected Client.');
                        }

                        document.getElementById("loader").style.display = "none";
                    },
                    error: function () {
                        alert("Error while Showing update data");
                    }

                    //
                });

                break;
            }
        }

        
    });

}

function clearcontrols(addflag) {
    cname = '';
    cid = 0;
    cno = 0;
    fileno = '';
    $('#C_NAME').val('');
    $('#lblclientdetails').text('');
    

    sublstobj = [];
    subobj = {};
    subobj.SL_NO = 1;
    subobj.GENID = Math.floor((Math.random() * 10000000) + 1);
    subobj.BILL_ID = 0;
    subobj.BILL_NO = 0;
    subobj.BILL_DATE = "";
    subobj.BILL_AMT = 0;
    subobj.PAID_AMT = 0;
    subobj.BAL_AMT = 0;
    subobj.BS_AMT = 0;
    sublstobj.push(subobj);

    loadsubcontrols();

    bsamt = 0;
    $("#BS_AMT").val(0);
    
    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());   

    $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
    $('#BS_DATE').datepicker('setDate', today);
    
    $("#PAYMODE_NAME").val(0);
    $('#BS_NO').val(0);

    if (addflag == 1) {
        $.ajax({
            url: "BillSettlement.aspx/GetLatestTrasnsactionNumber",
            data: '{}',
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                for (var i = 0; i < data.d.length; i++) {
                    $('#BS_NO').val(data.d[i].split('-')[0]);
                    $('#BS_DATE').datepicker({ dateFormat: 'dd-mm-yy' }).datepicker('setDate', data.d[i].split('-')[3] + '-' + data.d[i].split('-')[2] + '-' + data.d[i].split('-')[1]);
                    
                }
            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }
        });
    }

}

function loadsubcontrols() {
    $("#divservicedetaisdetails").empty();
    for (var i = 0; i < sublstobj.length; i++) {
        var newTextBoxDiv = $(document.createElement('div'))
            .attr("id", 'SubrowsDiv' + sublstobj[i].GENID);
        newTextBoxDiv.attr('class', 'row');

        newTextBoxDiv.after().html("<div class='form-group col-1'>" +
        "<input type='number' id='txtbillno" + sublstobj[i].GENID + "' data-id='" + sublstobj[i].GENID + "' class='form-control' value='" + sublstobj[i].BILL_NO + "' style='text-align: center;font-weight:bold;color:brown;background-color: white;' disabled='disabled'/>" +
        "</div>" +
        "<div class='form-group col-2'>" +
        "<input type='text' id='txtbilldate" + sublstobj[i].GENID + "' data-id='" + sublstobj[i].GENID + "' class='form-control' value='" + sublstobj[i].BILL_DATE + "' style='text-align: center;font-weight:bold;color:black;background-color: white;' disabled='disabled'/>" +
        "</div>" +
        "<div class='form-group col-2'>" +
        "<input type='text' id='txtbillamt" + sublstobj[i].GENID + "' data-id='" + sublstobj[i].GENID + "' class='form-control' value='" + sublstobj[i].BILL_AMT + "' style='text-align: center;font-weight:bold;color:blue;background-color: white;' disabled='disabled'/>" +
        "</div>" +
        "<div class='form-group col-2'>" +
        "<input type='text' id='txtpaidamt" + sublstobj[i].GENID + "' data-id='" + sublstobj[i].GENID + "' class='form-control' value='" + sublstobj[i].PAID_AMT + "' style='text-align: center;font-weight:bold;color:green;background-color: white;' disabled='disabled'/>" +
        "</div>" +
        "<div class='form-group col-2'>" +
        "<input type='text' id='txtbalamt" + sublstobj[i].GENID + "' data-id='" + sublstobj[i].GENID + "' class='form-control' value='" + sublstobj[i].BAL_AMT + "' style='text-align: center;font-weight:bold;color:red;background-color: white;' disabled='disabled'/>" +
        "</div>" +
        "<div class='form-group col-2'>" +
        "<input type='text' id='txtbsamt" + sublstobj[i].GENID + "' data-id='" + sublstobj[i].GENID + "' class='form-control txtbsamt' value='" + sublstobj[i].BS_AMT + "' style='text-align: center;font-weight:bold;color:deeppink;background-color: white;' />" +
        "</div>" +
        "<div class='form-group col-1'>" +
        "<img id='btnbillrowdel" + sublstobj[i].GENID + "' data-id='" + sublstobj[i].GENID + "' class='btnbillrowdel handcursor' src='../../Images/delete.png' style='margin-left: 8px;'/>" +
        "</div>");       
       

        newTextBoxDiv.appendTo("#divservicedetaisdetails");

    }

    $('.txtbsamt').on('input', function () {
        var id = $(this).attr("data-id");
        var amt = parseFloat($(this).val()) || 0;
        if (amt < 0 || $(this).val() == NaN)
        {
            $(this).val(0);
            return false;
        }
        for (var i = 0; i < sublstobj.length; i++) {
            if (sublstobj[i].GENID == id) {
                if (amt > (parseFloat($("#txtbalamt" + sublstobj[i].GENID).val()) || 0))
                {
                    alert('Amount cannot be greater than balance amount.')
                    $("#txtbsamt" + sublstobj[i].GENID).val($("#txtbalamt" + sublstobj[i].GENID).val());
                    amt = parseFloat($("#txtbalamt" + sublstobj[i].GENID).val()) || 0;
                }
                sublstobj[i].BS_AMT = amt;
                calcamt();
                return false;
            }
        }
    });   

    $(".btnbillrowdel").click(function () {
        if (confirm("Are you sure you want to delete !") == true) {
            var id = $(this).attr("data-id");
            var newsubItemsList = [];
            for (var i = 0; i < sublstobj.length; i++) {
                if (sublstobj[i].GENID != id) {
                    newsubItemsList.push(sublstobj[i]);
                }
            }
            sublstobj = [];
            for (var i = 0; i < newsubItemsList.length; i++) {
                sublstobj.push(newsubItemsList[i]);
                sublstobj[i].SL_NO = (i + 1);
            }
            loadsubcontrols();
            calcamt();
        }
    });

    $('input').on("keypress", function (e) {
        /* ENTER PRESSED*/
        if (e.keyCode == 13) {
            e.preventDefault();
            var inputs = $(this).closest('form').find(':input:visible:enabled');
            inputs.eq(inputs.index(this) + 1).focus();
            return false;
        }
    });

    if (sublstobj.length > 0) {
        $('#txtdescp' + sublstobj[sublstobj.length - 1].GENID).focus();
    }
}

function calcamt() {
    bsamt = 0;
    for (var i = 0; i < sublstobj.length; i++) {
        bsamt = bsamt + parseFloat(sublstobj[i].BS_AMT) || 0;
    }
    bsamt = round(bsamt, 2);
    $("#BS_AMT").val(bsamt);    
}

function round(value, precision) {
    var aPrecision = Math.pow(10, precision);
    return Math.round(value * aPrecision) / aPrecision;
}

$(function () {

    $(document).on("click", ".deleteButton", function () {
        if (confirm("Are you sure you want to delete !") == true) {
            var id = $(this).attr("data-id");
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "BillSettlement.aspx/DeleteData",
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

        $("#subheaderdiv").html("<h3 style='color:blue'>Bill Settlement -> Create a new Bill Settlement</h3>");

        clearcontrols(1);

        $('#SEARCHTEXT').focus();
    });

    $(document).on("click", ".cancelButton", function () {
        $('#mainlistingdiv').show();
        $('#mainldetaildiv').hide();
    });

    $(document).on("click", "#btnfetch", function () {
        fetchclients();
    });

    $("#btnSave").click(function () {

        if ($("#C_NAME").val().trim() == "") {
            alert("Please Select Client name.");
            $("#C_NAME").focus();
            return false;
        }

        if ($("#PAYMODE_NAME").val() == null || $("#PAYMODE_NAME").val() == undefined || $.trim($("#PAYMODE_NAME").val()) == '' || $.trim($("#PAYMODE_NAME").val()) == '0' || $.trim($("#PAYMODE_NAME").val()) == 0) {
            alert('Please select Paymode.');
            $("#PAYMODE_NAME").focus();
            return false;
        }

        if (netamt <= 0) {
            alert("Bill amount cannot be Zero, Please add atleast one valid service description.");
            $("#btnaddrow").focus();
            return false;
        }

        if (sublstobj == undefined || sublstobj == null || sublstobj.length <= 0) {
            alert("Please add atleast one service description.");
            $("#btnaddrow").focus();
            return false;
        }

        for (var i = 0; i < sublstobj.length; i++) {
            if ($("#txtdescp" + sublstobj[i].GENID).val().trim() == "") {
                alert("Please enter service description.");
                $("#txtdescp" + sublstobj[i].GENID).focus();
                return false;
            }
        }

        var obj = {};
        obj.C_ID = cid;
        obj.C_NO = cno;
        obj.FILE_NO = fileno;
        obj.C_NAME = cname;
        obj.BILL_DATE = $("#BILL_DATE").val();
        obj.DUE_DATE = $("#DUE_DATE").val();
        obj.REMARKS = $("#REMARKS").val();
        obj.TASK_ID = 0;
        obj.TASK_NO = 0;
        obj.PAYMODE_ID = $("#PAYMODE_NAME").val();
        obj.PAYMODE_NAME = $("#PAYMODE_NAME option:selected").text();
        obj.GROSS_AMT = grossamt;
        obj.SGST_AMT = sgstamt;
        obj.CGST_AMT = cgstamt;
        obj.IGST_AMT = igstamt;
        obj.OTH_AMT = othamt;
        obj.NET_AMT = netamt;
        obj.SUBARRAY = sublstobj;

        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "BillSettlement.aspx/InsertData",
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
      

    $(document).on("click", ".voidButton", function () {
        var id = $(this).attr("data-id");
        var checkid = 0;
        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "BillSettlement.aspx/CheckVoidBSEnrty",
            data: '{id: ' + id + '}',
            dataType: "json",
            success: function (data) {
                if (data.d.length > 0) {
                    checkid = data.d[0];
                }

                if (checkid != null && checkid != undefined && checkid > 0) {
                    alert('Cannot Void already Voided entry.');
                    document.getElementById("loader").style.display = "none";
                    return false;
                }

                if (confirm("Are you sure you want to Void/Cancel the entry!") == true) {
                    $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        url: "BillSettlement.aspx/VoidData",
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
            },
            error: function () {
                alert("Error while checking is void data of :" + id);
            }
        });
    });

    $(document).on("click", ".printButton", function () {

        var id = $(this).attr("data-id");
        console.log(id);

        $('#tablesubprn tbody').remove();
        $('#tablesubprn').append("<tbody>");
        $('#tablesubprn').append("</tbody>");
        $('#lblbillno').text('');
        $('#lblbilldate').text('');
        $('#lblfileno').text('');
        $('#lblpaymode').text('');
        $('#lblcname').text('');
        $('#lblcadd').text('');
        $('#lblcpan').text('');
        $('#lblcgstin').text('');
        $('#lblreamrks').text('');

        $('#lblgrossamt').text('');
        $('#lblcgstamt').text('');
        $('#lblsgstamt').text('');
        $('#lbligstamt').text('');
        $('#lblothamt').text('');
        $('#lblnetamt').text('');

        $('#trcgstamt').show();
        $('#trsgstamt').show();
        $('#trigstamt').show();
        $('#trothamt').show();
        $('#trvoid').hide();
        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "BillSettlement.aspx/EditData",
            data: '{id: ' + id + '}',
            dataType: "json",
            success: function (data) {
                if (data.d.length > 0) {
                    $("#lblbillno").text(data.d[0].MAINARRAY[0].BILL_NO);
                    $('#lblbilldate').text(data.d[0].MAINARRAY[0].BILL_DATE.split('-')[2] + '-' + data.d[0].MAINARRAY[0].BILL_DATE.split('-')[1] + '-' + data.d[0].MAINARRAY[0].BILL_DATE.split('-')[0]);
                    $("#lblfileno").text(data.d[0].MAINARRAY[0].FILE_NO);
                    $("#lblpaymode").text(data.d[0].MAINARRAY[0].PAYMODE_NAME);
                    $('#lblcname').text(data.d[0].MAINARRAY[0].C_NAME);
                    $('#lblcadd').text(data.d[0].MAINARRAY[0].C_DETAILS);
                    $('#lblcpan').text(data.d[0].MAINARRAY[0].PAN);
                    $('#lblcgstin').text(data.d[0].MAINARRAY[0].GSTIN);
                    $('#lblreamrks').text(data.d[0].MAINARRAY[0].REMARKS);

                    $('#lblgrossamt').text(data.d[0].MAINARRAY[0].GROSS_AMT);
                    $('#lblcgstamt').text(data.d[0].MAINARRAY[0].CGST_AMT);
                    $('#lblsgstamt').text(data.d[0].MAINARRAY[0].SGST_AMT);
                    $('#lbligstamt').text(data.d[0].MAINARRAY[0].IGST_AMT);
                    $('#lblothamt').text(data.d[0].MAINARRAY[0].OTH_AMT);
                    $('#lblnetamt').text(data.d[0].MAINARRAY[0].NET_AMT);

                    if (data.d[0].MAINARRAY[0].CGST_AMT == 0)
                        $('#trcgstamt').hide();
                    if (data.d[0].MAINARRAY[0].SGST_AMT == 0)
                        $('#trsgstamt').hide();
                    if (data.d[0].MAINARRAY[0].IGST_AMT == 0)
                        $('#trigstamt').hide();
                    if (data.d[0].MAINARRAY[0].OTH_AMT == 0)
                        $('#trothamt').hide();

                    if (data.d[0].MAINARRAY[0].VOID_STATUS == true) {
                        $('#trvoid').show();
                    }
                }

                $('#tablesubprn tbody').remove();
                $('#tablesubprn').append("<tbody>");
                for (var i = 0; i < data.d[0].SUBARRAY.length; i++) {
                    $('#tablesubprn').append(
                        "<tr><td style='border: 1px solid black;text-align:center;color:brown'><b>" + data.d[0].SUBARRAY[i].SL_NO + "</b></td><td style='border: 1px solid black;'>" + data.d[0].SUBARRAY[i].DESCP + "</td><td style='border: 1px solid black;color:blue'>" + data.d[0].SUBARRAY[i].REMARKS + "</td><td style='border: 1px solid black;text-align:center;color:red'><b>" + data.d[0].SUBARRAY[i].NET_AMT + "</b></td></tr>");
                }
                $('#tablesubprn').append("</tbody>");
                $('#printdiv').show();
                var divToPrint = document.getElementById("printdiv");
                newWin = window.open("");
                newWin.document.write(divToPrint.outerHTML);
                $('#printdiv').hide();
                newWin.print();
                //newWin.close();

            },
            error: function () {
                alert("Error while retrieving data of :" + id);
            }
        });


    });

});
