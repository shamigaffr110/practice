<%@ Page Title="Add Bill" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="AddBill.aspx.cs" Inherits="ElectricityBillProject.AddBill" %>

<asp:Content ContentPlaceHolderID="SidebarLinks" runat="server">
  <ul class="navbar-nav">
    <li class="nav-item active">
      <a class="nav-link" href="AddBill.aspx">
        <i class="fas fa-file-invoice-dollar"></i>
        <span>GenerateBill (Add Bill)</span>
      </a>
    </li>
    <li class="nav-item">
      <a class="nav-link" href="ViewBill.aspx">
        <i class="fas fa-eye"></i>
        <span>View Bill</span>
      </a>
    </li>
    <li class="nav-item">
      <a class="nav-link" href="ConnectionsInfo.aspx">
        <i class="fas fa-plug"></i>
        <span>Connections Info</span>
      </a>
    </li>
    <li class="nav-item">
      <a class="nav-link" href="ConcernsAdmin.aspx">
        <i class="fas fa-comments"></i>
        <span>Concerns Admin</span>
      </a>
    </li>
    <li class="nav-item">
      <a class="nav-link" href="Notices.aspx">
        <i class="fas fa-bell"></i>
        <span>Notices</span>
      </a>
    </li>
    <li class="nav-item">
      <a class="nav-link" href="RevenueReport.aspx">
        <i class="fas fa-chart-line"></i>
        <span>Revenue Report</span>
      </a>
    </li>
    <li class="nav-item">
      <a class="nav-link" href="ConnectionsAdmin.aspx">
        <i class="fas fa-tools"></i>
        <span>Connections Admin</span>
      </a>
    </li>
    <li class="nav-item">
      <a class="nav-link" href="Logout.aspx">
        <i class="fas fa-sign-out-alt"></i>
        <span>Logout</span>
      </a>
    </li>
  </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
  <div class="container-fluid mt-4">

    <!-- Add Bill Card -->
    <div class="card shadow mb-4 mx-auto" style="max-width: 600px;">
      <div class="card-header bg-primary text-white">
        <h5 class="mb-0 font-weight-bold">Add New Electricity Bill</h5>
      </div>
      <div class="card-body">

        <asp:Label ID="lblMsg" runat="server" CssClass="mb-3" />

        <div class="mb-3">
          <asp:TextBox ID="txtConsumer" runat="server" CssClass="form-control" placeholder="Consumer Number" />
          <asp:RequiredFieldValidator ID="rfvConsumer" runat="server" ControlToValidate="txtConsumer" ErrorMessage="*Required" ForeColor="red" Display="Dynamic" />
        </div>

        <div class="mb-3">
          <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Consumer Name" />
          <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ErrorMessage="*Required" ForeColor="red" Display="Dynamic" />
        </div>

        <div class="mb-3">
          <asp:TextBox ID="txtUnits" runat="server" CssClass="form-control" placeholder="Units Consumed" />
          <asp:RequiredFieldValidator ID="rfvUnits" runat="server" ControlToValidate="txtUnits" ErrorMessage="*Required" ForeColor="red" Display="Dynamic" />
          <asp:RangeValidator ID="rangeUnits" runat="server" ControlToValidate="txtUnits" MinimumValue="1" MaximumValue="100000" Type="Integer" ErrorMessage="Invalid units" ForeColor="red" Display="Dynamic" />
        </div>

        <div class="text-end">
          <asp:Button ID="btnGen" runat="server" Text="Generate Bill" CssClass="btn btn-primary" OnClick="btnGen_Click" />
        </div>
      </div>
    </div>

    <!-- Latest Bills Card -->
    <div class="card shadow mx-auto" style="max-width: 900px;">
      <div class="card-header bg-secondary text-white">
        <h5 class="mb-0">Latest Bills</h5>
      </div>
      <div class="card-body">
        <div class="input-group mb-3" style="max-width: 150px;">
          <asp:TextBox ID="txtN" runat="server" CssClass="form-control" Text="5" />
          <div class="input-group-append">
            <asp:Button ID="btnFetch" runat="server" Text="Fetch" CssClass="btn btn-outline-secondary" OnClick="btnFetch_Click" />
          </div>
        </div>
        <asp:GridView ID="grid" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="true" />
      </div>
    </div>

  </div>
</asp:Content>
