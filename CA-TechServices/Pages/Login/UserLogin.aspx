<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="CA_TechServices.Pages.Login.UserLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Santhosh G & Associates</title>
    <%--<link href="../../Content/font-awesome.min.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    
    <style>
        .wrapper {
            margin-top: 10px;
            margin-bottom: 10px;
        }

        .form-signin {
            max-width: 420px;
            padding: 30px 38px;
            margin: 0 auto;
            /*background-color: #eee;*/
            border: 3px dotted rgba(0,0,0,0.1);
        }

        .form-signin-heading {
            text-align: center;
            margin-bottom: 10px;
        }

        .form-control {
            position: relative;
            font-size: 16px;
            height: auto;
            padding: 10px;
        }


        .colorgraph {
            height: 7px;
            border-top: 0;
            background: #c4e17f;
            border-radius: 5px;
            background-image: -webkit-linear-gradient(left, #c4e17f, #c4e17f 12.5%, #f7fdca 12.5%, #f7fdca 25%, #fecf71 25%, #fecf71 37.5%, #f0776c 37.5%, #f0776c 50%, #db9dbe 50%, #db9dbe 62.5%, #c49cde 62.5%, #c49cde 75%, #669ae1 75%, #669ae1 87.5%, #62c2e4 87.5%, #62c2e4);
            background-image: -moz-linear-gradient(left, #c4e17f, #c4e17f 12.5%, #f7fdca 12.5%, #f7fdca 25%, #fecf71 25%, #fecf71 37.5%, #f0776c 37.5%, #f0776c 50%, #db9dbe 50%, #db9dbe 62.5%, #c49cde 62.5%, #c49cde 75%, #669ae1 75%, #669ae1 87.5%, #62c2e4 87.5%, #62c2e4);
            background-image: -o-linear-gradient(left, #c4e17f, #c4e17f 12.5%, #f7fdca 12.5%, #f7fdca 25%, #fecf71 25%, #fecf71 37.5%, #f0776c 37.5%, #f0776c 50%, #db9dbe 50%, #db9dbe 62.5%, #c49cde 62.5%, #c49cde 75%, #669ae1 75%, #669ae1 87.5%, #62c2e4 87.5%, #62c2e4);
            background-image: linear-gradient(to right, #c4e17f, #c4e17f 12.5%, #f7fdca 12.5%, #f7fdca 25%, #fecf71 25%, #fecf71 37.5%, #f0776c 37.5%, #f0776c 50%, #db9dbe 50%, #db9dbe 62.5%, #c49cde 62.5%, #c49cde 75%, #669ae1 75%, #669ae1 87.5%, #62c2e4 87.5%, #62c2e4);
        }
    </style>

    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>

</head>
<body>
    <div class="container">
        <img src="../../Images/CA%20Logo.png" alt="Logo" />
        <div class="wrapper">
            <form id="form1" runat="server" name="Login_Form" class="form-signin">


                <h3 class="form-signin-heading">Welcome Back! Please Sign In</h3>
                <hr class="colorgraph" />         

                <div class="card-body">
                    <div class="form-group bottom">
                        <label>Username (Email)</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <div class="input-group-text"><i class="fa fa-envelope"></i></div>
                            </div>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="rfvEmail" Display="Dynamic" ControlToValidate="txtEmail" CssClass="text-danger" runat="server" ErrorMessage="Please enter email address"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" ControlToValidate="txtEmail" CssClass="text-danger" runat="server" ErrorMessage="Enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group bottom">
                        <label>Password</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <div class="input-group-text"><i class="fa fa-lock"></i></div>
                            </div>
                            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="rfvPassword" ControlToValidate="txtPassword" CssClass="text-danger" runat="server" ErrorMessage="Please enter password"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnLogin" CssClass="btn btn-success rounded-0 btn-block" runat="server" Text="Login" OnClick="btnLogin_Click" />
                    </div>
                    <div class="form-group text-center">
                        <asp:HyperLink ID="linkRegistration" NavigateUrl="~/RegisterForm.aspx" CssClass="text-primary btn-link" runat="server">New User</asp:HyperLink>
                        <asp:HyperLink ID="linkForgotPassword" NavigateUrl="~/ForgotPassword.aspx" CssClass="text-primary btn-link" runat="server">Forgot Password</asp:HyperLink>
                    </div>
                    <div class="text-center">
                        <asp:Label ID="lblMessage" CssClass="text-danger" runat="server"></asp:Label>
                    </div>
                </div>
                
            </form>
        </div>
    </div>
</body>
</html>
