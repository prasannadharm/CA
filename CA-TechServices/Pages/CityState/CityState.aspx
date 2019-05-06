<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="CityState.aspx.cs" Inherits="CA_TechServices.Pages.CityState.CityState" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../../Content/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <script src="../../Scripts/jquery.dataTables.min.js"></script>
    <script src="../../Scripts/dataTables.bootstrap4.min.js"></script>    
    <script src="../../Scripts/app/citystate.js"></script>
    <div class="col-lg-12">
        <div class="panel panel-default">
            <div class="row">
                <div class="col-6">
                    <h2>City State Master</h2>
                </div>
                <div class="col-6">
                    <button type="button" id="btnAddNew" class="btn btn-success addNewButton" style="position: relative; float: right;">Add New</button>
                </div>
            </div>

            <!-- /.panel-heading -->
            <div class="panel-body" id="maindiv">
                <div class="table-responsive" id="griddiv">
                    <table id="tablemain" class="table table-striped table-bordered" style="width: 100%">
                        <thead>
                            <tr>
                                <th>City</th>
                                <th>State</th>
                                <th>Active</th>
                                <th></th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <!-- For Modal Popup  -->
    <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" id="PopupModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Edit City Details</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="panel-body">
                        <div class="form-group col-lg-12">
                            <label>City Name</label>
                            <input type="text" name="CITY" id="CITY1" class="form-control" placeholder="Please enter City Name"/>                            
                        </div>
                        <div class="form-group col-lg-12">
                            <label>State Name</label>
                            <select class="form-control" name="STATE" id="STATE1"></select>                            
                        </div>
                        <div class="form-group col-lg-12">
                            <label>Active Status</label>
                            <input type="checkbox" name="ACTIVE_STATUS" id="ACTIVE_STATUS1" style="margin-left: 10px; vertical-align: middle;" />
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSave" class="btn btn-primary">Save Data</button>
                    <button type="button" id="btnUpdate" class="btn btn-primary" edit-id="" >Update Data</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <!-- For Modal Popup  -->
</asp:Content>
