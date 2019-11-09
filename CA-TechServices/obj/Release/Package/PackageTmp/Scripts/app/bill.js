var tempclientlstobj = [];
var clientobj = {};
var cname = '';
var cid = 0;
var cno = 0;
var fileno = '';
var sublstobj = [];
var subobj = {};

var grossamt = 0;
var sgstamt = 0;
var cgstamt = 0;
var igstamt = 0;
var othamt = 0;
var netamt = 0;

$(document).ready(function () {
    $("#masterdiv").removeClass("container");
    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
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

    $.ajax({
        type: "POST",
        url: "Bill.aspx/GetActivePaymodeList",
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

    $('#SEARCHTEXT').keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            searchclients();
        }
    });

    //Vaule change in other text box
    $('#OTH_AMT').on('input', function () {
        othamt = parseFloat($(this).val()) || 0;
        netamt = grossamt + cgstamt + sgstamt + igstamt + othamt;
        $("#NET_AMT").val(netamt);
    });
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
                    "<td style='text-align:center;color:red'><b>" + data.d[i].NET_AMT + "</b></td>" +
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

$(function () {

    $(document).on("click", ".deleteButton", function () {
        if (confirm("Are you sure you want to delete !") == true) {
            var id = $(this).attr("data-id");
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "Bill.aspx/DeleteData",
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

        $("#subheaderdiv").html("<h3 style='color:blue'>Billing -> Create a new Bill</h3>");

        clearcontrols();

        $('#SEARCHTEXT').focus();
    });
       
    $(document).on("click", ".cancelButton", function () {
        $('#mainlistingdiv').show();
        $('#mainldetaildiv').hide();
    });
       
    $(document).on("click", "#btnsearchClient", function () {
        searchclients();
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
            url: "Bill.aspx/InsertData",
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

    $("#btnUpdate").click(function () {
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

        var id = $(this).attr("edit-id");

        var obj = {};
        obj.BILL_ID = id;
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
            url: "Bill.aspx/UpdateData",
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
        $("#subheaderdiv").html("<h3 style='color:blue'>Billing -> Create a new Bill</h3>");
        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "Bill.aspx/EditData",
            data: '{id: ' + id + '}',
            dataType: "json",
            success: function (data) {
                if (data.d.length > 0) {
                    if (data.d[0].MAINARRAY != null && data.d[0].MAINARRAY != undefined && data.d[0].MAINARRAY.length > 0) {

                        $("#subheaderdiv").html("<h3 style='color:blue'>Billing -> Edit Bill : " + data.d[0].MAINARRAY[0].BILL_NO + "</h3>");

                        cname = data.d[0].MAINARRAY[0].C_NAME;
                        cno = data.d[0].MAINARRAY[0].C_NO;
                        cid = data.d[0].MAINARRAY[0].C_ID;
                        fileno = data.d[0].MAINARRAY[0].FILE_NO;                      
                        
                        $("#C_NAME").val(data.d[0].MAINARRAY[0].C_NAME);
                        $("#BILL_NO").val(data.d[0].MAINARRAY[0].BILL_NO);
                        $("#PAYMODE_NAME").val(data.d[0].MAINARRAY[0].PAYMODE_ID);
                        $("#REMARKS").val(data.d[0].MAINARRAY[0].REMARKS);
                        $('#BILL_DATE').datepicker({ dateFormat: 'dd-mm-yy' }).datepicker('setDate', data.d[0].MAINARRAY[0].BILL_DATE.split('-')[2] + '-' + data.d[0].MAINARRAY[0].BILL_DATE.split('-')[1] + '-' + data.d[0].MAINARRAY[0].BILL_DATE.split('-')[0]);
                        $('#DUE_DATE').datepicker({ dateFormat: 'dd-mm-yy' }).datepicker('setDate', data.d[0].MAINARRAY[0].DUE_DATE.split('-')[2] + '-' + data.d[0].MAINARRAY[0].DUE_DATE.split('-')[1] + '-' + data.d[0].MAINARRAY[0].DUE_DATE.split('-')[0]);
                                                
                        grossamt = data.d[0].MAINARRAY[0].GROSS_AMT;
                        cgstamt = data.d[0].MAINARRAY[0].CGST_AMT;
                        sgstamt = data.d[0].MAINARRAY[0].SGST_AMT;
                        igstamt = data.d[0].MAINARRAY[0].IGST_AMT;
                        othamt = data.d[0].MAINARRAY[0].OTH_AMT;
                        netamt = data.d[0].MAINARRAY[0].NET_AMT;

                        $("#GROSS_AMT").val(data.d[0].MAINARRAY[0].GROSS_AMT);
                        $("#CGST_AMT").val(data.d[0].MAINARRAY[0].CGST_AMT);
                        $("#SGST_AMT").val(data.d[0].MAINARRAY[0].SGST_AMT);
                        $("#IGST_AMT").val(data.d[0].MAINARRAY[0].IGST_AMT);
                        $("#OTH_AMT").val(data.d[0].MAINARRAY[0].OTH_AMT);
                        $("#NET_AMT").val(data.d[0].MAINARRAY[0].NET_AMT);
                    }

                    if (data.d[0].SUBARRAY != null && data.d[0].SUBARRAY != undefined && data.d[0].SUBARRAY.length > 0) {
                        sublstobj = [];
                        for (var i = 0; i < data.d[0].SUBARRAY.length; i++) {
                            subobj = {};
                            subobj.SL_NO = data.d[0].SUBARRAY[i].SL_NO;
                            subobj.GENID = Math.floor((Math.random() * 10000000) + 1);
                            subobj.DESCP = data.d[0].SUBARRAY[i].DESCP;
                            subobj.GROSS_AMT = data.d[0].SUBARRAY[i].GROSS_AMT;
                            subobj.SGST_PER = data.d[0].SUBARRAY[i].SGST_PER;
                            subobj.SGST_AMT = data.d[0].SUBARRAY[i].SGST_AMT;
                            subobj.CGST_PER = data.d[0].SUBARRAY[i].CGST_PER;
                            subobj.CGST_AMT = data.d[0].SUBARRAY[i].CGST_AMT;
                            subobj.IGST_PER = data.d[0].SUBARRAY[i].IGST_PER;
                            subobj.IGST_AMT = data.d[0].SUBARRAY[i].IGST_AMT;
                            subobj.NET_AMT = data.d[0].SUBARRAY[i].NET_AMT;
                            subobj.REMARKS = data.d[0].SUBARRAY[i].REMARKS;
                            sublstobj.push(subobj);
                        }
                        loadsubcontrols();
                        calcamt();
                    }                   
                }

                $('#PAYMODE_NAME').focus();
                document.getElementById("loader").style.display = "none";
            },
            error: function () {
                alert("Error while retrieving data of :" + id);
            }
        });
    });

    $(document).on("click", "#btnaddrow", function () {
        subobj = {};
        subobj.SL_NO = sublstobj.length + 1;
        subobj.GENID = Math.floor((Math.random() * 10000000) + 1);        
        subobj.DESCP = "";
        subobj.GROSS_AMT = 0;
        subobj.SGST_PER = 0;
        subobj.SGST_AMT = 0;
        subobj.CGST_PER = 0;
        subobj.CGST_AMT = 0;
        subobj.IGST_PER = 0;
        subobj.IGST_AMT = 0;
        subobj.NET_AMT = 0;
        subobj.REMARKS = "";
        sublstobj.push(subobj);
        loadsubcontrols();
    });

});


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
        url: "Bill.aspx/GetClientSearchList",
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

                cname = tempclientlstobj[i].C_NAME;
                cid = tempclientlstobj[i].C_ID;
                cno = tempclientlstobj[i].C_NO;
                fileno = tempclientlstobj[i].FILE_NO;
                $('#C_NAME').val(tempclientlstobj[i].C_NAME);              
                break;
            }
        }        
        $('#PopupModalClientSearch').modal('hide');
        $("#PAYMODE_NAME").focus();
    });

}

function clearcontrols()
{
    cname = '';
    cid = 0;
    cno = 0;
    fileno = '';
    $('#C_NAME').val('');
    $("#SEARCHTEXT").val('');

    sublstobj = [];
    subobj = {};
    subobj.SL_NO = 1;
    subobj.GENID = Math.floor((Math.random() * 10000000) + 1);
    subobj.DESCP = "";
    subobj.GROSS_AMT = 0;
    subobj.SGST_PER = 0;
    subobj.SGST_AMT = 0;
    subobj.CGST_PER = 0;
    subobj.CGST_AMT = 0;
    subobj.IGST_PER = 0;
    subobj.IGST_AMT = 0;
    subobj.NET_AMT = 0;
    subobj.REMARKS = "";
    sublstobj.push(subobj);
    loadsubcontrols();

    grossamt = 0;
    sgstamt = 0;
    cgstamt = 0;
    igstamt = 0;
    othamt = 0;
    netamt = 0;
    $("#GROSS_AMT").val(0);
    $("#CGST_AMT").val(0);
    $("#SGST_AMT").val(0);
    $("#IGST_AMT").val(0);
    $("#OTH_AMT").val(0);
    $("#NET_AMT").val(0);

    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var plus7days = today.getDate() + 7;
    
    $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
    $('#BILL_DATE').datepicker('setDate', today);
    $('#DUE_DATE').datepicker('setDate', plus7days);
    $("#PAYMODE_NAME").val(0);
    $('#BILL_NO').val(0);

    $.ajax({
        url: "Bill.aspx/GetLatestTrasnsactionNumber",
        data: '{}',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            for (var i = 0; i < data.d.length; i++) {
                $('#BILL_NO').val(data.d[i].split('-')[0]);
                $('#BILL_DATE').datepicker({ dateFormat: 'dd-mm-yy' }).datepicker('setDate', data.d[i].split('-')[3] + '-' + data.d[i].split('-')[2] + '-' + data.d[i].split('-')[1]);
                var date2 = $('#BILL_DATE').datepicker('getDate');
                var nextDayDate = new Date();
                nextDayDate.setDate(date2.getDate() + 7);
                $('#DUE_DATE').datepicker('setDate', nextDayDate);
              
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

function loadsubcontrols()
{
    $("#divservicedetaisdetails").empty();
    for (var i = 0; i < sublstobj.length; i++) {
        var newTextBoxDiv = $(document.createElement('div'))
            .attr("id", 'SubrowsDiv' + sublstobj[i].GENID);
        newTextBoxDiv.attr('class', 'row');
        newTextBoxDiv.after().html("<div class='form-group col-3'>" +
        "<label id='lblslno" + sublstobj[i].GENID + "' data-id='" + sublstobj[i].GENID + "' style='text-align: center; color: brown; font-weight: 500; display: inline;margin-right:5px;'>" + sublstobj[i].SL_NO + "</label>" +
        "<input type='text' id='txtdescp" + sublstobj[i].GENID + "' data-id='" + sublstobj[i].GENID + "' class='form-control txtdescp' placeholder='Enter Description.' style='width: 88%; display: inline' value='" + sublstobj[i].DESCP + "' />" +
        "</div>" +
        "<div class='form-group col-6'>" +
        "<table style='width: 100%'>" +
        "<tr>" +
        "<td style='width: 20%;padding-right:20px'>" +
        "<input type='number' id='txtgrossamt" + sublstobj[i].GENID + "' class='form-control txtgrossamt' value='" + sublstobj[i].GROSS_AMT + "' style='text-align: center;width:100%;;background-color: white; color: brown; font-weight: 500' data-id='" + sublstobj[i].GENID + "' />" +
        "</td>" +
        "<td style='width: 20%'>" +
        "<input type='number' id='txtsgstper" + sublstobj[i].GENID + "' class='form-control txtsgstper' value='" + sublstobj[i].SGST_PER + "' style='text-align: center; width: 50%; display: inline;background-color: white; color: green; font-weight: 500' data-id='" + sublstobj[i].GENID + "' />" +
        "<label id='lblsgstamt" + sublstobj[i].GENID + "' data-id='" + sublstobj[i].GENID + "' style='text-align: center; color: blue; font-weight: 500; display: inline; width: 50%;margin-left:5px;'>" + sublstobj[i].SGST_AMT + "</label>" +
        "</td>" +
        "<td style='width: 20%'>" +
        "<input type='number' id='txtcgstper" + sublstobj[i].GENID + "' class='form-control txtcgstper' value='" + sublstobj[i].CGST_PER + "' style='text-align: center; width: 50%; display: inline;background-color: white; color: green; font-weight: 500' data-id='" + sublstobj[i].GENID + "' />" +
        "<label id='lblcgstamt" + sublstobj[i].GENID + "' data-id='" + sublstobj[i].GENID + "' style='text-align: center; color: blue; font-weight: 500; display: inline; width: 50%;margin-left:5px;'>" + sublstobj[i].CGST_AMT + "</label>" +
        "</td>" +
        "<td style='width: 20%'>" +
        "<input type='number' id='txtigstper" + sublstobj[i].GENID + "' class='form-control txtigstper' value='" + sublstobj[i].IGST_PER + "' style='text-align: center; width: 50%; display: inline;background-color: white; color: green; font-weight: 500' data-id='" + sublstobj[i].GENID + "' />" +
        "<label id='lbligstamt" + sublstobj[i].GENID + "' data-id='" + sublstobj[i].GENID + "' style='text-align: center; color: blue; font-weight: 500; display: inline; width: 50%;margin-left:5px;'>" + sublstobj[i].IGST_AMT + "</label>" +
        "</td>" +
        "<td style='width: 20%'>" +
        "<input type='number' id='txtnettamt" + sublstobj[i].GENID + "' class='form-control' value='" + sublstobj[i].NET_AMT + "' style='text-align: center;width:100%;background-color: white; color: orangered; font-weight: 500' disabled='disabled' data-id='" + sublstobj[i].GENID + "' />" +
        "</td>" +
        "</tr>" +
        "</table>" +
        "</div>" +
        "<div class='form-group col-3'>" +
        "<input type='text' id='txtremarks" + sublstobj[i].GENID + "' data-id='" + sublstobj[i].GENID + "' class='form-control txtremarks' placeholder='Enter remarks.' style='display: inline; width: 80%' value='" + sublstobj[i].REMARKS + "' />" +
        "<img id='btnbillrowdel" + sublstobj[i].GENID + "' class='btnbillrowdel handcursor' src='../../Images/delete.png' style='margin-left: 8px; display: inline;' data-id='" + sublstobj[i].GENID + "' />" +
        "</div>");

        newTextBoxDiv.appendTo("#divservicedetaisdetails"); 

    }

    $('.txtgrossamt').on('input', function () {
        var id = $(this).attr("data-id");
        for (var i = 0; i < sublstobj.length; i++) {
            if (sublstobj[i].GENID == id) {
                sublstobj[i].GROSS_AMT = parseFloat($(this).val()) || 0;
                sublstobj[i].SGST_AMT = round((sublstobj[i].SGST_PER / 100.0) * sublstobj[i].GROSS_AMT, 2);
                sublstobj[i].CGST_AMT = round((sublstobj[i].CGST_PER / 100.0) * sublstobj[i].GROSS_AMT, 2);
                sublstobj[i].IGST_AMT = round((sublstobj[i].IGST_PER / 100.0) * sublstobj[i].GROSS_AMT, 2);
                sublstobj[i].NET_AMT = round(sublstobj[i].GROSS_AMT + sublstobj[i].SGST_AMT + sublstobj[i].CGST_AMT + sublstobj[i].IGST_AMT, 2);

                $('#lblsgstamt' + sublstobj[i].GENID).text(sublstobj[i].SGST_AMT);
                $('#lblcgstamt' + sublstobj[i].GENID).text(sublstobj[i].CGST_AMT);
                $('#lbligstamt' + sublstobj[i].GENID).text(sublstobj[i].IGST_AMT);
                $('#txtnettamt' + sublstobj[i].GENID).val(sublstobj[i].NET_AMT);
                calcamt();
                return false;
            }
        }
    });

    $('.txtsgstper').on('input', function () {
        var id = $(this).attr("data-id");
        for (var i = 0; i < sublstobj.length; i++) {
            if (sublstobj[i].GENID == id) {
                sublstobj[i].SGST_PER = parseFloat($(this).val()) || 0;
                sublstobj[i].IGST_PER = 0;
                sublstobj[i].SGST_AMT = round((sublstobj[i].SGST_PER / 100.0) * sublstobj[i].GROSS_AMT, 2);
                sublstobj[i].CGST_AMT = round((sublstobj[i].CGST_PER / 100.0) * sublstobj[i].GROSS_AMT, 2);
                sublstobj[i].IGST_AMT = 0;
                sublstobj[i].NET_AMT = round(sublstobj[i].GROSS_AMT + sublstobj[i].SGST_AMT + sublstobj[i].CGST_AMT + sublstobj[i].IGST_AMT, 2);

                $('#lblsgstamt' + sublstobj[i].GENID).text(sublstobj[i].SGST_AMT);
                $('#lblcgstamt' + sublstobj[i].GENID).text(sublstobj[i].CGST_AMT);
                $('#lbligstamt' + sublstobj[i].GENID).text(sublstobj[i].IGST_AMT);
                $('#txtnettamt' + sublstobj[i].GENID).val(sublstobj[i].NET_AMT);
                calcamt();
                return false;
            }
        }
    });

    $('.txtcgstper').on('input', function () {
        var id = $(this).attr("data-id");
        for (var i = 0; i < sublstobj.length; i++) {
            if (sublstobj[i].GENID == id) {
                sublstobj[i].CGST_PER = parseFloat($(this).val()) || 0;                
                sublstobj[i].CGST_AMT = round((sublstobj[i].CGST_PER / 100.0) * sublstobj[i].GROSS_AMT, 2);
                sublstobj[i].IGST_PER = 0;
                sublstobj[i].IGST_AMT = 0;
                sublstobj[i].NET_AMT = round(sublstobj[i].GROSS_AMT + sublstobj[i].SGST_AMT + sublstobj[i].CGST_AMT + sublstobj[i].IGST_AMT, 2);

                $('#lblcgstamt' + sublstobj[i].GENID).text(sublstobj[i].CGST_AMT);
                
                $('#txtigstper' + sublstobj[i].GENID).val(sublstobj[i].IGST_PER);
                $('#lbligstamt' + sublstobj[i].GENID).text(sublstobj[i].IGST_AMT);             
                
                $('#txtnettamt' + sublstobj[i].GENID).val(sublstobj[i].NET_AMT);
                calcamt();
                return false;
            }
        }
    });

    $('.txtigstper').on('input', function () {
        var id = $(this).attr("data-id");
        for (var i = 0; i < sublstobj.length; i++) {
            if (sublstobj[i].GENID == id) {
                sublstobj[i].IGST_PER = parseFloat($(this).val()) || 0;

                sublstobj[i].CGST_PER = 0;
                sublstobj[i].SGST_PER = 0;
                sublstobj[i].SGST_AMT = 0;
                sublstobj[i].CGST_AMT = 0;
                sublstobj[i].IGST_AMT = round((sublstobj[i].IGST_PER / 100.0) * sublstobj[i].GROSS_AMT, 2);
                sublstobj[i].NET_AMT = round(sublstobj[i].GROSS_AMT + sublstobj[i].SGST_AMT + sublstobj[i].CGST_AMT + sublstobj[i].IGST_AMT, 2);

                $('#txtcgstper' + sublstobj[i].GENID).val(sublstobj[i].CGST_PER);
                $('#lblcgstamt' + sublstobj[i].GENID).text(sublstobj[i].CGST_AMT);

                $('#txtsgstper' + sublstobj[i].GENID).val(sublstobj[i].SGST_PER);
                $('#lblsgstamt' + sublstobj[i].GENID).text(sublstobj[i].SGST_AMT);
                
                $('#lbligstamt' + sublstobj[i].GENID).text(sublstobj[i].IGST_AMT);
                $('#txtnettamt' + sublstobj[i].GENID).val(sublstobj[i].NET_AMT);
                calcamt();
                return false;
            }
        }
    });

    $('.txtdescp').on('input', function () {
        var id = $(this).attr("data-id");
        for (var i = 0; i < sublstobj.length; i++) {
            if (sublstobj[i].GENID == id) {
                sublstobj[i].DESCP = $(this).val();
                return false;
            }
        }
    });

    $('.txtremarks').on('input', function () {
        var id = $(this).attr("data-id");
        for (var i = 0; i < sublstobj.length; i++) {
            if (sublstobj[i].GENID == id) {
                sublstobj[i].REMARKS = $(this).val();
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
}

function calcamt()
{
    othamt = parseFloat($('#OTH_AMT').val()) || 0;
    grossamt = 0;
    cgstamt = 0;
    sgstamt = 0;
    igstamt = 0;
    for (var i = 0; i < sublstobj.length; i++) {
        grossamt = grossamt + parseFloat(sublstobj[i].GROSS_AMT) || 0;
        sgstamt = sgstamt + parseFloat(sublstobj[i].SGST_AMT) || 0;
        cgstamt = cgstamt + parseFloat(sublstobj[i].CGST_AMT) || 0;
        igstamt = igstamt + parseFloat(sublstobj[i].IGST_AMT) || 0;
    }
    grossamt = round(grossamt, 2);
    sgstamt = round(sgstamt, 2);
    cgstamt = round(cgstamt, 2);
    igstamt = round(igstamt, 2);
    netamt = grossamt + cgstamt + sgstamt + igstamt + othamt;
    netamt = round(netamt, 2);
    $("#GROSS_AMT").val(grossamt);
    $("#SGST_AMT").val(sgstamt);
    $("#CGST_AMT").val(cgstamt);
    $("#IGST_AMT").val(igstamt);
    $("#NET_AMT").val(netamt);
}

function round(value, precision) {
    var aPrecision = Math.pow(10, precision);
    return Math.round(value * aPrecision) / aPrecision;
}