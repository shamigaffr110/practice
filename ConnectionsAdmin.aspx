<%@ Page Title="Manage Connections" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ConnectionsAdmin.aspx.cs" Inherits="ElectricityBillProject.ConnectionsAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <h2>Manage Connections</h2>

  <asp:GridView ID="gvConnections" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" OnRowCommand="gvConnections_RowCommand" DataKeyNames="connection_id">
    <Columns>
      <asp:BoundField DataField="connection_id" HeaderText="ID" ReadOnly="True" />
      <asp:BoundField DataField="user_id" HeaderText="User ID" />
      <asp:BoundField DataField="consumer_number" HeaderText="Consumer Number" />
      <asp:BoundField DataField="name" HeaderText="Name" />
      <asp:BoundField DataField="phone" HeaderText="Phone" />
      <asp:BoundField DataField="email" HeaderText="Email" />
      <asp:BoundField DataField="address" HeaderText="Address" />
      <asp:BoundField DataField="locality" HeaderText="Locality" />
      <asp:BoundField DataField="type" HeaderText="Type" />
      <asp:BoundField DataField="load" HeaderText="Load (kW)" />
      <asp:BoundField DataField="connection_type" HeaderText="Connection Type" />
      <asp:BoundField DataField="status" HeaderText="Status" />
      <asp:TemplateField HeaderText="Actions">
        <ItemTemplate>
          <asp:Button ID="btnApprove" runat="server" Text="Approve" CommandName="Approve" CommandArgument='<%# Eval("connection_id") %>' CssClass="btn btn-success btn-sm" />
          <asp:Button ID="btnReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("connection_id") %>' CssClass="btn btn-danger btn-sm" />
        </ItemTemplate>
      </asp:TemplateField>
    </Columns>
  </asp:GridView>
</asp:Content>
