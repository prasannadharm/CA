﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="CA_TechServices.Pages.Dashboard.UserDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <%--<img src="../../Images/dashboard.png" style="align-content:center" />--%>
    <table>
        <tr>
            <td>
                <h3>Work Progress</h3>
            </td>
        </tr>
        <tr>
            <td>
                <h4>Completed</h4>
            </td>
        </tr>
        <tr>
            <td>1. All Master screens except Client & Task Master</td>
        </tr>
        <tr>
            <td>2. User Management Screens</td>
        </tr>
        <tr>
            <td>3. Static website content Part</td>
        </tr>
        <tr>
            <td>
                <h4>Work In Progress</h4>
            </td>
        </tr>
        <tr>
            <td>1. Master -> Client Master: (Add, Edit, Delete & Listing Completed) & (Client Search, Doc Upload & Password manager pending)</td>
        </tr>
        <tr>
            <td>2. Master -> Task Master</td>
        </tr>
        <tr>
            <td>
                <h4>Pending</h4>
            </td>
        </tr>
        <tr>
            <td>1. All Transaction Screens: (Create Task, My Tasks, Billing & Settlement Screens)</td>
        </tr>
        <tr>
            <td>2. All Reports Screens: (Task Register, Pending Task Register, Client Report, Billing Rgister, Pending Bills & Settlement Registers)</td>
        </tr>
        <tr>
            <td>3. Dashoard Screen</td>
        </tr>
    </table>
</asp:Content>