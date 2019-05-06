<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="CA_TechServices.Pages.Login.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../../Scripts/app/changepassword.js"></script>
    <div class="col-12">
        <div class="panel panel-default">
            <div class="row" style="margin-bottom: 20px">
                <div class="col-12">
                    <h2>Change Password</h2>
                </div>
                <div class="col-12">
                </div>
            </div>

            <div class="row">
                <div class="form-group col-sm-12 col-md-6 col-lg-4">
                    <label>Current Password</label>
                    <input type="password" name="current_password" id="current_password1" class="form-control"/>
                </div>
            </div>

            <div class="row">
                <div class="form-group col-sm-12 col-md-6 col-lg-4">
                    <label>New Password</label>
                    <input type="password" name="new_password" id="new_password1" class="form-control"/>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-sm-12 col-md-6 col-lg-4">
                    <label>Confirm Password</label>
                    <input type="password" name="confirm_password" id="confirm_password1" class="form-control"/>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-12 col-md-6 col-lg-4">                    
                    <button type="button" class="btn btn-primary" id="btnsave">Change Password</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
