<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Bill.aspx.cs" Inherits="CA_TechServices.Pages.Bill.Bill" %>

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

    <script src="../../Scripts/app/bill.js"></script>

    <div id="loader"></div>
    <div class="col-lg-12" id="mainlistingdiv">
        <div class="panel panel-default">
            <div class="row">
                <div class="col-3">
                    <h2>Billing</h2>
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
                                <th>Bill No</th>
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
                        <div class="form-group col-12">
                            <label>Name :</label>
                            <input type="text" id="C_NAME" class="form-control" placeholder="Please select a Client." readonly="true" style="background-color: white; color: dodgerblue;" />
                        </div>
                    </div>
                </div>


                <div class="form-group col-12 col-md-6 col-lg-6">
                    <div class="row">
                        <div class="form-group col-2">
                            <label>Bill No</label>
                            <input type="number" id="BILL_NO" class="form-control" value="1" style="text-align: center; color: forestgreen;font-weight:500" />
                        </div>
                        <div class="form-group col-3">
                            <label>Bill Date</label>
                            <input class="form-control datepicker" id="BILL_DATE" name="date" placeholder="DD-MM-YYYY" type="text" style="width: 100%; text-align: center; color: forestgreen;font-weight:500" />
                        </div>
                        <div class="form-group col-4">
                            <label>Paymode</label>
                            <select id="PAYMODE_NAME" class="form-control">
                            </select>
                        </div>
                        <div class="form-group col-3">
                            <label>Due Date</label>
                            <input class="form-control datepicker" id="DUE_DATE" name="date" placeholder="DD-MM-YYYY" type="text" style="width: 100%; text-align: center; color: red;" />
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
                <h5>Service Details :</h5>
            </div>

            <div id="divservicedetais" style="border: solid 1px black; margin-bottom: 10px; padding-top: 10px">
                <div class="form-group col-12">
                    <div id="divservicedetaisheader" class="row" style="border-bottom: solid 1px black">
                        <div class="form-group col-3" style="margin-bottom: 0px">
                            <label>Service Description</label>
                        </div>
                        <div class="form-group col-6" style="margin-bottom: 0px">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 20%">
                                        <label>Amount</label>
                                    </td>
                                    <td style="width: 20%">
                                        <label>SGST (% & Rs)</label>
                                    </td>
                                    <td style="width: 20%">
                                        <label>CGST (% & Rs)</label>
                                    </td>
                                    <td style="width: 20%">
                                        <label>IGST (% & Rs)</label>
                                    </td>
                                    <td style="width: 20%">
                                        <label>Net Amt</label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="form-group col-3" style="margin-bottom: 0px">
                            <label>Remarks</label>
                        </div>
                    </div>
                </div>

                <div class="form-group col-12">
                    <div id="divservicedetaisdetails">
                        <%--<div class='form-group col-3'>
                            <label id='lblslno' data-id='' style='text-align: center; color: brown; font-weight: 500; display: inline'>1</label>
                            <input type='text' id='txtdescp' data-id='' class='form-control' placeholder='Enter Description.' style='width: 88%; display: inline' />
                        </div>
                        <div class='form-group col-6'>
                            <table style='width: 100%'>
                                <tr>
                                    <td style='width: 20%;padding-right:20px'>
                                        <input type='number' id='txtgrossamt' class='form-control' value='1' style='text-align: center;width:100%' />
                                    </td>
                                    <td style='width: 20%'>
                                        <input type='number' id='txtsgstper' class='form-control' value='1' style='text-align: center; width: 50%; display: inline' data-id='' />
                                        <label id='lblsgstamt' data-id='' style='text-align: center; color: blue; font-weight: 500; display: inline; width: 50%'>1</label>
                                    </td>
                                    <td style='width: 20%'>
                                        <input type='number' id='txtcgstper' class='form-control' value='1' style='text-align: center; width: 50%; display: inline' data-id='' />
                                        <label id='lblcgstamt' data-id='' style='text-align: center; color: blue; font-weight: 500; display: inline; width: 50%'>1</label>
                                    </td>
                                    <td style='width: 20%'>
                                        <input type='number' id='txtigstper' class='form-control' value='1' style='text-align: center; width: 50%; display: inline' data-id='' />
                                        <label id='lbligstamt' data-id='' style='text-align: center; color: blue; font-weight: 500; display: inline; width: 50%'>1</label>
                                    </td>
                                    <td style='width: 20%'>
                                        <input type='number' id='txtnettamt' class='form-control' value='1' style='text-align: center;width:100%' disabled='disabled' data-id='' />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="form-group col-3">
                            <input type='text' id='txtremarks' data-id='' class='form-control' placeholder='Enter remarks.' style='display: inline; width: 80%' />
                            <img id='btnbillrowdel' class='btnbillrowdel handcursor' src='../../Images/delete.png' style='margin-left: 8px; display: inline;' data-id='' />
                        </div>--%>
                    </div>


                    <div class='form-group col-12'>
                        <button type="button" value='Add' id='btnaddrow' style="margin-left: 10px">Add Row</button>
                    </div>

                </div>
            </div>
            <div class="form-group col-12">
                <div class="row">
                    <div class="form-group col-6 col-md-4 col-lg-2">
                        <label>Gross Amount</label>
                        <input type="number" id="GROSS_AMT" class="form-control" value="1" style="text-align: center; background-color: white; font-weight: 500; color: darkmagenta;" disabled="disabled" />
                    </div>

                    <div class="form-group col-6 col-md-4 col-lg-2">
                        <label>SGST Amount</label>
                        <input type="number" id="SGST_AMT" class="form-control" value="1" style="text-align: center; background-color: white; font-weight: 500; color: darkmagenta;" disabled="disabled" />
                    </div>

                    <div class="form-group col-6 col-md-4 col-lg-2">
                        <label>CGST Amount</label>
                        <input type="number" id="CGST_AMT" class="form-control" value="1" style="text-align: center; background-color: white; font-weight: 500; color: darkmagenta;" disabled="disabled" />
                    </div>

                    <div class="form-group col-6 col-md-4 col-lg-2">
                        <label>IGST Amount</label>
                        <input type="number" id="IGST_AMT" class="form-control" value="1" style="text-align: center; background-color: white; font-weight: 500; color: darkmagenta;" disabled="disabled" />
                    </div>

                    <div class="form-group col-6 col-md-4 col-lg-2">
                        <label>Others</label>
                        <input type="number" id="OTH_AMT" class="form-control" value="1" style="text-align: center; background-color: white; font-weight: 500; color: blue;" />
                    </div>

                    <div class="form-group col-6 col-md-4 col-lg-2">
                        <label>Net Amount</label>
                        <input type="number" id="NET_AMT" class="form-control" value="1" style="text-align: center; background-color: white; color: orangered; font-weight: 500" disabled="disabled" />
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
