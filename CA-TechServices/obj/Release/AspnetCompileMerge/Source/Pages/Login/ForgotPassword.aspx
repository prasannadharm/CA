<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="CA_TechServices.Pages.Login.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
    <link rel="shortcut icon" href="../../Images/favicon.ico" type="image/x-icon"/>  
    <style>
        

        .form-signin {
            max-width: 420px;
            padding: 30px 38px;
            margin: 0 auto;
            margin-top: 40px;
            /*background-color: #eee;*/
            border: 3px dotted rgba(0,0,0,0.1);
        }

        .form-control {
            position: relative;
            font-size: 16px;
            height: auto;
            padding: 10px;
        }      
    </style>
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/app/forgotpassword.js"></script>   
</head>
<body>
    <div class="container">
    <form id="form1" runat="server" class="form-signin">
       
         
            <div class="row" >
                <div class="col-12">
                    <h2>Forgot Password</h2>
                </div>
                <div class="col-12">
                </div>
            </div>

            <div class="row" style="margin-bottom: 10px">
                <div class="col-12">
                    <label>Please enter Email address</label>
                    <input type="email" name="email" id="email1" class="form-control"/>
                </div>
            </div>
            
            <div class="row" style="margin-bottom: 10px">
                <div class="col-12">                    
                    <button type="button" class="btn btn-primary" id="btngetdetails" style="width:100%">Retrive Password</button>                  
                </div>
            </div>
            <div class="row">
                <div class="col-12">                                                            
                    <asp:HyperLink ID="linkLogin" NavigateUrl="UserLogin.aspx" CssClass="text-primary btn-link" runat="server">Return to Login Page</asp:HyperLink>                
                </div>
            </div>
        
   
    </form>
        </div>
</body>
</html>
