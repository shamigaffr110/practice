<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConnectionInfo.aspx.cs" Inherits="ElectricityBillProject.ConnectionInfo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Connection Info</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h4 class="mb-0">📋 My Connection Details</h4>
            </div>
            <div class="card-body">
                <asp:Label ID="lblMsg" runat="server" CssClass="mb-3 text-danger"></asp:Label>

                <asp:GridView ID="gvConnections" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No connections found.">
                    <Columns>
                        <asp:BoundField HeaderText="Connection ID" DataField="connection_id" />
                        <asp:BoundField HeaderText="Consumer Number" DataField="consumer_number" />
                        <asp:BoundField HeaderText="Name" DataField="name" />
                        <asp:BoundField HeaderText="Phone" DataField="phone" />
                        <asp:BoundField HeaderText="Email" DataField="email" />
                        <asp:BoundField HeaderText="Address" DataField="address" />
                        <asp:BoundField HeaderText="Locality" DataField="locality" />
                        <asp:BoundField HeaderText="Type" DataField="type" />
                        <asp:BoundField HeaderText="Load (kW)" DataField="load" />
                        <asp:BoundField HeaderText="Connection Type" DataField="connection_type" />
                        <asp:BoundField HeaderText="Status" DataField="status" />
                        <asp:BoundField HeaderText="Applied On" DataField="created_at" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
