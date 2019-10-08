$(document).ready(function () {
    getDetails();
});

function getDetails() {
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
            $('#tablemain').append("<thead><tr><th>Client ID</th><th>File No</th><th>Name</th><th>Mobile</th><th>PAN</th><th>GSTIN</th><th>Active</th><th></th><th></th></tr></thead><tbody></tbody>");
            $('#tablemain tbody').remove();
            $('#tablemain').append("<tbody>");
            for (var i = 0; i < data.d.length; i++) {
                $('#tablemain').append(
                    "<tr><td>" + data.d[i].C_ID + "</td>" +
                    "<td>" + data.d[i].FILE_NO + "</td>" +
                    "<td>" + data.d[i].C_NAME + "</td>" +
                    "<td>" + data.d[i].MOBILE_NO1 + "</td>" +
                    "<td>" + data.d[i].PAN + "</td>" +
                    "<td>" + data.d[i].GSTIN + "</td>" +                    
                    "<td>" + "<input type='checkbox' onclick='return false;' " + (data.d[i].ACTIVE_STATUS == true ? "checked='checked'" : "") + "/></td>" +
                    "<td>" + "<input type='button' class='btn btn-warning btn-sm editButton' data-id='" + data.d[i].C_ID + "' name='submitButton' id='btnEdit' value='Edit' />" + "</td>" +
                    "<td><input type='button' class='btn btn-danger btn-sm deleteButton' data-id='" + data.d[i].C_ID + "' name='submitButton' id='btnDelete' value='Delete'/> </td></tr>");
            }
            $('#tablemain').append("</tbody>");
            $('#tablemain').DataTable();
            //data-toggle='modal' data-target='#PopupModal'
        },
        error: function () {
            alert("Error while Showing update data");
        }

        //
    });
}

$(function () {
    $("#btnSave").click(function () {
        if ($("#CLI_CAT_NAME1").val().trim() == "") {
            alert("Please enter Client category.");
            $("#CLI_CAT_NAME1").focus();
            return false;
        }

        var obj = {};
        obj.CLI_CAT_NAME = $("#CLI_CAT_NAME1").val();
        if ($('#ACTIVE_STATUS1').is(":checked")) {
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
                        getDetails();
                        alert(data.d[i].MSG);
                        $('#PopupModal').modal('hide');
                    }
                    else {
                        alert(data.d[i].MSG);
                        $("#CLI_CAT_NAME1").focus();
                        return false;
                    }
                }
            },
            error: function (data) {
                alert("Error while Adding data of :" + obj.NAME);
                $("#CLI_CAT_NAME1").focus();
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
                            getDetails();
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
        $('#PopupModal').modal('show');
        $('#PopupModal').focus();
        $("#CLI_CAT_NAME1").val('');
        $("#ACTIVE_STATUS1").prop('checked', true);
        $("div.modal-header h2").html("Add Client Details");
        $('#CLI_CAT_NAME1').focus();
    });

    $(document).on("click", ".editButton", function () {
        $('#btnSave').hide();
        $('#btnUpdate').show();
        $('#PopupModal').modal('show');
        $('#PopupModal').focus();
        $("#CLI_CAT_NAME1").val("");
        $("div.modal-header h2").html("Edit Client Details");
        var id = $(this).attr("data-id");
        console.log(id);
        $("#btnUpdate").attr("edit-id", id);
        //alert(id);  //getting the row id 
        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "ClientMaster.aspx/EditData",
            data: '{id: ' + id + '}',
            dataType: "json",
            success: function (data) {
                for (var i = 0; i < data.d.length; i++) {
                    $("#CLI_CAT_NAME1").val(data.d[i].CLI_CAT_NAME);
                    if (data.d[i].ACTIVE_STATUS == true)
                        $("#ACTIVE_STATUS1").prop('checked', true);
                    else
                        $("#ACTIVE_STATUS1").prop('checked', false);
                }
                $('#CLI_CAT_NAME1').focus();
            },
            error: function () {
                alert("Error while retrieving data of :" + id);
            }
        });
    });

    $("#btnUpdate").click(function () {
        if ($("#CLI_CAT_NAME1").val().trim() == "") {
            alert("Please enter Client category.");
            $("#CLI_CAT_NAME1").focus();
            return false;
        }

        var id = $(this).attr("edit-id");
        var obj = {};
        obj.CLI_CAT_ID = id;
        obj.CLI_CAT_NAME = $("#CLI_CAT_NAME1").val();
        if ($('#ACTIVE_STATUS1').is(":checked")) {
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
                        getDetails();
                        alert(data.d[i].MSG);
                        $('#PopupModal').modal('hide');
                    }
                    else {
                        alert(data.d[i].MSG);
                        $("#CLI_CAT_NAME1").focus();
                        return false;
                    }
                }
            },
            error: function (data) {
                alert("Error while Updating data of :" + id);
                $("#CLI_CAT_NAME1").focus();
                return false;
            }
        });
    });

});