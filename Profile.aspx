<%@ Page Title="Profile" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="ElectricityBillProject.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="card mx-auto mt-5" style="max-width: 600px;">
    <div class="card-body">
      <h4 class="card-title mb-4">My Profile</h4>
      <asp:TextBox ID="txtName" CssClass="form-control mb-2" runat="server" />
      <asp:TextBox ID="txtEmail" CssClass="form-control mb-2" runat="server" />
      <asp:TextBox ID="txtPhone" CssClass="form-control mb-2" runat="server" />
      <asp:Button ID="btnUpdate" CssClass="btn btn-primary mt-2" runat="server" Text="Update" OnClick="btnUpdate_Click" />
      <asp:Label ID="lblMsg" runat="server" CssClass="mt-3 text-success"></asp:Label>
    </div>
  </div>
</asp:Content>
