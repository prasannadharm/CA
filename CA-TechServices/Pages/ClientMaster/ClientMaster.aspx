<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ClientMaster.aspx.cs" Inherits="CA_TechServices.Pages.ClientMaster.ClientMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .filters .dropdown-menu > li > a {
            display: block;
            padding: 2px 15px;
            clear: both;
            font-weight: 400;
            line-height: 1.5;
            color: #000000;
            white-space: nowrap;
        }

        .handcursor {
            cursor: pointer;
            cursor: hand;
        }
    </style>

    <link href="../../Content/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <link href="../../Content/ajaxloader.css" rel="stylesheet" />
    <link href="../../Content/bootstrap-select.css" rel="stylesheet" />

    <link href="../../Content/bootstrap-datepicker3.css" rel="stylesheet" />
    <link href="../../Content/jquery-ui.min.css" rel="stylesheet" />
    <link href="../../Content/dataTables.bootstrap4.min.css" rel="stylesheet" />

    <script src="../../Scripts/jquery.dataTables.min.js"></script>
    <script src="../../Scripts/dataTables.bootstrap4.min.js"></script>
    <script src="../../Scripts/AjaxFileupload.js"></script>
    <script src="../../Scripts/popper2019.min.js"></script>
    <script src="../../Scripts/bootstrap-select.min.js"></script>
        
    <script src="../../Scripts/bootstrap-datepicker.min.js"></script>
    <script src="../../Scripts/jquery-ui.min.js"></script>

    <script src="../../Scripts/app/clientmaster.js"></script>

    <div id="loader"></div>
    <div class="col-lg-12" id="mainlistingdiv">
        <div class="panel panel-default">
            <div class="row">
                <div class="col-6">
                    <h2>Client Master</h2>
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
                                <th>Client ID</th>
                                <th>File No</th>
                                <th>Name</th>
                                <th>Mobile</th>
                                <th>PAN</th>
                                <th>GSTIN</th>
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

    <!-- For Detail Div  -->
    <div class="col-lg-12" id="mainldetaildiv" style="display: none">
        <div class="panel panel-default">
            <div class="row">
                <div class="col-12" id="subheaderdiv">
                    <h2 style='color: blue'>Client Master</h2>
                </div>
            </div>

            <div class="row">
                <div class="form-group col-12 col-md-6 col-lg-6">
                    <label>Client Name</label>
                    <input type="text" name="C_NAME" id="C_NAME" class="form-control" placeholder="Please enter Client Name" />
                </div>
                <div class="form-group col-12 col-md-3 col-lg-3">
                    <label>Alias Name</label>
                    <input type="text" name="ALIAS" id="ALIAS" class="form-control" placeholder="Please enter Alias Name" />
                </div>
                <div class="form-group col-12 col-md-3 col-lg-3">
                    <label>File No</label>
                    <input type="text" name="FILE_NO" id="FILE_NO" class="form-control" placeholder="Please enter File No." />
                </div>
            </div>

            <div class="row">
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>Father's Name</label>
                    <input type="text" name="FNAME" id="FNAME" class="form-control" placeholder="Please enter Father's Name." />
                </div>
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>Gender</label>
                    <select name="GENDER" id="GENDER" class="form-control" onchange="GenederComboChange()">
                        <option value="">--Select--</option>
                        <option value="Male">Male</option>
                        <option value="Female">Female</option>
                    </select>
                </div>
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>Husband Name</label>
                    <input type="text" name="HNAME" id="HNAME" class="form-control" placeholder="Please enter Husband Name." />
                </div>
            </div>

            <div class="row">
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>Contact Person</label>
                    <input type="text" name="CNT_NAME" id="CNT_NAME" class="form-control" placeholder="Please enter Contact Person Name." />
                </div>
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>Client Group</label>
                    <select name="CLI_GRP_NAME" id="CLI_GRP_NAME" class="form-control">
                        <option></option>
                    </select>
                </div>
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>Client Category</label>                   
                    <select id="CLI_CAT" multiple data-live-search="true" class="filters">
                    </select>
                </div>
            </div>

            <div class="row">
                <div class="form-group col-12 col-md-3 col-lg-3">
                    <label>Date of Birth</label>                   
                    <input class="form-control datepicker" id="DOB" name="date" placeholder="DD-MM-YYYY" type="text" style="width: 100%; text-align: center" />
                </div>
                <div class="form-group col-12 col-md-3 col-lg-3">
                    <label>PAN</label>
                    <input type="text" name="PAN" id="PAN" class="form-control" placeholder="Please enter PAN NO." />
                </div>
                <div class="form-group col-12 col-md-3 col-lg-3">
                    <label>Aadhaar</label>
                    <input type="text" name="AADHAAR" id="AADHAAR" class="form-control" placeholder="Please enter Aadhaar No." />
                </div>
                <div class="form-group col-12 col-md-3 col-lg-3">
                    <label>GSTIN</label>
                    <input type="text" name="GSTIN" id="GSTIN" class="form-control" placeholder="Please enter GSTIN No." />
                </div>
            </div>

            <div class="row">
                <div class="form-group col-12">
                    <label>Permanent Address</label>
                    <input type="text" name="ADDR" id="ADDR" class="form-control" placeholder="Please enter Address." />
                </div>
            </div>

            <div class="row">
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>State</label>
                    <select name="STATE" id="STATE" class="form-control" onchange="StateComboChange()">
                        <option></option>
                    </select>
                </div>
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>City</label>
                    <select name="CITY" id="CITY" class="form-control">
                        <option></option>
                    </select>
                </div>
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>PIN</label>
                    <input type="text" name="PIN" id="PIN" class="form-control" />
                </div>
            </div>

            <div class="row">
                <div class="form-group col-12">
                    <label>Present Address</label>
                    <input type="checkbox" name="SAME_AB" id="SAME_AB" value="SAMEAB" style="margin-left: 10px; margin-right: 5px; vertical-align: middle;" onchange="chkaddrchanged()" />
                    <label>Same as Above</label>
                    <input type="text" name="ADDR1" id="ADDR1" class="form-control" placeholder="Please enter Address." />
                </div>
            </div>

            <div class="row">
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>State</label>
                    <select name="STATE1" id="STATE1" class="form-control" onchange="StateComboChange1()">
                        <option></option>
                    </select>
                </div>
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>City</label>
                    <select name="CITY1" id="CITY1" class="form-control">
                        <option></option>
                    </select>
                </div>
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>PIN</label>
                    <input type="text" name="PIN1" id="PIN1" class="form-control" />
                </div>
            </div>

            <div class="row">
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>Phone No</label>
                    <input type="text" name="PH_NO" id="PH_NO" class="form-control" placeholder="Please enter Phone No" />
                </div>
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>Mobile No. : 1</label>
                    <input type="text" name="MOBILE_NO1" id="MOBILE_NO1" class="form-control" placeholder="Please enter Mobile No" />
                </div>
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>Mobile No. : 2</label>
                    <input type="text" name="MOBILE_NO2" id="MOBILE_NO2" class="form-control" placeholder="Please enter Mobile No" />
                </div>
            </div>

            <div class="row">
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>Email ID</label>
                    <input type="text" name="EMAIL_ID" id="EMAIL_ID" class="form-control" placeholder="Please enter Email Address." />
                </div>
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>Ward No</label>
                    <input type="text" name="WARD" id="WARD" class="form-control" placeholder="Please enter Ward No." />
                </div>
                <div class="form-group col-12 col-md-4 col-lg-4">
                    <label>Rack No</label>
                    <input type="text" name="RACK_NO" id="RACK_NO" class="form-control" placeholder="Please enter Rack No." />
                </div>
            </div>


            <div class="row">
                <div class="form-group col-12">
                    <label>Alert Message</label>
                    <input type="checkbox" name="ACTIVE_STATUS" id="ACTIVE_STATUS" value="ACTIVE_STATUS" style="margin-left: 10px; margin-right: 5px; vertical-align: middle;" />
                    <label>Active Status</label>
                    <input type="text" name="Alert_Msg" id="ALERT_MSG" class="form-control" placeholder="Please enter Alert Message." />
                </div>
            </div>
           

            <div class="row">
                <div class="col-12">
                    <button type="button" id="btnSave" class="btn btn-primary">Save Data</button>
                    <button type="button" id="btnUpdate" class="btn btn-primary" edit-id="">Update Data</button>
                    <button type="button" id="btnCancel" class="btn btn-danger cancelButton" style="margin-right: 15px">Cancel</button>
                    <label style="margin-left: 10px">Client ID</label>
                    <label id="C_ID" style="color: brown; margin-left: 5px; margin-right: 20px; font-weight: 500">Client ID</label>
                </div>
            </div>
        </div>
    </div>
    <!-- For Detail Div  -->
</asp:Content>
