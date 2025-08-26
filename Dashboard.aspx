<%@ Page Title="Dashboard" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ElectricityBillProject.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container-fluid">
    <h1 class="h3 mb-4 text-gray-800">Dashboard</h1>
    <div class="row">
      <div class="col-xl-3 mb-4">
        <div class="card border-left-warning shadow h-100 py-2">
          <div class="card-body">
            <div class="text-xs font-weight-bold text-warning text-uppercase mb-1">Total Due</div>
            <asp:Label ID="lblDue" runat="server" CssClass="h5 mb-0 font-weight-bold text-gray-800" Text="₹0"></asp:Label>
          </div>
        </div>
      </div>
      <div class="col-xl-3 mb-4">
        <div class="card border-left-success shadow h-100 py-2">
          <div class="card-body">
            <div class="text-xs font-weight-bold text-success text-uppercase mb-1">Last Payment</div>
            <asp:Label ID="lblLastPay" runat="server" CssClass="h5 mb-0 font-weight-bold text-gray-800" Text="--"></asp:Label>
          </div>
        </div>
      </div>
    </div>
  </div>
</asp:Content>
