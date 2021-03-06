﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="CreateTask.aspx.cs" Inherits="CA_TechServices.Pages.Task.CreateTask" %>

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

        #addrow {
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
    <div class="col-lg-12" id="mainldetaildiv" style="display: none">
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
                    <button type="button" id="btnskippending" class="btn btn-primary" style="display: inline; margin-top: -5px; margin-right: 10px; margin-top:30px">Skip</button>                    
                </div>
            </div>

            <div class="panel-body" id="pendingtaskdiv" style="margin-top:20px">
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
                <div class="col-12" id="subheaderdiv">
                    <h3 style='color: blue'>Create Task</h3>
                </div>
            </div>

            <div class="row">
            </div>
        </div>


    </div>
    <!-- For Detail Div  -->

</asp:Content>
