<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConcernsAdmin.aspx.cs" Inherits="ElectricityBillProject.ConcernsAdmin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Concerns Admin</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-4">
        <h3>Manage User Concerns</h3>

        <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mb-3"></asp:Label>

        <!-- Bulk Resolve All Open Concerns Button -->
        <asp:Button ID="btnResolveAll" runat="server" Text="Resolve All Open Concerns" CssClass="btn btn-primary mb-3"
            OnClick="btnResolveAll_Click" />

        <asp:GridView ID="gridConcerns" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
            EmptyDataText="No concerns found." OnRowCommand="gridConcerns_RowCommand">
            <Columns>
                <asp:BoundField DataField="concern_id" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="user_id" HeaderText="User ID" />
                <asp:BoundField DataField="consumer_number" HeaderText="Consumer Number" />
                <asp:BoundField DataField="message" HeaderText="Message" />
                <asp:BoundField DataField="status" HeaderText="Status" />
                <asp:BoundField DataField="created_at" HeaderText="Created At" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="resolved_at" HeaderText="Resolved At" DataFormatString="{0:yyyy-MM-dd HH:mm}" />

                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="btnResolve" runat="server" Text="Resolve" CssClass="btn btn-success btn-sm"
                            CommandName="Resolve" CommandArgument='<%# Eval("concern_id") %>'
                            Enabled='<%# Eval("status").ToString() == "Open" %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
