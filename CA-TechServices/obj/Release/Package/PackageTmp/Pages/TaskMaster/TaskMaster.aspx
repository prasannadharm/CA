<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="TaskMaster.aspx.cs" Inherits="CA_TechServices.Pages.TaskMaster.TaskMaster" %>

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

        #addstage {
            background-color: #3C3B6E;
            color: #ffffff;
            border-radius: 35px;
            height: 40px;
            padding-left: 20px;
            padding-right: 20px;
            border: 0px;
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

    <script src="../../Scripts/app/taskmaster.js"></script>

    <div id="loader"></div>
    <div class="col-lg-12" id="mainlistingdiv">
        <div class="panel panel-default">
            <div class="row">
                <div class="col-3">
                    <h2>Task Master</h2>
                </div>
                <div class="col-9">
                    <button type="button" id="btnAddNew" class="btn btn-success addNewButton" style="position: relative; float: right;">Add New</button>
                </div>
            </div>

            <!-- /.panel-heading -->
            <div class="panel-body" id="maindiv">
                <div class="table-responsive" id="griddiv">
                    <table id="tablemain" class="table table-striped table-bordered" style="width: 100%">
                        <thead>
                            <tr>
                                <th>Task Name</th>
                                <th>Description</th>
                                <th>Priority</th>
                                <th>Frequency</th>
                                <th>Active</th>
                                <th></th>
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

    <!-- For Detail Div  -->
    <div class="col-lg-12" id="mainldetaildiv" style="display: none">
        <div class="panel panel-default">
            <div class="row" style="margin-bottom: 20px">
                <div class="col-12" id="subheaderdiv">
                    <h3 style='color: blue'>Task Master -> Add Task Details</h3>
                </div>
            </div>

            <div class="row">
                <div class="form-group col-12 col-md-6 col-lg-6">
                    <div class="row">
                        <div class="form-group col-12">
                            <h5>Task Details:</h5>
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-12 col-md-9 col-lg-9">
                            <label>Task Name</label>
                            <input type="text" name="T_NAME" id="T_NAME" class="form-control" placeholder="Please enter Task Name" />
                        </div>
                        <div class="form-group col-12 col-md-3 col-lg-3">
                            <label>Priority</label>
                            <input type="number" name="PRIORITY" id="PRIORITY" class="form-control" value="1" style="text-align: center" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-12 col-md-12 col-lg-12">
                            <label>Descritpion</label>
                            <input type="text" name="T_DESC" id="T_DESC" class="form-control" placeholder="Please enter Description." />
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-12 col-md-9 col-lg-9">
                            <label>Occurance</label>
                            <select name="RECURRING_TYPE" id="RECURRING_TYPE" class="form-control" onchange="recurringcombochange()">
                                <option value="">--Select--</option>
                                <option value="Once">Once</option>
                                <option value="Weekly">Once in a Week</option>
                                <option value="Bi-Monthly">Once in 15 Days</option>
                                <option value="Monthly">Once in a Month</option>
                                <option value="Quarterly">Once in a Quarter</option>
                                <option value="Bi-Yearly">Once in 6 Months</option>
                                <option value="Yearly">Once in a Year</option>
                                <option value="Custom">Custom</option>
                            </select>
                        </div>

                        <div class="form-group col-12 col-md-3 col-lg-3">
                            <label>Days</label>
                            <input type="number" name="RECURRING_DAYS" id="RECURRING_DAYS" class="form-control" value="1" style="text-align: center" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-4 col-md-4 col-lg-4">
                            <label>Occurance Day</label>
                            <input type="number" name="RECURRING_START_DAY" id="RECURRING_START_DAY" class="form-control" value="1" style="text-align: center" />
                        </div>

                        <div class="form-group col-4 col-md-4 col-lg-4">
                            <label>Ends On</label>
                            <input class="form-control datepicker" id="RECURRING_END_DATE" name="date" placeholder="DD-MM-YYYY" type="text" style="width: 100%; text-align: center" />
                        </div>

                        <div class="form-group col-4 col-md-4 col-lg-4">
                            <input type="checkbox" name="ACTIVE_STATUS" id="ACTIVE_STATUS" value="ACTIVE_STATUS" style="margin-left: 10px; margin-right: 5px; vertical-align: middle; padding-top: 30px" />
                            <label style="padding-top: 30px">Active</label>
                        </div>
                    </div>


                    <div class="row">
                        <div class="form-group col-12">
                            <h5>Task Stage Details:</h5>
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-12">
                            <div id='StagesGroup' style="margin-bottom: 5px">
                                <%-- <div id="StagesDiv">                                    
                                    <label id='lblslno' style='margin-left: 10px; display: inline; text-align: center;font-weight: bold;color:brown'>1</label>
                                    <input id='txtstages' list='lststages' class='form-control' style='margin-left: 10px; width: 40%; display: inline; margin-bottom: 5px' placeholder="Select Stage"/>
                                    <select id='cmbusers' class='form-control cmbusers' style='margin-left: 10px; width: 25%; display: inline; margin-bottom: 5px'/>
                                    <img id='btnstageup' class='btnstageup handcursor' src='../../Images/up.png' />
                                    <img id='btnstagedown' class='btnstagedown handcursor' src='../../Images/down.png' />
                                    <img id='btnstaedel' class='btnstagedel handcursor' src='../../Images/delete.png' style='margin-left:8px' />
                                </div>--%>
                            </div>
                            <datalist id="lststages">
                            </datalist>
                            <button type="button" value='Add' id='addstage' style="margin-left: 10px">Add Stage</button>
                        </div>
                    </div>
                </div>


                <div class="form-group col-12 col-md-6 col-lg-6">
                    <div class="row">
                        <div class="form-group col-12">
                            <h5>Client Category Mapping Details:</h5>
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-12">
                            <label>Client Category</label>
                            <select id="cmb_CLI_CAT" multiple data-live-search="true" class="filters searchcntrls">
                            </select>
                        </div>                        
                    </div>

                    <div class="row">
                        <div class="form-group col-12">
                            <h5>Client Mapping Details:</h5>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-4">
                            <label>Search By</label>
                            <select id="SEARCHBY" class="form-control">
                                <option value="NAME">Name</option>
                                <option value="C_NO">C NO</option>
                                <option value="FILE_NO">File No</option>
                                <option value="PAN">PAN</option>
                                <option value="AADHAAR">Aadhaar</option>
                                <option value="GSTIN">GSTIN</option>
                                <option value="MOB">Mobile</option>
                                <option value="CLI_GRP">Group</option>
                                <option value="CLI_CAT">Category</option>
                            </select>
                        </div>

                        <div class="form-group col-6">
                            <label>Search Text</label>
                            <input type="text" id="SEARCHTEXT" class="form-control" placeholder="Please enter Search Text." style="margin-left: -15px;" />
                        </div>

                        <div class="form-group col-2">
                            <button type="button" id="btnsearchClient" class="btn btn-primary" style="margin-top: 32px; margin-left: -30px;">Search</button>
                        </div>

                    </div>

                    <div class="row">
                        <div class="form-group col-12" id="detdivclientdetails">
                            <div class="table-responsive" id="griddivDetClientList">
                                <table id="tableDetClientList" class="table table-striped table-bordered" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>C No</th>
                                            <th>Name</th>
                                            <th>File No</th>
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
            </div>




            <div class="row">
                <div class="col-12">
                    <button type="button" id="btnSave" class="btn btn-primary">Save Data</button>
                    <button type="button" id="btnUpdate" class="btn btn-primary" edit-id="">Update Data</button>
                    <button type="button" id="btnCancel" class="btn btn-danger cancelButton" style="margin-right: 15px">Cancel</button>
                </div>
            </div>
        </div>
    </div>
    <!-- For Detail Div  -->

    <!-- For Modal Popup For Task Stages Details -->
    <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" id="PopupModalStageDetails">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header" id="divstageheader">
                    <h4>Task Stages</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="panel-body" id="maindivstagedetails">
                        <div class="table-responsive" id="griddivStageDetails">
                            <table id="tableStageDetails" class="table table-striped table-bordered" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>Sl No</th>
                                        <th>Stage Name</th>
                                        <th>User Assigned</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <!-- For Modal Popup  -->

    <!-- For Modal Popup For Mapped Client Details -->
    <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" id="PopupModalClientList">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header" id="divclientheader">
                    <h4>Clients Mapped to Task -></h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="panel-body" id="maindivclientdetails">
                        <div class="table-responsive" id="griddivClientList">
                            <table id="tableClientList" class="table table-striped table-bordered" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>C No</th>
                                        <th>Name</th>
                                        <th>File No</th>
                                        <th>PAN</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <!-- For Modal Popup  -->

    <!-- For Modal Popup Screen Client Selection via Search -->
    <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" id="PopupModalClientSearch">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header mhs">
                    <h4>Search results for</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="panel-body">
                        <div class="table-responsive" id="gridclientsearchdiv">
                            <table id="tableclientsearch" class="table table-striped table-bordered" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>C No</th>
                                        <th>File No</th>
                                        <th>Name</th>
                                        <th>PAN</th>
                                        <th>Aadhaar</th>
                                        <th>GSTIN</th>
                                        <th>Select</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <!-- For Modal Popup  -->

</asp:Content>
