﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="BillSettlement.aspx.cs" Inherits="CA_TechServices.Pages.Bill.BillSettlement" %>

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

    <script src="../../Scripts/app/billsettlement.js"></script>

    <div id="loader"></div>
    <div class="col-lg-12" id="mainlistingdiv">
        <div class="panel panel-default">
            <div class="row">
                <div class="col-3">
                    <h2>Bill Settlement</h2>
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
                                <th>No</th>
                                <th>Date</th>
                                <th>C No</th>
                                <th>File No</th>
                                <th>Client</th>
                                <th>Paymode</th>
                                <th>Amount</th>
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
        <div class="panel panel-default">
            <div class="row" style="margin-bottom: 20px">
                <div class="col-12" id="subheaderdiv">
                    <h3 style='color: blue'>Bill Settlement</h3>
                </div>
            </div>

            <div class="row">
                <div class="form-group col-12 col-md-6 col-lg-6">

                    <div class="row">
                        <div class="form-group col-8">
                            <label>Client Name :</label>
                            <input type="text" id="C_NAME" class="form-control" placeholder="Please select a Client." readonly="true" style="background-color: white; color: dodgerblue;" />
                        </div>
                        <div class="form-group col-4" style="text-align:center">
                            <button type="button" id="btnfetch" class="btn btn-primary" style="margin-top: 32px; margin-left: -30px;">Fetch Pending Bills</button>
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-12">
                            <label>Client Details:</label>
                            <label id="lblclientdetails" style="color: brown">Client</label>
                        </div>
                    </div>

                </div>


                <div class="form-group col-12 col-md-6 col-lg-6">
                    <div class="row">
                        <div class="form-group col-3">
                            <label>No</label>
                            <input type="number" id="BS_NO" class="form-control" value="1" style="text-align: center; color: forestgreen; font-weight: 500" />
                        </div>
                        <div class="form-group col-3">
                            <label>Date</label>
                            <input class="form-control datepicker" id="BS_DATE" name="date" placeholder="DD-MM-YYYY" type="text" style="width: 100%; text-align: center; color: forestgreen; font-weight: 500" />
                        </div>
                        <div class="form-group col-6">
                            <label>Paymode</label>
                            <select id="PAYMODE_NAME" class="form-control">
                            </select>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-12 col-md-12 col-lg-12">
                            <label>Remarks</label>
                            <input type="text" id="REMARKS" class="form-control" placeholder="Please enter Remarks." />
                        </div>
                    </div>
                </div>                
            </div>


            <div class="form-group col-12">
                <h5>Bill Details :</h5>
            </div>

            <div id="divservicedetais" style="border: solid 1px black; margin-bottom: 10px; padding-top: 10px">
                <div class="form-group col-12">
                    <div id="divservicedetaisheader" class="row" style="border-bottom: solid 1px black">
                        <div class="form-group col-1" style="margin-bottom: 0px">
                            <label>Bill No</label>
                        </div>
                        <div class="form-group col-2" style="margin-bottom: 0px">
                            <label>Bill Date</label>
                        </div>
                        <div class="form-group col-2" style="margin-bottom: 0px">
                            <label>Bill Amt</label>
                        </div>
                        <div class="form-group col-2" style="margin-bottom: 0px">
                            <label>Paid Amt</label>
                        </div>
                        <div class="form-group col-2" style="margin-bottom: 0px">
                            <label>Bal Amt</label>
                        </div>
                        <div class="form-group col-2" style="margin-bottom: 0px">
                            <label>Settle Amt</label>
                        </div>
                        <div class="form-group col-1" style="margin-bottom: 0px">
                            
                        </div>
                    </div>
                </div>

                <div class="form-group col-12">
                    <div id="divservicedetaisdetails" >
                        <%--<div class='form-group col-1'>                            
                            <input type='number' id='txtbillno' class='form-control' value='1' style='text-align: center;font-weight:bold;color:brown;background-color: white;' disabled="disabled" data-id=''  />
                        </div>
                        <div class='form-group col-2'>                            
                            <input type='text' id='txtbilldate' class='form-control' value='2019-11-11' style='text-align: center;font-weight:bold;color:black;background-color: white;' disabled="disabled" data-id='' />
                        </div>
                        <div class='form-group col-2'>                            
                            <input type='text' id='txtbillamt' class='form-control' value='1' style='text-align: center;font-weight:bold;color:blue;background-color: white;' disabled="disabled" data-id='' />
                        </div>
                        <div class='form-group col-2'>                            
                            <input type='text' id='txtpaidamt' class='form-control' value='1' style='text-align: center;font-weight:bold;color:green;background-color: white;' disabled="disabled" data-id='' />
                        </div>
                        <div class='form-group col-2'>                            
                            <input type='text' id='txtbalamt' class='form-control' value='1' style='text-align: center;font-weight:bold;color:red;background-color: white;' disabled="disabled" data-id='' />
                        </div>
                        <div class='form-group col-2'>                            
                            <input type='text' id='txtbsamt' class='form-control' value='1' style='text-align: center;font-weight:bold;color:deeppink;background-color: white;' data-id='' />
                        </div>
                        <div class='form-group col-1'>                            
                            <img id='btnbillrowdel' class='btnbillrowdel handcursor' src='../../Images/delete.png' style='margin-left: 8px;' data-id='' />
                        </div>--%>
                    </div>
                </div>
            </div>
            <div class="form-group col-12">
                <div class="row">
                    <div class="form-group col-6 col-md-4 col-lg-1">
                    </div>

                    <div class="form-group col-0 col-md-0 col-lg-2">
                    </div>

                    <div class="form-group col-0 col-md-0 col-lg-2">
                    </div>

                    <div class="form-group col-0 col-md-0 col-lg-2">
                    </div>

                    <div class="form-group col-0 col-md-0 col-lg-2">
                    </div>

                    <div class="form-group col-6 col-md-4 col-lg-3">
                        <label>Net Amount</label>
                        <input type="number" id="BS_AMT" class="form-control" value="1" style="text-align: center; background-color: white; color: orangered; font-weight: bold" disabled="disabled" />
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
                                        <th>Client Name</th>
                                        <th>Details</th>
                                        <th>Bills</th>
                                        <th>Bill Amt</th>
                                        <th>Bal Amt</th>                                        
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

    <!-- For Print Popup  -->
    <div class="col-lg-12" id="printdiv" style="display: none">
        <div class="panel panel-default">
            <div class="row">
                <div class="col-12" style="text-align: center">
                    <img src="../../Images/CA%20Logo.png" />
                    <h2 style='color: blue'>Receipt Voucher</h2>
                </div>
            </div>

            <div class="row">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 20%">
                            <label>Voucher No</label>
                        </td>
                        <td style="width: 1%">
                            <label>: </label>
                        </td>
                        <td style="width: 39%">
                            <label id="lblbsno" style="font-weight: bold;"></label>
                        </td>
                        <td style="width: 20%">
                            <label>Voucher Date</label>
                        </td>
                        <td style="width: 1%">
                            <label>: </label>
                        </td>
                        <td style="width: 39%">
                            <label id="lblbsdate" style="font-weight: bold;"></label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>File No</label>
                        </td>
                        <td>
                            <label>: </label>
                        </td>
                        <td>
                            <label id="lblfileno" style="font-weight: bold;"></label>
                        </td>
                        <td>
                            <label>Paymode</label>
                        </td>
                        <td>
                            <label>: </label>
                        </td>
                        <td>
                            <label id="lblpaymode" style="font-weight: bold;"></label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Client</label>
                        </td>
                        <td>
                            <label>: </label>
                        </td>
                        <td colspan="4">
                            <label id="lblcname" style="font-weight: bold;"></label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Address</label>
                        </td>
                        <td>
                            <label>: </label>
                        </td>
                        <td colspan="4">
                            <label id="lblcadd"></label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>PAN</label>
                        </td>
                        <td>
                            <label>: </label>
                        </td>
                        <td>
                            <label id="lblcpan" style="font-weight: bold;"></label>
                        </td>
                        <td>
                            <label>GSTIN</label>
                        </td>
                        <td>
                            <label>: </label>
                        </td>
                        <td>
                            <label id="lblcgstin" style="font-weight: bold;"></label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Remarks</label>
                        </td>
                        <td>
                            <label>: </label>
                        </td>
                        <td colspan="3">
                            <label id="lblreamrks"></label>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="row" id="trvoid">
                <div class="form-group col-12" style="text-align: center">
                    <h3 style='color: red'>*****  Invoice Cancelled  *****</h3>
                </div>
            </div>

            <div class="row">
                <div class="form-group col-12">
                    <h3 style='color: brown'>Bill Details</h3>
                </div>
            </div>

            <div class="table-responsive" id="gridsubdivprn">
                <table id="tablesubprn" style="width: 100%; border-collapse: collapse">
                    <thead>
                        <tr>
                            <th style="border: 1px solid black;">Bill No</th>
                            <th style="border: 1px solid black;">Bill Date</th>
                            <th style="border: 1px solid black;">Bill Amount</th>
                            <th style="border: 1px solid black;">Paid Amt</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>


            <div class="row">
                <table style="width: 100%; margin-top: 15px">
                    <tr>
                        <td colspan="6" style="height:50px">

                        </td>
                    </tr>
                    <tr id="trnetamt">
                        <td style="width: 50%" colspan="3">
                            <label style="font-weight: bold;">For Santhosh G & Associates</label>
                        </td>
                        <td style="width: 30%; text-align: right">
                            <label>Total Amount</label>
                        </td>
                        <td style="width: 1%">
                            <label>: </label>
                        </td>
                        <td style="width: 19%; text-align: center">
                            <label id="lblbsamt" style="color: brown; font-weight: bold;"></label>
                        </td>
                    </tr>
                </table>
            </div>

        </div>
    </div>
    <!-- For Print Popup  -->
</asp:Content>
