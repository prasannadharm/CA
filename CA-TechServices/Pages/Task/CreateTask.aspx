<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="CreateTask.aspx.cs" Inherits="CA_TechServices.Pages.Task.CreateTask" %>

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

        #addrow, #addstage {
            background-color: #3C3B6E;
            color: #ffffff;
            border-radius: 35px;
            height: 40px;
            padding-left: 20px;
            padding-right: 20px;
            border: 0px;
        }

        #btnClientDetails, #btnSearchClient {
            background-color: #3C3B6E;
            color: #ffffff;
            border-radius: 35px;
            height: 40px;
            padding-left: 20px;
            padding-right: 20px;
            border: 0px;
        }

        input[type=number]::-webkit-inner-spin-button,
        input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        #btnaddrow {
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

    <script src="../../Scripts/app/createtasks.js"></script>

    <div id="loader"></div>

    <div class="col-lg-12" id="mainlistingdiv">
        <div class="panel panel-default">
            <div class="row">
                <div class="col-3">
                    <h2>Tasks</h2>
                </div>
                <div class="col-9">
                    <label class="control-label" style="display: inline">Date From </label>
                    <input class="form-control datepicker" id="dtpFrom" name="date" placeholder="DD-MM-YYYY" type="text" style="width: 140px; display: inline; text-align: center" />
                    <label class="control-label" style="display: inline">To</label>
                    <input class="form-control datepicker" id="dtpTo" name="date" placeholder="DD-MM-YYYY" type="text" style="width: 140px; display: inline; margin-right: 15px; text-align: center" />
                    <button type="button" id="btnSearch" class="btn btn-success" style="display: inline; margin-right: 10px; margin-top: -5px">Search</button>
                    <button type="button" id="btnAddNew" class="btn btn-success addNewButton" style="position: relative; float: right;">Add New</button>
                </div>
            </div>

            <!-- /.panel-heading -->
            <div class="panel-body" id="maindiv">
                <div class="table-responsive" id="griddiv">
                    <table id="tablemain" class="table table-striped table-bordered" style="width: 100%">
                        <thead>
                            <tr>
                                <th>Task No</th>
                                <th>Date</th>
                                <th>Task Name</th>
                                <th>Client</th>
                                <th>C No</th>
                                <th>File No</th>
                                <th>Sch On</th>
                                <th>Created By</th>
                                <th>Void</th>
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
    <div class="col-lg-12" id="maindetaildiv" style="display: none">
        <div class="panel panel-default" id="pendingtasksdivmain" style="display: none">
            <div class="row" style="margin-bottom: 20px">
                <div class="col-12">
                    <h3 style='color: blue'>Create Task -> Search Pending Task</h3>
                </div>
            </div>

            <div class="row">
                <div class="col-6 col-md-3 col-lg-3">
                    <label>Task Name</label>
                    <select id="cmb_Task_Filter" multiple data-live-search="true" class="filters">
                    </select>
                </div>

                <div class="col-6 col-md-3 col-lg-3">
                    <label>Client Category</label>
                    <select id="cmb_Cli_Cat_Filter" multiple data-live-search="true" class="filters">
                    </select>
                </div>

                <div class="col-6 col-md-3 col-lg-3">
                    <label>Client</label>
                    <select id="cmb_Client_Filter" multiple data-live-search="true" class="filters">
                    </select>
                </div>

                <div class="col-6 col-md-3 col-lg-3">
                    <button type="button" id="btnSearchPendingTasks" class="btn btn-success" style="display: inline; margin-right: 10px; margin-top: 30px">Search</button>
                    <button type="button" id="btnskippending" class="btn btn-primary" style="display: inline; margin-top: -5px; margin-right: 10px; margin-top: 30px">Skip</button>
                    <button type="button" id="btnCancelPending" class="btn btn-danger cancelButton" style="margin-right: 10px; margin-top: 30px;">Cancel</button>
                </div>
            </div>

            <div class="panel-body" id="pendingtaskdiv" style="margin-top: 20px">
                <div class="table-responsive" id="gridpendingtaskdiv">
                    <table id="tablependingtask" class="table table-striped table-bordered" style="width: 100%">
                        <thead>
                            <tr>
                                <th>Task Name</th>
                                <th>Priority</th>
                                <th>Frequency</th>
                                <th>Sch On</th>
                                <th>Client</th>
                                <th>C No</th>
                                <th>File No</th>
                                <th>PAN</th>
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

        <div class="panel panel-default" id="tasksdetailsmaindiv" style="display: none">
            <div class="row" style="margin-bottom: 20px">
                <div class="col-10" id="subheaderdiv">
                    <h3 style='color: blue'>Create Task</h3>
                </div>
                <div class="col-2">
                    <button type="button" id="btnCancelDetailTop" class="btn btn-danger cancelButton" style="margin-right: 10px;">Cancel</button>
                </div>
            </div>

            <div id="createtaskstabsdiv">
                <!-- Nav tabs -->
                <ul class="nav nav-tabs" role="tablist">
                    <li class="nav-item">
                        <a class="nav-link active" data-toggle="tab" href="#divtabtasks">Task Details</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" data-toggle="tab" href="#divtabnotes">Notes</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" data-toggle="tab" href="#divtabdocs">Documents</a>
                    </li>
                </ul>

                <!-- Tab panes -->
                <div class="tab-content">
                    <div id="divtabtasks" class="tab-pane active">
                        <br>
                        <div class="row">
                            <div class="form-group col-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="form-group col-12">
                                        <h5>Task Details:</h5>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="form-group col-12 col-md-6 col-lg-6">
                                        <label>Task No</label>
                                        <input type="number" name="TASKNO" id="TASK_NO" class="form-control" value="1" style="text-align: center" />
                                    </div>

                                    <div class="form-group col-12 col-md-6 col-lg-6">
                                        <label>Task Date</label>
                                        <input class="form-control datepicker" id="TASK_DATE" name="date" placeholder="DD-MM-YYYY" type="text" style="width: 100%; text-align: center" />
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
                                    <div class="form-group col-12">
                                        <h5>Client Details:</h5>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="form-group col-12 col-md-4 col-lg-4">
                                        <label>C No</label>
                                        <input type="text" name="C_NO" id="C_NO" class="form-control" placeholder="Please select Client" />
                                    </div>
                                    <div class="form-group col-12 col-md-5 col-lg-5">
                                        <label>File No</label>
                                        <input type="text" name="FILE_NO" id="FILE_NO" class="form-control" placeholder="Please select Client" />
                                    </div>
                                    <div class="form-group col-12 col-md-3 col-lg-3">
                                        <button type="button" id="btnSearchClient" class="btn btn-success" style="display: inline; margin-right: 10px; margin-top: 30px">Search client</button>
                                    </div>
                                    <div class="form-group col-12 col-md-9 col-lg-9">
                                        <label>Client Name</label>
                                        <input type="text" name="C_NAME" id="C_NAME" class="form-control" placeholder="Please select Client" />
                                    </div>
                                    <div class="form-group col-12 col-md-3 col-lg-3">
                                        <button type="button" id="btnClientDetails" class="btn btn-success" style="display: inline; margin-right: 10px; margin-top: 30px">Client Details</button>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group col-12 col-md-6 col-lg-6">
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
                        </div>
                    </div>
                    <div id="divtabnotes" class="tab-pane fade">
                        <br>
                        <h3>Notes</h3>
                        <p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam.</p>
                    </div>
                    <div id="divtabdocs" class="tab-pane fade">
                        <br>
                        <h3>Documents Required</h3>
                        <p>Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
                    </div>
                </div>
            </div>
        </div>


    </div>
    <!-- For Detail Div  -->

</asp:Content>
