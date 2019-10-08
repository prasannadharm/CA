<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="CA_TechServices.Pages.Company.Company" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <script src="../../Scripts/app/companymaster.js"></script>
    <div class="col-lg-12">
        <div class="panel panel-default">
            <div class="row" style="margin-bottom:20px">
                <div class="col-6">
                    <h2>Company Details</h2>
                </div>
                <div class="col-6">
                </div>
            </div>
            
            <div class="row">
                <div class="form-group col-12">
                    <label>Company Name</label>
                    <input type="text" name="COMPANY_NAME" id="COMPANY_NAME1" class="form-control" placeholder="Please enter Company name" />
                </div>
            </div>

            <div class="row">
                <div class="form-group col-12">
                    <label>Address</label>
                    <input type="text" name="ADDRESS" id="ADDRESS1" class="form-control" placeholder="Please enter address" />
                </div>
            </div>

            <div class="row">
                <div class="form-group col-4">
                    <label>State</label>
                    <select name="STATE" id="STATE1" class="form-control" onchange="StateComboChange()"> 
                        <option></option>
                    </select>
                </div>

                <div class="form-group col-4">
                    <label>City</label>
                    <select name="CITY" id="CITY1" class="form-control">
                        <option></option>
                    </select>
                </div>

                <div class="form-group col-4">
                    <label>Pin</label>
                    <input type="text" name="PIN_NO" id="PIN_NO1" class="form-control" placeholder="Please enter PIN" />
                </div>
            </div>

            <div class="row">
                <div class="form-group col-6">
                    <label>GSTIN</label>
                    <input type="text" name="GSTIN" id="GSTIN1" class="form-control" placeholder="Please enter GSTIN" />
                </div>

                <div class="form-group col-6">
                    <label>Telephone</label>
                    <input type="text" name="TELEPHONE" id="TELEPHONE1" class="form-control" placeholder="Please enter Telephone No" />
                </div>              
            </div>

            <div class="row">
                <div class="form-group col-6">
                    <label>FAX</label>
                    <input type="text" name="FAX" id="FAX1" class="form-control" placeholder="Please enter FAX No" />
                </div>

                <div class="form-group col-6">
                    <label>E-Mail</label>
                    <input type="text" name="EMAIL" id="EMAIL1" class="form-control" placeholder="Please enter email address" />
                </div>              
            </div>

            <div class="row">
                <div class="form-group col-12">
                    <label>Website</label>
                    <input type="text" name="WEB" id="WEB1" class="form-control" placeholder="Please enter Website address" />
                </div>                        
            </div>

            <div class="row">                
                <div class="col-12">
                    <button type="button" id="btnSave" class="btn btn-primary" style="position: relative; float: right;">Save</button>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
