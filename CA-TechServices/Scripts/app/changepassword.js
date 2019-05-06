$(function () {
    $("#btnsave").click(function () {
        if ($("#current_password1").val().trim() == "") {
            alert("Please enter old password.");
            $("#current_password1").focus();
            return false;
        }

        if ($("#new_password1").val().trim() == "") {
            alert("Please enter new password.");
            $("#new_password1").focus();
            return false;
        }

        if ($("#confirm_password1").val().trim() == "") {
            alert("Please re-enter new password.");
            $("#confirm_password1").focus();
            return false;
        }

        if ($("#new_password1").val().trim() != $("#confirm_password1").val().trim()) {
            alert("Please enter macthing new password.");
            $("#confirm_password1").focus();
            return false;
        }

        var obj = {};
        obj.USER_ID = 0;
        obj.OLD_PASSWORD = $("#current_password1").val();
        obj.NEW_PASSWORD = $("#confirm_password1").val();

        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "ChangePassword.aspx/UpdateData",
            data: '{obj: ' + JSON.stringify(obj) + '}',
            dataType: "json",
            success: function (data) {
                for (var i = 0; i < data.d.length; i++) {
                    if (data.d[i].RESULT === 1) {
                        alert(data.d[i].MSG);
                        window.location = 'UserLogin.aspx';
                    }
                    else {
                        alert(data.d[i].MSG);
                        $("#current_password1").focus();
                        return false;
                    }
                }
            },
            error: function (data) {
                alert("Error while Adding data of :" + obj.NAME);
                $("#current_password1").focus();
                return false;
            }
        });

    });

});