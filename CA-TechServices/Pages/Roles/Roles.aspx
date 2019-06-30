<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="CA_TechServices.Pages.Roles.Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../../Content/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <script src="../../Scripts/jquery.dataTables.min.js"></script>
    <script src="../../Scripts/dataTables.bootstrap4.min.js"></script>

    <script src="../../Scripts/app/rolesmaster.js"></script>
    <div class="col-lg-12">
        <div class="panel panel-default">
            <div class="row">
                <div class="col-6">
                    <h2>Roles Master</h2>
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
                                <th>Role Name</th>
                                <th>Active</th>
                                <th></th>
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
    <!-- For Modal Popup Edit Details -->
    <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" id="PopupModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Edit Roles Details</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="panel-body">
                        <div class="form-group col-lg-12">
                            <label>Role Name</label>
                            <input type="text" name="NAME" id="NAME1" class="form-control" placeholder="Please enter Name" />
                        </div>
                        <div class="form-group col-lg-12">
                            <label>Active Status</label>
                            <input type="checkbox" name="ACTIVE_STATUS" id="ACTIVE_STATUS1" style="margin-left: 10px; vertical-align: middle;" />
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSave" class="btn btn-primary">Save Data</button>
                    <button type="button" id="btnUpdate" class="btn btn-primary" edit-id="">Update Data</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <!-- For Modal Popup  -->


    <!-- For Modal Popup Screen Authorizations Details -->
     <%-- <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" id="PopupModalAuth">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Screen Authorization for Roles</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group col-3">
                                <label for="selTransaction">Transaction</label>
                                <select multiple="multiple" class="form-control" id="selTransaction">
                                    <option></option>
                                </select>
                            </div>
                            <div class="form-group col-3">
                                <label for="selReports">Reports</label>
                                <select multiple="multiple" class="form-control" id="selReports">
                                    <option></option>                                    
                                </select>
                            </div>
                            <div class="form-group col-3">
                                <label for="selMaster">Master</label>
                                <select multiple="multiple" class="form-control" id="selMaster">
                                    <option></option>                                  
                                </select>
                            </div>
                            <div class="form-group col-3">
                                <label for="selTools">Tools</label>
                                <select multiple="multiple" class="form-control" id="selTools">
                                    <option></option>                                    
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <label style="margin-right:30px">Please multi select the Screens by using Ctrl/Shift + Click.</label>
                    <button type="button" id="btnSaveAuth" class="btn btn-primary" edit-id="">Save Data</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>--%>
    <!-- For Modal Popup  -->
    <!-- New code for Assigning roles -->
    <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" id="PopupModalAuth">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Screen Authorization for Roles</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="panel-body">
                        <div class="table-responsive">
                            <table id="tbl_Roles" class="table table-striped table-bordered" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>Screen Name</th>
                                        <th>Access</th>
                                        <th>Readonly</th>
                                    </tr>
                                </thead>
                                <tbody id="tbl_Roles_tbody"></tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSaveAuth" class="btn btn-primary" edit-id="">Save Data</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <!-- For Modal Popup  -->

</asp:Content>
