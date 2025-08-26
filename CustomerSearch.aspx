<%@ Page Title="Customer Search" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CustomerSearch.aspx.cs" Inherits="ElectricityBillProject.CustomerSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <h2>Customer Search</h2>

  <div class="mb-3">
    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Placeholder="Enter name, email or phone"></asp:TextBox>
  </div>
  <div class="mb-3">
    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
  </div>

  <asp:GridView ID="gvCustomers" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" EmptyDataText="No records found.">
    <Columns>
      <asp:BoundField DataField="connection_id" HeaderText="Connection ID" />
      <asp:BoundField DataField="name" HeaderText="Name" />
      <asp:BoundField DataField="phone" HeaderText="Phone" />
      <asp:BoundField DataField="email" HeaderText="Email" />
      <asp:BoundField DataField="address" HeaderText="Address" />
      <asp:BoundField DataField="locality" HeaderText="Locality" />
      <asp:BoundField DataField="type" HeaderText="Type" />
      <asp:BoundField DataField="connection_type" HeaderText="Connection Type" />
      <asp:BoundField DataField="status" HeaderText="Status" />
    </Columns>
  </asp:GridView>
</asp:Content>
